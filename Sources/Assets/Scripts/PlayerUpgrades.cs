using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public enum PlayerUpgradeTypes
{
    CanThrowShuriken = 0, ShurikenNumber = 1, SkurikenSpeed = 2,
    CanJump = 3, JumpHigher = 4, JumpFaster = 5, 
    CanDodge = 6, DodgeDuration=7, DodgeAttackReturn = 8,
    Katana = 9
} ;

public class PlayerUpgrades : MonoBehaviour
{
    private List<PlayerUpgradeTypes> mPlayerUpgradeTypes;

    private bool mNewUpgrade;

    public List<PlayerUpgradeTypes> CurrentPlayerUpgradeTypes
    {
        get { return mPlayerUpgradeTypes; }
        set { mPlayerUpgradeTypes = value; }
    }

    public PlayerUpgradeTypes LastUpgrade;

    public bool NewUpgrade
    {
        get { return mNewUpgrade; }
        set { mNewUpgrade = value; }
    }

    public void AddUpgrade(PlayerUpgradeTypes pPlayerUpgradeTypes)
    {
        LastUpgrade = pPlayerUpgradeTypes;
        mPlayerUpgradeTypes.Add(pPlayerUpgradeTypes);
        mNewUpgrade = true;
    }
    
    // Use this for initialization
	void Start () 
    {
        mPlayerUpgradeTypes = new List<PlayerUpgradeTypes>();
        mPlayerUpgradeTypes.Add(PlayerUpgradeTypes.CanThrowShuriken);
	}
}
