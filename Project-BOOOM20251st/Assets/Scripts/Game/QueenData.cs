using UnityEngine;

[CreateAssetMenu(fileName = "QueenData", menuName = "Scriptable Objects/QueenData")]
public class QueenData : ScriptableObject
{
    public int HP = 3;
    public int PerDayCost = 1;
    public int PerDaySpawnBug = 1;
}
