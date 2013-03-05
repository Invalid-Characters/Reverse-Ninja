using UnityEngine;
using System.Collections;

public class RhythmFactory 
{
    public static string UpAudioClip = "chump"; //jump
    public static string DownAudioClip = "ting"; //duck
    public static string LeftAudioClip = "oudyaaa"; //attack
    public static string RightAudioClip = "hum"; //dodge


    public RhythmFactory()
    {
    }

    public Rhythm CreateUpRhythm(float pBeginTimer, float pEndTimer)
    {
        return new Rhythm(RhythmType.Up,pBeginTimer,pEndTimer,UpAudioClip);
    }

    public Rhythm CreateDownRhythm(float pBeginTimer, float pEndTimer)
    {
        return new Rhythm(RhythmType.Down, pBeginTimer, pEndTimer, DownAudioClip);
    }

    public Rhythm CreateLeftRhythm(float pBeginTimer, float pEndTimer)
    {
        return new Rhythm(RhythmType.Left, pBeginTimer, pEndTimer, LeftAudioClip);
    }

    public Rhythm CreateRightRhythm(float pBeginTimer, float pEndTimer)
    {
        return new Rhythm(RhythmType.Right, pBeginTimer, pEndTimer, RightAudioClip);
    }

	
}
