using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class RhythmSequenceManager
{
    public enum State
    {
        Fail,
        Success,
        OnGoingSequence,
        NoSequence,
        WaitingForActionToEnd
    } ;
    
    private List<RhythmSequence> mRhythmSequences;

    private float mCooldownTime;
    private State mState;

    private int mIndexSequence = -1;
    private int mLastSequence = -1;
    private int mFailSequenceIndex = -1;

    private Texture2D BlackTexture;
    private Texture2D RedTexture;
    private Texture2D GreenTexture;
    private const int ArrowSpace = 22;

    float mTotalTimeElapsed = 0.0f;

    public const int Attack = 0;
    public const int Jump = 1;
    public const int Duck = 2;
    public const int Dodge = 3;

    public List<RhythmSequence> RhythmSequences
    {
        get { return mRhythmSequences; }
        set { mRhythmSequences = value; }
    }
    
    public RhythmSequence AttackSequence
    {
        get { return RhythmSequences[Attack]; }
        set { RhythmSequences[Attack] = value; }
    }

    public RhythmSequence JumpSequence
    {
        get { return RhythmSequences[Jump]; }
        set { RhythmSequences[Jump] = value; }
    }

    public RhythmSequence DuckSequence
    {
        get { return RhythmSequences[Duck]; }
        set { RhythmSequences[Duck] = value; }
    }

    public RhythmSequence DodgeSequence
    {
        get { return RhythmSequences[Dodge]; }
        set { RhythmSequences[Dodge] = value; }
    }

    public RhythmSequence CurrentSequence
    {
        get
        {
            if (IndexSequence == -1)
            {
                return null;
            }
            return RhythmSequences[IndexSequence];
        }
        set { RhythmSequences[IndexSequence] = value; }
    }

    public int IndexSequence
    {
        get { return mIndexSequence; }
        set { mIndexSequence = value; }
    }

    public State CurrentState
    {
        get { return mState; }
        set { mState = value; }
    }

    public float CooldownTime
    {
        get { return mCooldownTime; }
        set { mCooldownTime = value; }
    }

    public RhythmSequenceManager()
    {
        InitializeTextures();
        InitializeRhythmSequences();
    }

    private void InitializeTextures()
    {
        GreenTexture = new Texture2D(1, 1);
        GreenTexture.SetPixel(0, 0, Color.green);
        GreenTexture.SetPixel(0, 1, Color.green);
        GreenTexture.SetPixel(1, 0, Color.green);
        GreenTexture.SetPixel(1, 1, Color.green);
        GreenTexture.Apply();

        RedTexture = new Texture2D(1, 1);
        RedTexture.SetPixel(0, 0, Color.red);
        RedTexture.SetPixel(0, 1, Color.red);
        RedTexture.SetPixel(1, 0, Color.red);
        RedTexture.SetPixel(1, 1, Color.red);
        RedTexture.Apply();

        BlackTexture = new Texture2D(1, 1);
        BlackTexture.SetPixel(0, 0, Color.black);
        BlackTexture.SetPixel(0, 1, Color.black);
        BlackTexture.SetPixel(1, 0, Color.black);
        BlackTexture.SetPixel(1, 1, Color.black);
        BlackTexture.Apply();
    }

    private void InitializeRhythmSequences()
    {
        RhythmSequenceFactory rhythmSequenceFactory = new RhythmSequenceFactory(); 
        mRhythmSequences = new List<RhythmSequence>(4);

        mRhythmSequences.Add(rhythmSequenceFactory.CreateAttackSequence());
        mRhythmSequences.Add(null);
        mRhythmSequences.Add(null);
        mRhythmSequences.Add(null);
    }

    public void UpdateRhythmSequenceManagerTimer()
    {
        if (CurrentSequence != null)
        {
            CurrentSequence.CurrentRhythm.UpdateElapsedTime();
        }

        if (CurrentState == State.Fail || CurrentState == State.WaitingForActionToEnd)
        { 
            CooldownTime += Time.deltaTime;

            if (CooldownTime > 0.75)
            {
                CurrentState = State.NoSequence;
                mIndexSequence = -1;
            }
        }

        mTotalTimeElapsed += Time.deltaTime;
    }

    public void UpdateRhythmSequenceManager () 
    {
        //if (IsTimed())
        //{
        //  //  mBeat.transform.Rotate(0, 0, 1);
        //}

        //mBeat.SetActive(IsTimed());
        if (CurrentState == State.NoSequence && CurrentSequence == null)
        {
            UpdateNoneSequence();
        }
        else if (CurrentState == State.OnGoingSequence)
        {
            if (IsTimed())
            {
                CurrentSequence.UpdateRhythmSequence();
            }

            if (CurrentSequence.HasFailed)
            {
                CurrentState = State.Fail;
                CooldownTime = 0;
                mLastSequence = IndexSequence;
                mFailSequenceIndex = CurrentSequence.RhythmIndex;
                CurrentSequence.IsComplete = false;
                CurrentSequence.RhythmIndex = 0;
                IndexSequence = -1;
            }
            else if (CurrentSequence.IsComplete)
            {
                CurrentState = State.Success;
                CooldownTime = 0;
                mLastSequence = IndexSequence;
            }
        }
	}

    private void UpdateNoneSequence()
    {
        if (IsTimed())
        {
            if (InputManager.GetKeyDownUp())
            {
                MessageManager.Instance.ChangeMessage(RhythmType.Up, 1.0f, false);
                SoundManager.Instance.Play(RhythmFactory.UpAudioClip);
                DetermineAndStartSequence(KeyCode.UpArrow);
            }
            else if (InputManager.GetKeyDownLeft())
            {
                MessageManager.Instance.ChangeMessage(RhythmType.Left, 1.0f, false);
                SoundManager.Instance.Play(RhythmFactory.LeftAudioClip);
                DetermineAndStartSequence(KeyCode.LeftArrow);
            }
            else if (InputManager.GetKeyDownRight())
            {
                MessageManager.Instance.ChangeMessage(RhythmType.Right, 1.0f, false);
                SoundManager.Instance.Play(RhythmFactory.RightAudioClip);
                DetermineAndStartSequence(KeyCode.RightArrow);
            }
            else if (InputManager.GetKeyDownDown())
            {
                MessageManager.Instance.ChangeMessage(RhythmType.Down, 1.0f, false);
                SoundManager.Instance.Play(RhythmFactory.DownAudioClip);
                DetermineAndStartSequence(KeyCode.DownArrow);
            }
        }
    }


    bool IsTimed()
    {
        int timeElapsed = Mathf.FloorToInt((mTotalTimeElapsed / 0.1f) / 0.1f);
        bool isTimed = CheckValideTime(timeElapsed);
        return isTimed;// || CheckValideTime(timeElapsed2));
    }

    bool CheckValideTime(int timeElapsed)
    {
        string strTime = timeElapsed.ToString();
        if (strTime.Length < 3)
            return false;
        int trueTime = int.Parse(strTime.Substring(strTime.Length - 2, 2));

        return (((50 - 16) <= trueTime && (50 + 16) >= trueTime) ||
            (trueTime <= 16) || (trueTime >= 100 - 20));
        //string strTime = timeElapsed.ToString();

        //return (!strTime.EndsWith("7"));
        //return (strTime.EndsWith("4") || strTime.EndsWith("5") || strTime.EndsWith("6") ||
        //    strTime.EndsWith("9") || strTime.EndsWith("0") || strTime.EndsWith("1"));
    }

    private void DetermineAndStartSequence(KeyCode pKeyCode)
    {
        int cptSequence;
        for (cptSequence = 0; cptSequence < RhythmSequences.Count; cptSequence++)
        {
            if (mRhythmSequences[cptSequence] != null && mRhythmSequences[cptSequence].GetFirstRhythmKeyCode() == pKeyCode)
            {
                IndexSequence = cptSequence;
                CurrentSequence.StartRhythmSequence();
                CurrentState = State.OnGoingSequence;
                break;
            }
        }
    }

    public void UpdateGUI()
    {
        //PUT GUI
        if (CurrentState == State.Fail && mLastSequence != -1)
        {
            float TotalWidth = (50 + (mRhythmSequences[mLastSequence].Rhythms.Count * ArrowSpace));
            GUI.BeginGroup(new Rect(Screen.width - TotalWidth, Screen.height-30, TotalWidth, 30));

            //FAIL MESSAGE
            Texture2D currentTexture = GUI.skin.box.normal.background;
            
            GUI.skin.box.normal.background = BlackTexture;
            string sequenceName = GetSequenceName(mLastSequence);

            int currentWidth = 50;
            GUI.Box(new Rect(0, 0, currentWidth, 30), sequenceName);

            int cptRhythm = 0;

            foreach (Rhythm rhythm in mRhythmSequences[mLastSequence].Rhythms)
            {
                rhythm.GetRhythmArrow();

                if (mFailSequenceIndex == cptRhythm)
                {
                    GUI.skin.box.normal.background = RedTexture;
                }
                else if (mFailSequenceIndex < cptRhythm)
                {
                    GUI.skin.box.normal.background = BlackTexture;
                }
                else
                {
                    GUI.skin.box.normal.textColor = Color.black;
                    GUI.skin.box.normal.background = GreenTexture;
                }

                GUI.Box(new Rect(currentWidth, 0, ArrowSpace, 30), rhythm.GetRhythmArrow());
                GUI.skin.box.normal.textColor = Color.white;

                currentWidth += ArrowSpace;
                cptRhythm++;
            }
            GUI.skin.box.normal.background = currentTexture;
            GUI.EndGroup();
        }
        else if (CurrentState == State.NoSequence)
        {
        }
        else if (CurrentState == State.Success || CurrentState == State.WaitingForActionToEnd)
        {
            float TotalWidth = (50 + (mRhythmSequences[mLastSequence].Rhythms.Count * ArrowSpace));
            GUI.BeginGroup(new Rect(Screen.width - TotalWidth, Screen.height - 30, TotalWidth, 30));
            
            Texture2D currentTexture = GUI.skin.box.normal.background;
            
            GUI.skin.box.normal.background = BlackTexture;
            string sequenceName = GetSequenceName(mLastSequence);

            int currentWidth = 50;
            GUI.Box(new Rect(0, 0, currentWidth, 30), sequenceName);

            int cptRhythm = 0;

            foreach (Rhythm rhythm in mRhythmSequences[mLastSequence].Rhythms)
            {
                rhythm.GetRhythmArrow();

                GUI.skin.box.normal.textColor = Color.black;
                GUI.skin.box.normal.background = GreenTexture;

                GUI.Box(new Rect(currentWidth, 0, ArrowSpace, 30), rhythm.GetRhythmArrow());
                GUI.skin.box.normal.textColor = Color.white;

                currentWidth += ArrowSpace;
                cptRhythm++;
            }
            GUI.skin.box.normal.background = currentTexture;
            GUI.EndGroup();
        }
        else if (CurrentState == State.OnGoingSequence)
        {

            float TotalWidth = 50 + (CurrentSequence.Rhythms.Count * ArrowSpace);
            GUI.BeginGroup(new Rect(Screen.width - TotalWidth, Screen.height - 30, TotalWidth, 30));
            
            Texture2D currentTexture = GUI.skin.box.normal.background;

            GUI.skin.box.normal.background = BlackTexture;
            string sequenceName = GetSequenceName(mIndexSequence);

            int currentWidth = 50;
            GUI.Box(new Rect(0, 0, currentWidth, 30), sequenceName);

            int cptRhythm = 0;

            foreach (Rhythm rhythm in CurrentSequence.Rhythms)
            {
                rhythm.GetRhythmArrow();

                if (CurrentSequence.RhythmIndex > cptRhythm || rhythm.IsRhythmSuccess())
                {
                    GUI.skin.box.normal.textColor = Color.black;
                    GUI.skin.box.normal.background = GreenTexture;
                }
                else
                {
                    GUI.skin.box.normal.background = BlackTexture;
                }

                GUI.Box(new Rect(currentWidth, 0, ArrowSpace, 30), rhythm.GetRhythmArrow());
                GUI.skin.box.normal.textColor = Color.white;

                currentWidth += ArrowSpace;
                cptRhythm++;
            }
            GUI.skin.box.normal.background = currentTexture;
            GUI.EndGroup();
        }
    }

    private string GetSequenceName(int pIndex)
    {
        string sequenceName = "";
        if (pIndex == 0)
        {
            sequenceName = "Attack";
        }
        else if (pIndex == 1)
        {
            sequenceName = "Jump";
        }
        else if (pIndex == 2)
        {
            sequenceName = "Katana";
        }
        else if (pIndex == 3)
        {
            sequenceName = "Dodge";
        }
        return sequenceName;
    }
}
