using UnityEngine;

[CreateAssetMenu(fileName = "QueenData", menuName = "Scriptable Objects/QueenData")]
public class QueenData : ScriptableObject
{
    public int HP;
    public int[] PerDayCost;
    public int SpawnBugCost;
}
