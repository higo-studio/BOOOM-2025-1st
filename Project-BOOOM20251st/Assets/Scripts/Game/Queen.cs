using UnityEngine;
using UnityCommunity.UnitySingleton;

public class Queen: MonoSingleton<Queen>
{
    [SerializeField]
    protected QueenData queenData;
    public int OriginHp => queenData.HP;
    public int[] PerDayCost => queenData.PerDayCost;
    public int SpawnBugCost => queenData.SpawnBugCost;
    public int HP;

    protected override void OnInitialized() 
    {
        HP = OriginHp;
    }

}