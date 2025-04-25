using UnityEngine;
using System.Collections.Generic;


public enum BugState
{
    // 自由态、朝目标点行进状态、农田工作中、探险中
    FREE, TO_TARGET, WORKING, EXPLORING
}

public class BugUnit : MonoBehaviour
{
    public BugState state { get; private set; }
    public int HP { get; private set; }
    
    public HashSet<FoodUnit> carryingFoodList = new();
    private System.Object target;

    void Awake()
    {
        state = BugState.FREE;
        HP = BugSystem.Instance.OriginHP;
        BugSystem.Instance.AddBug(this);
    }

    void OnDestroy()
    {
        if (BugSystem.BeenCleaned)
            return;
        BugSystem.Instance.RemoveBug(this);
    }

    public bool CanCarryMore 
    {
        get 
        {
            if (state == BugState.FREE)
                return true;
            if (state == BugState.WORKING || state == BugState.EXPLORING)
                return false;
            // TO_TARGET的处理
            if (target is not Queen && target is not FoodUnit)
                return false;
            int carryingCount = target is FoodUnit ? carryingFoodList.Count + 1 : carryingFoodList.Count;
            return carryingCount < BugSystem.Instance.CarryNum;
        }
    }

    public void CarryFood(FoodUnit food)
    {

    }

    public void SetTarget(System.Object target, bool stillFree=false)
    {
        state = stillFree ? BugState.FREE : BugState.TO_TARGET;
        this.target = target;
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void Update() 
    {
        if (state == BugState.TO_TARGET || state == BugState.FREE)
            Move();
    }

    private void Move()
    {
        if (target is null)
            return;
        Vector3 targetPos;
        if (target is Vector3)
            targetPos = (Vector3)target;
        else
            targetPos = ((Component)target).transform.position;
        Debug.Log(targetPos);

    }

}
