using System.Collections.Generic;
using UnityEngine;
using UnityCommunity.UnitySingleton;
using System.Collections;
using System;

public class FoodSystem: MonoSingleton<FoodSystem>
{
    public int FoodNum { get; private set; } = 0;
    
    [Tooltip("随机生成食物的间隔时间")]public float RandomGenerateTime = 10f;
    [Tooltip("食物预制体")]public Transform FoodUnitPrefab;
    [Header("随机生成食物的范围")]
    public Vector2 GenerateRange = new Vector2(-40, 40);

    
    private Coroutine generateCoroutine;
    
    
    public HashSet<FoodUnit> StayingFoods = new HashSet<FoodUnit>();
    public HashSet<FoodUnit> CarryingFoods = new HashSet<FoodUnit>();

    protected override void OnInitializing()
    {
        StartGenerate();
    }

    public void JoinFood(FoodUnit unit)
    {
        CarryingFoods.Remove(unit);
        StayingFoods.Add(unit);
    }

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
    
    public void StartGenerate()
    {
        if (generateCoroutine != null)
            return;
        generateCoroutine = StartCoroutine(GenerateFoodRadom());
    }

    public void StopGenerate()
    {
        if (generateCoroutine == null)
            return;
        StopCoroutine(generateCoroutine);
        generateCoroutine = null;
    }

    public IEnumerator GenerateFoodRadom()
    {
        while(true)
        {
            var food = Instantiate(FoodUnitPrefab);
            food.parent = Floor.Instance.transform;
            var randomX = UnityEngine.Random.Range(GenerateRange.x, GenerateRange.y);
            // 获取地板的生成位置位置
            // 这里仅测试
            var position = Floor.Instance.transform.position;
            position.x = randomX;
            position.y += 5;
            food.position = position;
            yield return new WaitForSeconds(RandomGenerateTime);
        }
    }
}