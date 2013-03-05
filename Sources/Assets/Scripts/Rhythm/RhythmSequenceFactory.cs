using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RhythmSequenceFactory
{
    private RhythmFactory mRhythmFactory;
    private List<Rhythm> mRhythmList;
    
    public RhythmSequenceFactory()
    {
        mRhythmList = new List<Rhythm>();
        mRhythmFactory = new RhythmFactory();
    }

    private const float beginRhythm = 0.4f;
    private const float endRhythm = 1.5f;


	public RhythmSequence CreateAttackSequence()
    {
        mRhythmList = new List<Rhythm>();

        mRhythmList.Add(mRhythmFactory.CreateLeftRhythm(0.0f, 0.0f));
        mRhythmList.Add(mRhythmFactory.CreateRightRhythm(beginRhythm, endRhythm));
        mRhythmList.Add(mRhythmFactory.CreateLeftRhythm(beginRhythm, endRhythm));

	    return new RhythmSequence(mRhythmList);
    }

    public RhythmSequence CreateJumpSequence()
    {
        mRhythmList = new List<Rhythm>();

        mRhythmList.Add(mRhythmFactory.CreateUpRhythm(0.0f, 0.0f));
        mRhythmList.Add(mRhythmFactory.CreateDownRhythm(beginRhythm, endRhythm));
        mRhythmList.Add(mRhythmFactory.CreateUpRhythm(beginRhythm, endRhythm));

        return new RhythmSequence(mRhythmList);
    }

    public RhythmSequence CreateDodgeSequence()
    {
        mRhythmList = new List<Rhythm>();

        mRhythmList.Add(mRhythmFactory.CreateRightRhythm(0.0f, 0.0f));
        mRhythmList.Add(mRhythmFactory.CreateUpRhythm(beginRhythm, endRhythm));
        mRhythmList.Add(mRhythmFactory.CreateRightRhythm(beginRhythm, endRhythm));

        return new RhythmSequence(mRhythmList);
    }

    public RhythmSequence CreateDuckSequence()
    {
        mRhythmList = new List<Rhythm>();

        mRhythmList.Add(mRhythmFactory.CreateDownRhythm(0.0f, 0.0f));
        mRhythmList.Add(mRhythmFactory.CreateRightRhythm(beginRhythm, endRhythm));
        mRhythmList.Add(mRhythmFactory.CreateUpRhythm(beginRhythm, endRhythm));
        mRhythmList.Add(mRhythmFactory.CreateLeftRhythm(beginRhythm, endRhythm));

        return new RhythmSequence(mRhythmList);
    }
}
