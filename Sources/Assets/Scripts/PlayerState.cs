using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerState : BaseGame
{
    public enum State
    {
        Idle, 
        Running, 
        Jumping, 
        ThrowingShuriken, 
        Hide, 
        Katana, 
        KillGobber, 
        Die
    }

    [System.Serializable]
    public class TextureSettings
    {
        public State mState = State.Idle;
        public Texture mSpriteSheet = null;

        public SpriteSettings mSpriteSettings = new SpriteSettings();
    }

    readonly string ShurikenLifePrefab = "Shuriken";
    readonly string ShurikenPrefab = "Hero Shuriken";
    readonly string LifePrefab = "Life";

    private RhythmSequenceManager mRhythmSequenceManager;
    private PlayerUpgrades mPlayerUpgrade;
    private int mNbRunning = 0;
    private bool mHasIncrementedRunning = false;

    public Transform mHand = null;
    public float mLifeScale = 10.0f;

    public float mDistanceToRun = 100.0f;
    public float mHideDuration = 1.0f;
    public float mKatanaDuration = 1.0f;
    public BackgroundTranslate mSpeedReference = null;
    public List<TextureSettings> mSpriteList = new List<TextureSettings>();
    private RhythmSequenceFactory mRhythmSequenceFactory = new RhythmSequenceFactory();

    public AudioSource mJumpSound = null;
    public AudioSource mThrowingShurikenSound = null;

    Gobber mGobber = null;
    Stack<GameObject> mLifeGameObject = new Stack<GameObject>();
    Stack<int> mIsDoingAction = new Stack<int>();
    bool mIsOnGround = false;

    public PlayerUpgrades PlayerUpgrade
    {
        get
        {
            if(mPlayerUpgrade == null)
            {
                mPlayerUpgrade= this.gameObject.GetComponent<PlayerUpgrades>();
            }
            return mPlayerUpgrade;
        }
        set { mPlayerUpgrade = value; }
    }

    void OnCollisionEnter(Collision collision)
    {
        BackgroundTranslate ground = collision.gameObject.GetComponent<BackgroundTranslate>();
        Projectile projectile = collision.gameObject.GetComponent<Projectile>();
        LifeIncrease lifeIncrease = collision.gameObject.GetComponent<LifeIncrease>();
        
        if (ground != null)
        {
            mIsOnGround = true;
        }

        else if (projectile != null)
        {
            mLife--;

            if (mLife == 0)
            {
                SystemMessageManager.Instance.ShowMessage(SystemMessage.YouAreDead, null);
                SwitchState((int)State.Die);
            }

            if (mLifeGameObject.Count != 0)
            {
                DestroyObject(mLifeGameObject.Peek());
                mLifeGameObject.Pop();
            }
        }
        
        else if(lifeIncrease != null)
        {
            mLife++;

            Vector3 lifePosition = Camera.main.ScreenToWorldPoint(new Vector3((mLifeScale * 5.0f), 0.0f, this.transform.position.z));
            lifePosition = new Vector3((lifePosition.x + (mLife-1) * mLifeScale), lifePosition.y, lifePosition.z);
            
            GameObject shurikenGO = (Instantiate(Resources.Load(ShurikenLifePrefab)) as GameObject);
            shurikenGO.transform.position = new Vector3(lifePosition.x, -(lifePosition.y), 20.0f);
            shurikenGO.transform.localScale = new Vector3(mLifeScale, mLifeScale, shurikenGO.transform.localScale.z);

            Shuriken currentShuriken = shurikenGO.GetComponent<Shuriken>();
            currentShuriken.mIsUsingTimeLimit = false;

                
            mLifeGameObject.Push(shurikenGO);
            
            Destroy(collision.gameObject);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        BackgroundTranslate ground = collision.gameObject.GetComponent<BackgroundTranslate>();

        if (ground != null)
        {
            mIsOnGround = false;
        }
    }

    protected override void OnStart()
    {
        mNbRunning = -1;
        Vector3 lifePosition = Camera.main.ScreenToWorldPoint(new Vector3((mLifeScale * 5.0f), 0.0f, this.transform.position.z));

        for (int i = 0; i < mLife; i++)
        {
            GameObject shurikenGO = (Instantiate(Resources.Load(ShurikenLifePrefab)) as GameObject);

            shurikenGO.transform.position = new Vector3(lifePosition.x, -(lifePosition.y), 20.0f);
            shurikenGO.transform.localScale = new Vector3(mLifeScale, mLifeScale, shurikenGO.transform.localScale.z);

            Shuriken currentShuriken = shurikenGO.GetComponent<Shuriken>();
            currentShuriken.mIsUsingTimeLimit = false;

            lifePosition = new Vector3((lifePosition.x + mLifeScale), lifePosition.y, lifePosition.z);

            mLifeGameObject.Push(shurikenGO);
        }

        SpawnEnemy.IsFirstEnemy = true;

        mRhythmSequenceManager = new RhythmSequenceManager();        
        BaseGame.IsEnvironmentMoving = false;
        SwitchState((int)State.Idle);


        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Life"), LayerMask.NameToLayer("Shuriken"));
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Life"), LayerMask.NameToLayer("Enemy"));
    }

    protected override void EnterState()
    {
        switch ((State)mStateID)
        {
            case State.Idle:
                {
                }
                break;

            case State.Running:
                {
                    BaseGame.IsEnvironmentMoving = true;

                    StartCoroutine(coRun());

                    SpawnEnemy.IsMoving = true;
                    SpawnEnemy.TotalDistance = (mDistanceToRun / 10.0f);

                    GameObject[] shurikenList = GameObject.FindGameObjectsWithTag("Shuriken");

                    for (int i = 0; i < shurikenList.Length; i++)
                    {
                        DestroyObject(shurikenList[i]);
                    }
                }
                break;

            case State.Jumping:
                {
                    mIsOnGround = false;

                    StartCoroutine(coJump());

                    if (mJumpSound != null)
                    {
                        SoundManager.Instance.Play(mJumpSound);
                    }
                }
                break;

            case State.ThrowingShuriken:
                {
                    StartCoroutine(coThrowShuriken());

                    if (mThrowingShurikenSound != null)
                    {
                        SoundManager.Instance.Play(mThrowingShurikenSound);
                    }
                }
                break;

            case State.Hide:
                {
                    StartCoroutine(coHide());
                }
                break;

            case State.Katana:
                {
                    StartCoroutine(coKatana());
                }
                break;

            case State.Die:
                {
                    mIsDoingAction.Clear();

                    BoxCollider boxCollider = this.GetComponent<BoxCollider>();

                    boxCollider.size = new Vector3(
                        boxCollider.size.x,
                        (boxCollider.size.y - 0.2f),
                        boxCollider.size.z);
                }
                break;

            default: break;
        }
    }

    void OnGUI()
    {
        mRhythmSequenceManager.UpdateGUI();
    }


    void FixedUpdate()
    {
        mRhythmSequenceManager.UpdateRhythmSequenceManagerTimer();
    }

    protected override void UpdateState()
    {
        UpdateNewUpgrade();

        UpdateRhythmSequenceManager();
        switch ((State)mStateID)
        {
            default: break;
        }
    }

    private void UpdateNewUpgrade()
    {
        if (PlayerUpgrade.NewUpgrade)
        {
            if(PlayerUpgrade.LastUpgrade ==PlayerUpgradeTypes.CanDodge)
            {
                TutorielManager.Instance.ShowMessage("New technique : Dodge with → , ↑ , → ");
                mRhythmSequenceManager.DodgeSequence = mRhythmSequenceFactory.CreateDodgeSequence();
            }
            else if(PlayerUpgrade.LastUpgrade ==PlayerUpgradeTypes.CanJump)
            {
                TutorielManager.Instance.ShowMessage("New technique : Jump with ↑ , ↓ , ↑ ");
                mRhythmSequenceManager.JumpSequence = mRhythmSequenceFactory.CreateJumpSequence();
            }
            else if (PlayerUpgrade.LastUpgrade == PlayerUpgradeTypes.Katana)
            {
                TutorielManager.Instance.ShowMessage("New technique : Kill Gobber with ↓, →, ↑, ←");
                mRhythmSequenceManager.DuckSequence = mRhythmSequenceFactory.CreateDuckSequence();
            }
            else if (PlayerUpgrade.LastUpgrade == PlayerUpgradeTypes.SkurikenSpeed)
            {
                mShurikenForce *= 2; 
            }
            else if (PlayerUpgrade.LastUpgrade == PlayerUpgradeTypes.ShurikenNumber)
            {
                mNbShuriken *= 2;
            }
            else if (PlayerUpgrade.LastUpgrade == PlayerUpgradeTypes.JumpHigher)
            {
                this.rigidbody.mass = 1;
            }
            else if (PlayerUpgrade.LastUpgrade == PlayerUpgradeTypes.JumpFaster)
            {
                mJumpForce *= 2;
                this.rigidbody.mass = 1.48f;
            }
            else if (PlayerUpgrade.LastUpgrade == PlayerUpgradeTypes.DodgeDuration)
            {
                mHideDuration = 3.0f;
            }

            PlayerUpgrade.NewUpgrade = false;
        }
    }

    private void UpdateRhythmSequenceManager()
    {
        if (mStateID != (int)State.Running)
        {
            mRhythmSequenceManager.UpdateRhythmSequenceManager();
            if (mRhythmSequenceManager.CurrentSequence != null && mRhythmSequenceManager.CurrentState == RhythmSequenceManager.State.Success)
            {
                mRhythmSequenceManager.CurrentState = RhythmSequenceManager.State.WaitingForActionToEnd;
                mRhythmSequenceManager.CooldownTime = 0;
                
                if (mRhythmSequenceManager.IndexSequence == RhythmSequenceManager.Attack && mRhythmSequenceManager.AttackSequence != null)
                {
                    SwitchState((int)State.ThrowingShuriken);
                }
                else if (mRhythmSequenceManager.IndexSequence == RhythmSequenceManager.Dodge && mRhythmSequenceManager.DodgeSequence != null)
                {
                    SwitchState((int)State.Hide);
                }
                else if (mRhythmSequenceManager.IndexSequence == RhythmSequenceManager.Duck && mRhythmSequenceManager.DuckSequence != null && 
                    mGobber != null)
                {
                    if (!mGobber.IsFirstKill && mGobber.mLife <= 0 && (State)mStateID != State.Katana)
                    {
                        SwitchState((int)State.Katana);
                    }
                }
                else if (mRhythmSequenceManager.IndexSequence == RhythmSequenceManager.Jump && mRhythmSequenceManager.JumpSequence != null)
                {
                    SwitchState((int)State.Jumping);
                }

                mRhythmSequenceManager.IndexSequence = -1;
            }

            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    SwitchState((int)State.Jumping);
            //}

            //else if (Input.GetKeyDown(KeyCode.RightShift))
            //{
            //    SwitchState((int)State.ThrowingShuriken);
            //}

            //else if (Input.GetKeyDown(KeyCode.RightControl))
            //{
            //    SwitchState((int)State.Hide);
            //}

            //else if (Input.GetKeyDown(KeyCode.RightAlt))
            //{
            //    SwitchState((int)State.Katana);
            //}

            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                if (GameStateManager.Instance.GameState.CurrentState != GameState.State.UpgradeState)
                {
                    if (mHasIncrementedRunning == false)
                    {
                        mNbRunning++;
                        mHasIncrementedRunning = true;
                        if (mNbRunning == 0)
                        {
                            TutorielManager.Instance.ShowMessage("Basics : Attack with the rhythm ← , →, ← ");
                            mNbRunning++;
                        } 
                        else if (mNbRunning % 2 == 0)
                        {
                            if (mNbRunning == 2)
                            {
                                TutorielManager.Instance.ShowMessage("Choose upgrades wisely");
                            }

                            GameStateManager.Instance.GameState.SwitchState(GameState.State.UpgradeState);
                        }
                        else if (mNbRunning == 7)
                        {
                            TutorielManager.Instance.ShowMessage("Wait and react. A good strategy.");
                        }
                        else if (mNbRunning == 11)
                        {
                            TutorielManager.Instance.ShowMessage("Gaining all skills can make your quest a succesful one.");
                        }
                    }
                    else
                    {
                        if (mNbRunning == 3)
                        {
                            SystemMessageManager.Instance.ShowMessage(SystemMessage.Follow, null);
                        }
                        else if (mNbRunning == 5)
                        {
                            SystemMessageManager.Instance.ShowMessage(SystemMessage.ItTakes, null);
                        }
                        else if (mNbRunning == 7)
                        {
                            SystemMessageManager.Instance.ShowMessage(SystemMessage.Learning, null);
                        }
                        else if (mNbRunning == 9)
                        {
                            SystemMessageManager.Instance.ShowMessage(SystemMessage.Technique, null);
                        }
                        else if (mNbRunning == 11)
                        {
                            SystemMessageManager.Instance.ShowMessage(SystemMessage.Reaction, null);
                        }
                        else if (mNbRunning == 13)
                        {
                            SystemMessageManager.Instance.ShowMessage(SystemMessage.LeftRight, null);
                        }
                        else if (mNbRunning % 2 == 0)
                        {
                            SystemMessageManager.Instance.ShowMessage(SystemMessage.LevelUp, null);
                        }
                        else
                        {
                            SystemMessageManager.Instance.ShowMessage(SystemMessage.Fight, null);
                        }

                        mHasIncrementedRunning = false;
                        SwitchState((int)State.Running);
                    }
                }
            }
        }

        switch ((State)mStateID)
        {
            default: break;
        }
    }

    protected override void ExitState()
    {
        if ((State)mStateID != State.Idle)
        {
            //mRhythmSequenceManager.IndexSequence = -1;
            //mRhythmSequenceManager.CurrentState = RhythmSequenceManager.State.NoSequence;
        }

        if((State)mStateID == State.Hide)
        {
            if (mPlayerUpgrade.CurrentPlayerUpgradeTypes.Contains(PlayerUpgradeTypes.DodgeAttackReturn))
            {
                GameObject shurikenGO = (Instantiate(
                Resources.Load(ShurikenPrefab),
                mHand.position,
                Quaternion.identity) as GameObject);

                shurikenGO.rigidbody.AddForce((Vector3.left * mShurikenForce));
            }
        }

        switch ((State)mStateID)
        {
            case State.Running:
                {
                    if (SpawnEnemy.IsBossFight)
                    {
                        mGobber = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Gobber>();
                    }
                }
                break;

            default: break;
        }
    }

    protected override void SetSpriteTexture()
    {
        SpriteManager.Instance.SetSpriteTexture(this.gameObject, GetTexture(), GetSpriteSettings());
    }

    Texture GetTexture()
    {
        foreach (TextureSettings state in mSpriteList)
        {
            if (state.mState == (State)mStateID)
            {
                return state.mSpriteSheet;
            }
        }

        return null;
    }

    SpriteSettings GetSpriteSettings()
    {
        foreach (TextureSettings state in mSpriteList)
        {
            if (state.mState == (State)mStateID)
            {
                return state.mSpriteSettings;
            }
        }

        return new SpriteSettings();
    }

    void PopAction()
    {
        if (mIsDoingAction.Count > 0)
        {
            mIsDoingAction.Pop();

            if (mIsDoingAction.Count == 0)
            {
                SwitchState((int)State.Idle);
            }
        }
    }

    IEnumerator coRun()
    {
        float distance = 0.0f;

        if (mIsDoingAction.Contains(mStateID))
        {
            yield break;
        }

        SoundManager.Instance.Play("Run");

        mIsDoingAction.Push(mStateID);

        while (distance < (mDistanceToRun / 10.0f))
        {
            distance += (Time.deltaTime * mSpeedReference.mTranslationSpeed);
            SpawnEnemy.Distance = distance;

            yield return new WaitForEndOfFrame();
        }

        BaseGame.IsEnvironmentMoving = SpawnEnemy.IsMoving = false;
        PopAction();
    }

    IEnumerator coJump()
    {
        if (mIsDoingAction.Contains(mStateID))
        {
            yield break;
        }

        mIsDoingAction.Push(mStateID);

        SoundManager.Instance.Play("jump");

        this.rigidbody.AddForce((Vector3.up * mJumpForce));
        
        while (!mIsOnGround)
        {
            yield return new WaitForEndOfFrame();
        }

        PopAction();
    }

    IEnumerator coThrowShuriken()
    {
        if (mIsDoingAction.Contains(mStateID))
        {
            yield break;
        }

        mIsDoingAction.Push(mStateID);


        SoundManager.Instance.Play("throw");
        
        GameObject shurikenGO = (Instantiate(
            Resources.Load(ShurikenPrefab),
            mHand.position,
            Quaternion.identity) as GameObject);

        Physics.IgnoreCollision(shurikenGO.collider, GameObject.FindGameObjectWithTag("Player").collider);

        shurikenGO.rigidbody.AddForce((Vector3.left * mShurikenForce));

        int valeur = Random.Range(0, mLife * 4);
        if (valeur <= 1)
        {
            Instantiate(Resources.Load(LifePrefab));
        }

        if (mNbShuriken == 2)
        {
            GameObject shurikenGO2 = (Instantiate(
              Resources.Load(ShurikenPrefab),
              mHand.position,
              Quaternion.identity) as GameObject);
            Vector3 force = (Vector3.left*mShurikenForce);

            if (mIsOnGround)
            {
                force += new Vector3(0, 500, 0);
            }
            else
            {
                force += new Vector3(0, -250, 0);
            }

            shurikenGO2.rigidbody.AddForce(force);
        }
        
        for (int i = 0; i < (GetSpriteSettings().mFramesPerSecond * GetSpriteSettings().mNbColumns); i++)
        {
            yield return new WaitForEndOfFrame();
        }

        PopAction();
    }

    IEnumerator coHide()
    {
        if (mIsDoingAction.Contains(mStateID))
        {
            yield break;
        }

        mIsDoingAction.Push(mStateID);

        SoundManager.Instance.Play("hide");

        SpriteManager.Instance.RestartFrame(this.gameObject, 2, 2);

        while (!SpriteManager.Instance.IsPaused(this.gameObject))
        {
            yield return new WaitForEndOfFrame();
        }

        OnAvoid();
        yield return new WaitForSeconds(mHideDuration);

        SpriteManager.Instance.RestartFrame(this.gameObject, 2, 2);

        while (!SpriteManager.Instance.IsPaused(this.gameObject))
        {
            yield return new WaitForEndOfFrame();
        }

        OnAvoid(false);
        PopAction();
    }

    IEnumerator coDecapitateGobber()
    {
        SpriteManager.Instance.RestartFrame(this.gameObject, 2, 2);

        while (!SpriteManager.Instance.IsPaused(this.gameObject))
        {
            yield return new WaitForEndOfFrame();
        }

        mGobber.Decapitated();
        SwitchState((int)State.Idle);
    }

    IEnumerator coKatana()
    {
        while (this.transform.position.x >= (mGobber.transform.position.x + (mGobber.transform.localScale.x / 2.0f)))
        {
            this.transform.position = new Vector3(
                (this.transform.position.x - (Time.deltaTime * 20.0f)),
                this.transform.position.y,
                this.transform.position.z);

            yield return new WaitForEndOfFrame();
        }

        StartCoroutine(coDecapitateGobber());
        SwitchState((int)State.KillGobber);
        //if (mIsDoingAction.Contains(mStateID))
        //{
        //    yield break;
        //}

        //mIsDoingAction.Push(mStateID);

        //OnAvoid();
        //yield return new WaitForSeconds(mKatanaDuration);

        //OnAvoid(false);
        //PopAction();
    }

    void OnAvoid(bool isAvoidCollision = true)
    {
        Physics.IgnoreLayerCollision(this.gameObject.layer, LayerMask.NameToLayer("Shuriken"), isAvoidCollision);
    }
}
