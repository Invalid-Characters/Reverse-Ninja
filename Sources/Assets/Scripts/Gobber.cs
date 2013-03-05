using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gobber : BaseGame
{
    public enum State
    {
        Idle,
        Running,
        Jumping,
        Attack, 
        Stun, 
        Die
    }

    [System.Serializable]
    public class TextureSettings
    {
        public State mState = State.Idle;
        public Texture mSpriteSheet = null;

        public SpriteSettings mSpriteSettings = new SpriteSettings();
    }

    public GameObject mFlyPrefab = null;
    public Transform mHand = null;
    public List<TextureSettings> mSpriteList = new List<TextureSettings>();
    public Vector2 mRandomActionJumpTime = Vector2.zero;
    public Vector2 mRandomActionShurikenTime = Vector2.zero;
    public float mEnrageActionTweak = 0.0f;

    public BackgroundTranslate mSpeedReference = null;
    public float mDistanceTweak = 50.0f;
    public float mDecapitationTime = 2.0f;
    public int mEnrageLife = 10;

    float mActionJumpTime = 0.0f, mActionShurikenTime = 0.0f;
    float mTimeJumpElapsed = 0.0f, mTimeShurikenElapsed = 0.0f;
    Stack<int> mIsDoingAction = new Stack<int>();
    bool mIsFixedPosition = false;
    bool mIsOnGround = false;

    bool mIsFirstKill = true;
    public bool IsFirstKill
    {
        get { return mIsFirstKill; }
    }

    void Awake()
    {
        mSpeedReference = GameObject.Find("Ground").GetComponent<BackgroundTranslate>();
        
        Physics.IgnoreLayerCollision(this.gameObject.layer, this.gameObject.layer);
    }

    void FixedUpdate()
    {
        if (SpawnEnemy.IsMoving && !mIsFixedPosition)
        {
            this.transform.localPosition = new Vector3(
                (this.transform.localPosition.x + ((Time.deltaTime * mSpeedReference.mTranslationSpeed) * mDistanceTweak)),
                this.transform.localPosition.y,
                this.transform.localPosition.z);
        }

        else if (!mIsFixedPosition)
        {
            mIsFixedPosition = true;
            SwitchState((int)State.Idle);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        BackgroundTranslate ground = collision.gameObject.GetComponent<BackgroundTranslate>();
        Shuriken shuriken = collision.gameObject.GetComponent<Shuriken>();

        if (ground != null)
        {
            mIsOnGround = true;
        }

        else if (shuriken != null)
        {
            mLife--;

            if (mLife == 0)
            {
                SystemMessageManager.Instance.ShowMessage(SystemMessage.XP, this.gameObject);

                if (!mIsFirstKill)
                {
                    this.renderer.material.color = Color.white;
                    SwitchState((int)State.Stun);

                    mLife--;
                }

                else
                {
                    mIsFirstKill = false;
                    mLife = mEnrageLife;
                    StartCoroutine(coEnrage());

                    mRandomActionShurikenTime = new Vector2(
                        (mRandomActionShurikenTime.x * mEnrageActionTweak),
                        (mRandomActionShurikenTime.y * mEnrageActionTweak));
                }
            }
        }
    }

    protected override void OnStart()
    {
        SwitchState((int)State.Running);
    }

    protected override void EnterState()
    {
        switch ((State)mStateID)
        {
            case State.Idle:
                {
                    mActionJumpTime = Random.Range(mRandomActionJumpTime.x, mRandomActionJumpTime.y);
                    mActionShurikenTime = Random.Range(mRandomActionShurikenTime.x, mRandomActionShurikenTime.y);
                    mTimeJumpElapsed = mTimeShurikenElapsed = 0.0f;
                }
                break;

            case State.Jumping:
                {
                    mIsOnGround = false;

                    StartCoroutine(coJump());
                }
                break;

            case State.Attack:
                {
                    StartCoroutine(coAttack());
                }
                break;

            case State.Stun:
                {
                    mIsDoingAction.Clear();
                }
                break;

            case State.Die:
                {
                    mIsDoingAction.Clear();

                    this.gameObject.layer = LayerMask.NameToLayer("Shuriken");
                    StartCoroutine(coDie());
                }
                break;
        }
    }

    protected override void UpdateState()
    {
        if (!BaseGame.IsEnvironmentMoving && (State)mStateID != State.Stun)
        {
            mTimeJumpElapsed += Time.deltaTime;
            mTimeShurikenElapsed += Time.deltaTime;

            if (mTimeJumpElapsed >= mActionJumpTime)
            {
                SwitchState((int)State.Jumping);
                mTimeJumpElapsed = 0.0f;

                mActionJumpTime = Random.Range(mRandomActionJumpTime.x, mRandomActionJumpTime.y);
            }

            if (!SpawnEnemy.IsFirstEnemy)
            {
                if (mTimeShurikenElapsed >= mActionShurikenTime)
                {
                    SwitchState((int)State.Attack);
                    mTimeShurikenElapsed = 0.0f;

                    mActionShurikenTime = Random.Range(mRandomActionShurikenTime.x, mRandomActionShurikenTime.y);
                }
            }
        }

        switch ((State)mStateID)
        {
            case State.Idle:
                {
                    
                }
                break;
        }
    }

    protected override void ExitState()
    {
    }

    protected override void SetSpriteTexture()
    {
        SpriteManager.Instance.SetSpriteTexture(this.gameObject, GetTexture(), GetSpriteSettings());
    }

    public void Decapitated()
    {
        SwitchState((int)State.Die);
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
        if (mIsDoingAction.Count != 0)
        {
            mIsDoingAction.Pop();

            if (mIsDoingAction.Count == 0)
            {
                SwitchState((int)State.Idle);
            }
        }
    }

    void CheckStack()
    {
        if (mIsDoingAction.Count == 1)
        {
            StartCoroutine(coCheckStack());
        }
    }

    IEnumerator coCheckStack()
    {
        float timeElapsed = 0.0f;

        while (mIsDoingAction.Count != 0)
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed >= 5.0f)
            {
                mIsDoingAction.Clear();
                SwitchState((int)State.Idle);

                yield break;
            }

            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator coJump()
    {
        if (mIsDoingAction.Contains(mStateID))
        {
            yield break;
        }

        mIsDoingAction.Push(mStateID);
        CheckStack();

        this.rigidbody.AddForce((Vector3.up * mJumpForce));

        while (!mIsOnGround)
        {
            yield return new WaitForEndOfFrame();
        }

        PopAction();
    }

    IEnumerator coAttack()
    {
        if (mIsDoingAction.Contains(mStateID))
        {
            yield break;
        }

        mIsDoingAction.Push(mStateID);
        CheckStack();

        GameObject flyGO = (Instantiate(
            mFlyPrefab,
            mHand.position,
            Quaternion.identity) as GameObject);

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Physics.IgnoreCollision(flyGO.collider, go.collider);
        }

        flyGO.rigidbody.AddForce((Vector3.right * mShurikenForce));

        for (int i = 0; i < (GetSpriteSettings().mFramesPerSecond * GetSpriteSettings().mNbColumns); i++)
        {
            yield return new WaitForEndOfFrame();
        }

        PopAction();
    }

    IEnumerator coDie()
    {
        SpriteManager.Instance.RestartFrame(this.gameObject, 1, 2);

        while (!SpriteManager.Instance.IsPaused(this.gameObject))
        {
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(mDecapitationTime);

        Material material = this.renderer.material;

        while (material.color.a > 0.0f)
        {
            material.color = new Color(material.color.r, material.color.g, material.color.b, (material.color.a - Time.deltaTime));

            yield return new WaitForEndOfFrame();
        }

        while (BaseGame.IsEnvironmentMoving)
        {
            yield return new WaitForEndOfFrame();
        }

        Application.LoadLevel("FinalCutscene");
        DestroyObject(this.gameObject);
    }

    IEnumerator coEnrage()
    {
        Material material = this.renderer.material;
        Vector3 colorScale = new Vector3(
            (Color.red.r - material.color.r),
            (Color.red.g - material.color.g),
            (Color.red.b - material.color.b));

        float timeElapsed = 0.0f;
        
        while (material.color != Color.red)
        {
            material.color = new Color(
                (material.color.r + (colorScale.x * Time.deltaTime)),
                (material.color.g + (colorScale.y * Time.deltaTime)),
                (material.color.b + (colorScale.z * Time.deltaTime)));

            timeElapsed += Time.deltaTime;

            if (timeElapsed >= 5.0f)
            {
                material.color = Color.red;

                yield break;
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
