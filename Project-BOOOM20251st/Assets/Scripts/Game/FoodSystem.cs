using System.Collections.Generic;
using UnityEngine;
using UnityCommunity.UnitySingleton;
using System;

public class FoodSystem: Singleton<FoodSystem>
{
    public int FoodNum { get; private set; } = 0;
    
    
    private HashSet<FoodUnit> StayingFoods = new HashSet<FoodUnit>();
    private HashSet<FoodUnit> CarryingFoods = new HashSet<FoodUnit>();

    public void CarryFood(FoodUnit unit)
    {
        StayingFoods.Remove(unit);
        CarryingFoods.Add(unit);
    }

    public void StorageFood(FoodUnit unit)
    {
        CarryingFoods.Remove(unit);
        FoodNum += unit.ResourceNum;
    }
    
    public void SpendFood(int num)
    {
        FoodNum = Math.Max(0, FoodNum - num);
    }
    
}