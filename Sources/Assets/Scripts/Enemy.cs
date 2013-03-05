using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : BaseGame
{
    public enum State
    {
        Idle,
        Running,
        Jumping,
        ThrowingShuriken, 
        Die
    }

    [System.Serializable]
    public class TextureSettings
    {
        public State mState = State.Idle;
        public Texture mSpriteSheet = null;

        public SpriteSettings mSpriteSettings = new SpriteSettings();
    }

    readonly string ShurikenPrefab = "Enemy Shuriken";

    private RhythmSequenceManager mRhythmSequenceManager;

    public Transform mHand = null;
    public List<TextureSettings> mSpriteList = new List<TextureSettings>();
    public Vector2 mRandomActionJumpTime = Vector2.zero;
    public Vector2 mRandomActionShurikenTime = Vector2.zero;

    public BackgroundTranslate mSpeedReference = null;
    public float mDistanceTweak = 50.0f;

    float mActionJumpTime = 0.0f, mActionShurikenTime = 0.0f;
    float mTimeJumpElapsed = 0.0f, mTimeShurikenElapsed = 0.0f;
    Stack<int> mIsDoingAction = new Stack<int>();
    bool mIsFixedPosition = false;
    bool mIsOnGround = false;
    bool mIsThrowReady = false;

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

        if (this.transform.position.y < -250.0f)
        {
            DestroyObject(this.gameObject);
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
                SwitchState((int)State.Die);
            }
        }
    }

    void OnDestroy()
    {
        SpawnEnemy.IsFirstEnemy = false;
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
                    SetSpriteTexture();

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

            case State.ThrowingShuriken:
                {
                    StartCoroutine(coThrowShuriken());
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
        if (!BaseGame.IsEnvironmentMoving && mIsThrowReady)
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
                    SwitchState((int)State.ThrowingShuriken);
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

        if (this.gameObject != null)
        {
            if (mLife <= 0)
            {
                DestroyObject(this.gameObject);
            }
        }
    }

    protected override void ExitState()
    {
        switch ((State)mStateID)
        {
            case State.Running:
                {
                    mTimeJumpElapsed = mTimeShurikenElapsed = 0.0f;
                    mIsThrowReady = true;
                }
                break;
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

        SoundManager.Instance.Play("enemyjump");

        mIsDoingAction.Push(mStateID);
        CheckStack();

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
        CheckStack();


        SoundManager.Instance.Play("throw");

        GameObject shurikenGO = (Instantiate(
            Resources.Load(ShurikenPrefab),
            mHand.position,
            Quaternion.identity) as GameObject);

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Physics.IgnoreCollision(shurikenGO.collider, go.collider);
        }

        shurikenGO.rigidbody.AddForce((Vector3.right * mShurikenForce));

        for (int i = 0; i < (GetSpriteSettings().mFramesPerSecond * GetSpriteSettings().mNbColumns); i++)
        {
            yield return new WaitForEndOfFrame();
        }

        PopAction();
    }

    IEnumerator coDie()
    {
        SpriteManager.Instance.RestartFrame(this.gameObject, 2, 2);

        while (!SpriteManager.Instance.IsPaused(this.gameObject))
        {
            yield return new WaitForEndOfFrame();
        }

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

        DestroyObject(this.gameObject);
    }
}
