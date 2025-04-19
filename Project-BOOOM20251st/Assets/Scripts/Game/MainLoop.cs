using UnityEngine;

public class MainLoop : MonoBehaviour
{

    [Tooltip("当前回合数")]
    public int CurRound = 0;
    [Tooltip("每回合时间")]
    public float PerRoundTime = 120f;
    [Tooltip("当前回合时间")]
    public float CurRoundTIme = 0f;

    void Start()
    {
        CurRound = 1;
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
    }

    void Update()
    {
        
    }

    void NextRound()
    {
        // 执行下一回合操作
        
    }
}
