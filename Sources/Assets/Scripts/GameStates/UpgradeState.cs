using UnityEngine;
using System.Collections;

public class UpgradeState : GameState
{
    private float top = 0;
    private float height = 0;
    private float left = 0;
    private float width = 0;

    public int mCptUpgrade = 5;

    private Texture2D mCanThrowShuriken;
    private Texture2D mShurikenNumber;
    private Texture2D mShurikenSpeed;
    private Texture2D mCanJump;
    private Texture2D mJumpHigher;
    private Texture2D mJumpFaster;
    private Texture2D mCanDodge;    
    private Texture2D mDodgeDuration;
    private Texture2D mDodgeAttackReturn;
    private Texture2D mKatana;

    public Texture2D CanThrowShuriken
    {
        get
        {
            if (mCanThrowShuriken == null)
            {
                mCanThrowShuriken = Resources.Load("UI/Jump") as Texture2D;
            }
            return mCanThrowShuriken;
        }
        set { mCanThrowShuriken = value; }
    }

    public Texture2D ShurikenNumber
    {
        get
        {
            if (mShurikenNumber == null)
            {
                mShurikenNumber = Resources.Load("UI/Jump") as Texture2D;
            }
            return mShurikenNumber;
        }
        set { mShurikenNumber = value; }
    }

    public Texture2D ShurikenSpeed
    {
        get
        {
            if (mShurikenSpeed == null)
            {
                mShurikenSpeed = Resources.Load("UI/Jump") as Texture2D;
            }
            return mShurikenSpeed;
        }
        set { mShurikenSpeed = value; }
    }

    public Texture2D CanJump
    {
        get
        {
            if (mCanJump == null)
            {
                mCanJump = Resources.Load("UI/Jump") as Texture2D;
            }
            return mCanJump;
        }
        set { mCanJump = value; }
    }

    public Texture2D JumpHigher
    {
        get
        {
            if (mJumpHigher == null)
            {
                mJumpHigher = Resources.Load("UI/Jump") as Texture2D;
            }
            return mJumpHigher;
        }
        set { mJumpHigher = value; }
    }

    public Texture2D JumpFaster
    {
        get
        {
            if (mJumpFaster == null)
            {
                mJumpFaster = Resources.Load("UI/Jump") as Texture2D;
            }
            return mJumpFaster;
        }
        set { mJumpFaster = value; }
    }

    public Texture2D CanDodge
    {
        get
        {
            if (mCanDodge == null)
            {
                mCanDodge = Resources.Load("UI/Jump") as Texture2D;
            }
            return mCanDodge;
        }
        set { mCanDodge = value; }
    }

    public Texture2D DodgeDuration
    {
        get
        {
            if (mDodgeDuration == null)
            {
                mDodgeDuration = Resources.Load("UI/Jump") as Texture2D;
            }
            return mDodgeDuration;
        }
        set { mDodgeDuration = value; }
    }

    public Texture2D DodgeAttackReturn
    {
        get
        {
            if (mDodgeAttackReturn == null)
            {
                mDodgeAttackReturn = Resources.Load("UI/Jump") as Texture2D;
            }
            return mDodgeAttackReturn;
        }
        set { mDodgeAttackReturn = value; }
    }

    public Texture2D Katana
    {
        get
        {
            if (mKatana == null)
            {
                mKatana = Resources.Load("UI/Jump") as Texture2D;
            }
            return mKatana;
        }
        set { mKatana = value; }
    }

    private PlayerUpgrades mPlayerUpgrades;

    public PlayerUpgrades PlayerUpgrade
    {
        get 
        { 
            if(mPlayerUpgrades == null)
            {
                PlayerUpgrades upgrade = GameObject.Find("Player").GetComponent<PlayerUpgrades>();
                if (upgrade != null)
                {
                    mPlayerUpgrades = upgrade;
                }
            }
            return mPlayerUpgrades;
        }
        set { mPlayerUpgrades = value; }
    }

    private Texture2D mBackground;
    private Texture2D mLevelUp;
    private Texture2D mAlreadyUpgrade;
    private Texture2D mCantUpgrade;
    private Texture2D mShurikenType;
    private Texture2D mJumpType;
    private Texture2D mDodgeType;
    private Texture2D mKatanaType;

    private Texture2D mKatanaing;
    private Texture2D mDodgeAttackCounter;
    private Texture2D mDodgeCan;
    private Texture2D mDodgeFaster;
    private Texture2D mJumpFasterImg;
    private Texture2D mJumpHigherImg;
    private Texture2D mJump;
    private Texture2D mShurikenOfNumber;
    private Texture2D mShurikenSpeedImg;
    private Texture2D mShurikenThrowCan;



    public UpgradeState()
    {
        top = (Screen.height * (0.25f / 9.0f));
        height = (Screen.height * (8.5f / 9.0f));
        left = (Screen.width * (0.2f));
        width = (Screen.width * (0.5f));

        mCurrentState = State.UpgradeState;

        mKatanaing = Resources.Load("UI/Katana") as Texture2D;
        mDodgeAttackCounter = Resources.Load("UI/dodgeattackcounter") as Texture2D;
        mDodgeCan = Resources.Load("UI/dodgecan") as Texture2D;
        mDodgeFaster = Resources.Load("UI/dodgefaster") as Texture2D;
        mJumpFasterImg = Resources.Load("UI/jumpfaster") as Texture2D;
        mJumpHigherImg = Resources.Load("UI/jumphigher") as Texture2D;
        mJump = Resources.Load("UI/Jump") as Texture2D;
        mShurikenOfNumber = Resources.Load("UI/shurikenofnumber") as Texture2D;
        mShurikenSpeedImg = Resources.Load("UI/shurikenofspeed") as Texture2D;
        mShurikenThrowCan = Resources.Load("UI/THROWCANSHURKIEN") as Texture2D;

        mBackground = Resources.Load("UI/upgbck") as Texture2D;
        mLevelUp = Resources.Load("UI/upgrade") as Texture2D;
        mAlreadyUpgrade = Resources.Load("UI/alrreadyupgrade") as Texture2D;
        mCantUpgrade = Resources.Load("UI/cantupgrade") as Texture2D;
        mShurikenType = Resources.Load("UI/shuriken") as Texture2D;
        mJumpType = Resources.Load("UI/jumptype") as Texture2D;
        mDodgeType = Resources.Load("UI/dodgetype") as Texture2D;
        mKatanaType = Resources.Load("UI/Katanatype") as Texture2D;
    }

    protected override void EnterState()
    {
        
    }

    public override void UpdateState()
    {
    }



    public override void UpdateStateGUI()
    {
        if(PlayerUpgrade.CurrentPlayerUpgradeTypes.Count > mCptUpgrade)
        {
            return;
        }

        Texture2D currentTexture = GUI.skin.box.normal.background;
        
        GUI.skin.box.normal.background = mBackground;

        GUI.Box(new Rect(left, top, width, height), "");

        GUI.skin.box.normal.background = currentTexture;

        GUI.BeginGroup(new Rect(left, top, width, height));
        GUI.Box(new Rect(0, 0, width, height * 0.2f), mLevelUp);

            float upgradeBoxWidth = width;
            float upgradeBoxHeight = height*0.8f;
        
            ShurikenUpgradeBox(upgradeBoxHeight, upgradeBoxWidth);

            JumpUpgradeBox(upgradeBoxHeight, upgradeBoxWidth);

            DodgeUpgradeBox(upgradeBoxHeight, upgradeBoxWidth);

            KatanaUpgradeBox(upgradeBoxHeight, upgradeBoxWidth);
        GUI.EndGroup();
    }

    private void KatanaUpgradeBox(float upgradeBoxHeight, float upgradeBoxWidth)
    {
        GUI.BeginGroup(new Rect(0, height * 0.92f, upgradeBoxWidth, upgradeBoxHeight));
        GUI.BeginGroup(new Rect(0, 0, upgradeBoxWidth, upgradeBoxHeight));
        float widthThrowShuriken = upgradeBoxWidth;
        float heightThrowShuriken = upgradeBoxHeight * 0.1f;
        GUI.BeginGroup(new Rect(0, 0, widthThrowShuriken, heightThrowShuriken));
        GUI.Box(new Rect(0, 0, widthThrowShuriken * 0.3f, heightThrowShuriken), mKatanaType); // Image
        GUI.Box(new Rect(widthThrowShuriken * 0.3f, 0, widthThrowShuriken * 0.4f, heightThrowShuriken), mKatanaing); // Texte
        if (PlayerUpgrade.CurrentPlayerUpgradeTypes.Count != mCptUpgrade)
        {
            GUI.Box(new Rect(widthThrowShuriken * 0.7f, 0, widthThrowShuriken * 0.3f, heightThrowShuriken), mCantUpgrade); // Pas disponible
        }
        else
        {
            if (GUI.Button(new Rect(widthThrowShuriken * 0.7f, 0, widthThrowShuriken * 0.3f, heightThrowShuriken), "UPGRADE"))
            {
                mPlayerUpgrades.AddUpgrade(PlayerUpgradeTypes.Katana); //Upgrade
                SwitchState(State.MovingState);
            }
        }
        GUI.EndGroup();
        GUI.EndGroup();
        GUI.EndGroup();
    }

    private void DodgeUpgradeBox(float upgradeBoxHeight, float upgradeBoxWidth)
    {
        float widthThrowShuriken = upgradeBoxWidth;
        float heightThrowShuriken = upgradeBoxHeight * 0.1f;
        GUI.BeginGroup(new Rect(0, height * 0.68f, upgradeBoxWidth, upgradeBoxHeight));

        GUI.Box(new Rect(0, 0, widthThrowShuriken * 0.3f, heightThrowShuriken * 3), mDodgeType); // Image

        GUI.BeginGroup(new Rect(0, 0, upgradeBoxWidth, upgradeBoxHeight));
        GUI.BeginGroup(new Rect(0, 0, widthThrowShuriken, heightThrowShuriken));
        GUI.Box(new Rect(widthThrowShuriken * 0.3f, 0, widthThrowShuriken * 0.4f, heightThrowShuriken), mDodgeCan); // Texte
        if (PlayerUpgrade.CurrentPlayerUpgradeTypes.Contains(PlayerUpgradeTypes.CanDodge))
        {
            GUI.Box(new Rect(widthThrowShuriken * 0.7f, 0, widthThrowShuriken * 0.3f, heightThrowShuriken), mAlreadyUpgrade); // Déjà prit par défaut
        }
        else
        {
            if (mCptUpgrade == PlayerUpgrade.CurrentPlayerUpgradeTypes.Count)
            {
                GUI.Box(new Rect(widthThrowShuriken * 0.7f, 0, widthThrowShuriken * 0.3f, heightThrowShuriken), mCantUpgrade); // Peut pas upgradé
            }
            else if (GUI.Button(new Rect(widthThrowShuriken * 0.7f, 0, widthThrowShuriken * 0.3f, heightThrowShuriken), "UPGRADE"))
            {
                mPlayerUpgrades.AddUpgrade(PlayerUpgradeTypes.CanDodge); //Upgrade
                SwitchState(State.MovingState);
            }
        }
        GUI.EndGroup();
        GUI.EndGroup();

        GUI.BeginGroup(new Rect(0, heightThrowShuriken, widthThrowShuriken, heightThrowShuriken));
        GUI.Box(new Rect(widthThrowShuriken * 0.3f, 0, widthThrowShuriken * 0.4f, heightThrowShuriken), mDodgeFaster); // Texte

        if (PlayerUpgrade.CurrentPlayerUpgradeTypes.Contains(PlayerUpgradeTypes.DodgeDuration))
        {
            GUI.Box(new Rect(widthThrowShuriken * 0.7f, 0, widthThrowShuriken * 0.3f, heightThrowShuriken), mAlreadyUpgrade); // Déjà upgradé
        }
        else
        {
            if (PlayerUpgrade.CurrentPlayerUpgradeTypes.Contains(PlayerUpgradeTypes.CanDodge) == false || mCptUpgrade == PlayerUpgrade.CurrentPlayerUpgradeTypes.Count)
            {
                GUI.Box(new Rect(widthThrowShuriken * 0.7f, 0, widthThrowShuriken * 0.3f, heightThrowShuriken), mCantUpgrade); // Peut pas upgradé
            }
            else if (GUI.Button(new Rect(widthThrowShuriken * 0.7f, 0, widthThrowShuriken * 0.3f, heightThrowShuriken), "UPGRADE"))
            {
                mPlayerUpgrades.AddUpgrade(PlayerUpgradeTypes.DodgeDuration); //Upgrade
                SwitchState(State.MovingState);
            }
        }
        GUI.EndGroup();

        GUI.BeginGroup(new Rect(0, heightThrowShuriken + heightThrowShuriken, widthThrowShuriken, heightThrowShuriken));
        GUI.Box(new Rect(widthThrowShuriken * 0.3f, 0, widthThrowShuriken * 0.4f, heightThrowShuriken), mDodgeAttackCounter); // Texte
        if (PlayerUpgrade.CurrentPlayerUpgradeTypes.Contains(PlayerUpgradeTypes.DodgeAttackReturn))
        {
            GUI.Box(new Rect(widthThrowShuriken * 0.7f, 0, widthThrowShuriken * 0.3f, heightThrowShuriken), mAlreadyUpgrade); // Déjà upgradé
        }
        else
        {
            if (PlayerUpgrade.CurrentPlayerUpgradeTypes.Contains(PlayerUpgradeTypes.DodgeDuration) == false || mCptUpgrade == PlayerUpgrade.CurrentPlayerUpgradeTypes.Count)
            {
                GUI.Box(new Rect(widthThrowShuriken * 0.7f, 0, widthThrowShuriken * 0.3f, heightThrowShuriken), mCantUpgrade); // Peut pas upgradé
            }
            else if (GUI.Button(new Rect(widthThrowShuriken * 0.7f, 0, widthThrowShuriken * 0.3f, heightThrowShuriken), "UPGRADE"))
            {
                mPlayerUpgrades.AddUpgrade(PlayerUpgradeTypes.DodgeAttackReturn); //Upgrade
                SwitchState(State.MovingState);
            }
        }

        GUI.EndGroup();
        GUI.EndGroup();
    }

    private void JumpUpgradeBox(float upgradeBoxHeight, float upgradeBoxWidth)
    {
        float widthThrowShuriken = upgradeBoxWidth;
        float heightThrowShuriken = upgradeBoxHeight * 0.1f;
        GUI.BeginGroup(new Rect(0, height * 0.44f, upgradeBoxWidth, upgradeBoxHeight));

        GUI.Box(new Rect(0, 0, widthThrowShuriken * 0.3f, heightThrowShuriken * 3), mJumpType); // Image
            GUI.BeginGroup(new Rect(0, 0, upgradeBoxWidth, upgradeBoxHeight));
                GUI.BeginGroup(new Rect(0, 0, widthThrowShuriken, heightThrowShuriken));
                    GUI.Box(new Rect(widthThrowShuriken * 0.3f, 0, widthThrowShuriken * 0.4f, heightThrowShuriken), mJump); // Texte
                    if (PlayerUpgrade.CurrentPlayerUpgradeTypes.Contains(PlayerUpgradeTypes.CanJump))
                    {
                        GUI.Box(new Rect(widthThrowShuriken * 0.7f, 0, widthThrowShuriken * 0.3f, heightThrowShuriken), mAlreadyUpgrade); // Déjà prit par défaut
                    }
                    else
                    {
                        if(mCptUpgrade == PlayerUpgrade.CurrentPlayerUpgradeTypes.Count)
                        {
                            GUI.Box(new Rect(widthThrowShuriken * 0.7f, 0, widthThrowShuriken * 0.3f, heightThrowShuriken), mCantUpgrade); // Peut pas upgradé
                        }
                        else if (GUI.Button(new Rect(widthThrowShuriken * 0.7f, 0, widthThrowShuriken * 0.3f, heightThrowShuriken), "UPGRADE"))
                        {
                            mPlayerUpgrades.AddUpgrade(PlayerUpgradeTypes.CanJump); //Upgrade
                            SwitchState(State.MovingState);
                        }
                    }
                GUI.EndGroup();
            GUI.EndGroup();

            GUI.BeginGroup(new Rect(0, heightThrowShuriken, widthThrowShuriken, heightThrowShuriken));
            //GUI.Box(new Rect(0, 0, widthThrowShuriken * 0.3f, heightThrowShuriken), mJumpType); // Image
            GUI.Box(new Rect(widthThrowShuriken * 0.3f, 0, widthThrowShuriken * 0.4f, heightThrowShuriken), mJumpHigherImg); // Texte

            if (PlayerUpgrade.CurrentPlayerUpgradeTypes.Contains(PlayerUpgradeTypes.JumpHigher))
            {
                GUI.Box(new Rect(widthThrowShuriken * 0.7f, 0, widthThrowShuriken * 0.3f, heightThrowShuriken), mAlreadyUpgrade); // Déjà upgradé
            }
            else
            {
                if (PlayerUpgrade.CurrentPlayerUpgradeTypes.Contains(PlayerUpgradeTypes.CanJump) == false || mCptUpgrade == PlayerUpgrade.CurrentPlayerUpgradeTypes.Count)
                {
                    GUI.Box(new Rect(widthThrowShuriken * 0.7f, 0, widthThrowShuriken * 0.3f, heightThrowShuriken), mCantUpgrade); // Peut pas upgradé
                }
                else if (GUI.Button(new Rect(widthThrowShuriken * 0.7f, 0, widthThrowShuriken * 0.3f, heightThrowShuriken), "UPGRADE"))
                {
                    mPlayerUpgrades.AddUpgrade(PlayerUpgradeTypes.JumpHigher); //Upgrade
                    SwitchState(State.MovingState);
                }
            }
            GUI.EndGroup();

            GUI.BeginGroup(new Rect(0, heightThrowShuriken + heightThrowShuriken, widthThrowShuriken, heightThrowShuriken));
            //GUI.Box(new Rect(0, 0, widthThrowShuriken * 0.3f, heightThrowShuriken), mJumpType); // Image
            GUI.Box(new Rect(widthThrowShuriken * 0.3f, 0, widthThrowShuriken * 0.4f, heightThrowShuriken), mJumpFasterImg); // Texte
            if (PlayerUpgrade.CurrentPlayerUpgradeTypes.Contains(PlayerUpgradeTypes.JumpFaster))
            {
                GUI.Box(new Rect(widthThrowShuriken * 0.7f, 0, widthThrowShuriken * 0.3f, heightThrowShuriken), mAlreadyUpgrade); // Déjà upgradé
            }
            else
            {
                if (PlayerUpgrade.CurrentPlayerUpgradeTypes.Contains(PlayerUpgradeTypes.JumpHigher) == false || mCptUpgrade == PlayerUpgrade.CurrentPlayerUpgradeTypes.Count)
                {
                    GUI.Box(new Rect(widthThrowShuriken * 0.7f, 0, widthThrowShuriken * 0.3f, heightThrowShuriken), mCantUpgrade); // Peut pas upgradé
                }
                else if (GUI.Button(new Rect(widthThrowShuriken * 0.7f, 0, widthThrowShuriken * 0.3f, heightThrowShuriken), "UPGRADE"))
                {
                    mPlayerUpgrades.AddUpgrade(PlayerUpgradeTypes.JumpFaster); //Upgrade
                    SwitchState(State.MovingState);
                }
            }

            GUI.EndGroup();
        GUI.EndGroup();
    }

    private void ShurikenUpgradeBox(float upgradeBoxHeight, float upgradeBoxWidth)
    {
        float widthThrowShuriken = upgradeBoxWidth;
        float heightThrowShuriken = upgradeBoxHeight * 0.1f;
        GUI.BeginGroup(new Rect(0, height * 0.20f, upgradeBoxWidth, upgradeBoxHeight));
        GUI.Box(new Rect(0, 0, widthThrowShuriken * 0.3f, heightThrowShuriken * 3), mShurikenType); // Image

            GUI.BeginGroup(new Rect(0, 0,widthThrowShuriken,heightThrowShuriken));
                GUI.Box(new Rect(widthThrowShuriken * 0.3f,0,widthThrowShuriken * 0.4f,heightThrowShuriken), mShurikenThrowCan); 
                GUI.Box(new Rect(widthThrowShuriken * 0.7f, 0, widthThrowShuriken * 0.3f, heightThrowShuriken), mAlreadyUpgrade); // Déjà prit par défaut
            GUI.EndGroup();

            GUI.BeginGroup(new Rect(0, heightThrowShuriken, widthThrowShuriken, heightThrowShuriken));
                GUI.Box(new Rect(widthThrowShuriken * 0.3f, 0, widthThrowShuriken * 0.4f, heightThrowShuriken), mShurikenOfNumber); // Texte

                if (PlayerUpgrade.CurrentPlayerUpgradeTypes.Contains(PlayerUpgradeTypes.ShurikenNumber))
                {
                    GUI.Box(new Rect(widthThrowShuriken * 0.7f, 0, widthThrowShuriken * 0.3f, heightThrowShuriken), mAlreadyUpgrade); // Déjà upgradé
                }
                else
                {
                    if (PlayerUpgrade.CurrentPlayerUpgradeTypes.Contains(PlayerUpgradeTypes.CanDodge) == false && PlayerUpgrade.CurrentPlayerUpgradeTypes.Contains(PlayerUpgradeTypes.CanJump) == false)
                    {
                        GUI.Box(new Rect(widthThrowShuriken * 0.7f, 0, widthThrowShuriken * 0.3f, heightThrowShuriken), mCantUpgrade); // pas upgradeable
                    }
                    else if (GUI.Button(new Rect(widthThrowShuriken * 0.7f, 0, widthThrowShuriken * 0.3f, heightThrowShuriken),"UPGRADE"))
                    {
                        mPlayerUpgrades.AddUpgrade(PlayerUpgradeTypes.ShurikenNumber); //Upgrade
                        SwitchState(State.MovingState);
                    }
                }
            GUI.EndGroup();

            GUI.BeginGroup(new Rect(0, heightThrowShuriken + heightThrowShuriken, widthThrowShuriken, heightThrowShuriken));
                GUI.Box(new Rect(widthThrowShuriken * 0.3f, 0, widthThrowShuriken * 0.4f, heightThrowShuriken), mShurikenSpeedImg); // Texte
                if (PlayerUpgrade.CurrentPlayerUpgradeTypes.Contains(PlayerUpgradeTypes.SkurikenSpeed))
                {
                    GUI.Box(new Rect(widthThrowShuriken * 0.7f, 0, widthThrowShuriken * 0.3f, heightThrowShuriken), mAlreadyUpgrade); // Déjà upgradé
                }
                else
                {
                    if (PlayerUpgrade.CurrentPlayerUpgradeTypes.Contains(PlayerUpgradeTypes.ShurikenNumber) == false || mCptUpgrade == PlayerUpgrade.CurrentPlayerUpgradeTypes.Count)
                    {
                        GUI.Box(new Rect(widthThrowShuriken * 0.7f, 0, widthThrowShuriken * 0.3f, heightThrowShuriken), mCantUpgrade); // Peut pas upgradé
                    }
                    else if (GUI.Button(new Rect(widthThrowShuriken * 0.7f, 0, widthThrowShuriken * 0.3f, heightThrowShuriken),"UPGRADE"))
                    {
                        mPlayerUpgrades.AddUpgrade(PlayerUpgradeTypes.SkurikenSpeed); //Upgrade
                        SwitchState(State.MovingState);
                    }
                }
                       
            GUI.EndGroup();
        GUI.EndGroup();
    }

    protected override void ExitState()
    {
    }
}
