using UnityEngine;
using UnityCommunity.UnitySingleton;

public class Queen : MonoBehaviour
{
    [SerializeField]
    protected QueenData queenData;
    public int OriginHp => queenData.HP;
    public int PerDayCost => queenData.PerDayCost;
    public int PerDayGain => queenData.PerDaySpawnBug;
    public int HP;

    private void Start()
    {
        HP = OriginHp;
    }

}