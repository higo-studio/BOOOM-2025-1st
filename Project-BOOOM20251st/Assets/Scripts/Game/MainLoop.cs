using UnityEngine;
using DG.Tweening;
using System;
using JSAM;
using System.Collections;
using System.Collections.Generic;

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
    
    [Header("背景音乐")]
    public MainAudioLibraryMusic bgMusic;

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

        // 启动协程等待 AudioManager 初始化完成
        StartCoroutine(WaitForAudioManagerInit());
    }

    IEnumerator WaitForAudioManagerInit()
    {
        while (AudioManager.Instance == null || !AudioManager.Instance.Initialized)
        {
            yield return null;
        }

        try
        {
            Debug.Log($"尝试播放音乐: {bgMusic}");
            // 播放背景音乐
            AudioManager.PlayMusic(bgMusic);
            Debug.Log($"成功播放音乐: {bgMusic}");
        }
        catch (KeyNotFoundException ex)
        {
            Debug.LogError($"未找到 {bgMusic} 对应的音乐文件，请检查 JSAM 配置。错误信息: {ex.Message}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"播放音乐时发生未知错误: {ex.Message}");
        }
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
