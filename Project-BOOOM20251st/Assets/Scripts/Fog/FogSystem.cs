using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityCommunity.UnitySingleton;

public class FogSystem : MonoSingleton<FogSystem>
{
    [SerializeField]
    private int SizeOfTerrain = 10;
    [SerializeField]
    private short Width = 10;
    [SerializeField]
    private short Height = 10;


    /// <summary>
    /// 记录逻辑网格
    /// </summary>
    private short[,] Grid;


    protected override void OnInitializing()
    {
        Grid = new short[Height, Width];
    }

    protected override void OnInitialized()
    {
    }

    public override void ClearSingleton()
    {
    }

    public void Step()
    {
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                if (Grid[i, j] < 0)
                    Grid[i, j] = 0;
            }
        }
    }
    


}
