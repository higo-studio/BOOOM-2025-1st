using UnityEngine;
using DG.Tweening;
using System;

public class MainLoop : MonoBehaviour
{

    [Tooltip("当前回合数")]
    public int CurRound = 0;
    [Tooltip("每回合时间")]
    public float PerRoundTime = 120f;
    [Tooltip("当前回合时间")]
    public float CurRoundTIme = 0f;

    [Header("游戏场景定义")]
    [Header("最大回合数")]
    public int MaxRound = 15;
    public float TransitionTime = 1f;
    
    

    void Start()
    {
        // 初始化
        CurRound = 1;
        DOTween.Init();
        // FoodSystem.CreateInstance();
        // ResourceSystem.CreateInstance();
    }

    void FixedUpdate() 
    {
        CurRoundTIme += Time.fixedDeltaTime;
        if (CurRoundTIme >= PerRoundTime)
        {
            CurRoundTIme = 0f;
            CurRound++;
            NextRound();
        }
        // 测试先这么写
        BugSystem.Instance.Decide();
    }

    void Update()
    {
        
    }

    void NextRound()
    {
        // 结算血量、粮食等
        var queen = Queen.Instance;
        var foodSystem = FoodSystem.Instance;
        var roundCostFood = queen.PerDayCost[CurRound - 1];
        if (roundCostFood > foodSystem.FoodNum)
        {
            // 食物不足，扣血
            queen.HP -= 1;
            if (queen.HP <= 0)
            {
                GameOver();
                return;
            }
        }
        foodSystem.SpendFood(Math.Min(roundCostFood, foodSystem.FoodNum));
        // 切换、移动场景
    }

    void GameOver()
    {
        Debug.Log("Game Over");
    }
}
