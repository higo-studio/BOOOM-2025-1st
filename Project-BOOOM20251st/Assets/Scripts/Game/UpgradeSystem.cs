using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BugData
{
    public float MoveSpeed;
    public int HP;
    public int CarryNum;
}

public class UpgradeSystem: MonoBehaviour
{
    [SerializeField]
    public BugData bugData;
}