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
    public bool randomMusic;

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

        if (randomMusic)
        {
            // 获取音乐库中所有音乐
            var allMusics = Enum.GetValues(typeof(MainAudioLibraryMusic)) as MainAudioLibraryMusic[];
            if (allMusics != null && allMusics.Length > 0)
            {
                // 随机选择一首音乐
                int randomIndex = UnityEngine.Random.Range(0, allMusics.Length);
                MainAudioLibraryMusic randomMusic = allMusics[randomIndex];
                AudioManager.PlayMusic(randomMusic);
                Debug.Log($"成功随机播放音乐: {randomMusic}");
            }
        }
        else
        {
            // 播放指定音乐
            AudioManager.PlayMusic(bgMusic);
            Debug.Log($"成功播放音乐: {bgMusic}");
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
        var roundCostFood = Queen.PerDayCost;
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
        // 扣除食物
        FoodSystem.SpendFood(Math.Min(roundCostFood, FoodSystem.FoodNum));
        // 生虫子
        for (int i = 0; i < Queen.PerDayGain; i++)
        {
            BugSystem.CreateNewBug(Queen.transform.position);
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over");
    }
}
