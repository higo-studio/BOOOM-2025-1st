using System.Collections.Generic;
using UnityEngine;
using UnityCommunity.UnitySingleton;

[System.Serializable]
public struct BugData
{
    public float MoveSpeed;
    public int HP;
    public int CarryNum;
}

public class UpgradeSystem: MonoSingleton<UpgradeSystem>
{
    [SerializeField]
    public BugData bugData;
}