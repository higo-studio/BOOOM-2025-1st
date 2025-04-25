using UnityEngine;
using DG.Tweening;
using System;

public class MainLoop : MonoBehaviour
{
    [Header("系统逻辑")]
    public BugSystem BugSystem;
    public FoodSystem FoodSystem;
    public Queen Queen;
    public ResourceSystem ResourceSystem;
    public UpgradeSystem UpgradeSystem;
    public Floor Floor;

    [Header("系统定义")]
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
    
    public static MainLoop Instance = null;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // 初始化
        CurRound = 1;
        DOTween.Init();
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
        BugSystem.Decide();
    }

    void Update()
    {
        
    }

    void NextRound()
    {
        // 结算血量、粮食等
        var roundCostFood = Queen.PerDayCost[CurRound - 1];
        if (roundCostFood > FoodSystem.FoodNum)
        {
            // 食物不足，扣血
            Queen.HP -= 1;
            if (Queen.HP <= 0)
            {
                GameOver();
                return;
            }
        }
        // 切换、移动场景
        FoodSystem.SpendFood(Math.Min(roundCostFood, FoodSystem.FoodNum));
    }

    void GameOver()
    {
        Debug.Log("Game Over");
    }
}
