using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public enum BugState
{
    // 自由态、朝目标点行进状态、农田工作中、探险中
    FREE, TO_TARGET, WORKING, EXPLORING
}

public class BugUnit : MonoBehaviour
{
    public BugState state { get; private set; }
    public int HP { get; private set; }
    
    public List<FoodUnit> carryingFoodList = new();

    [SerializeField]
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
            if (target is not Queen)
                return false;
            int carryingCount = target is FoodUnit ? carryingFoodList.Count + 1 : carryingFoodList.Count;
            return carryingCount < BugSystem.Instance.CarryNum;
        }
    }

    public void CarryFood(FoodUnit food)
    {
        carryingFoodList.Add(food);
        FoodSystem.Instance.CarryFood(food);
        target = Queen.Instance;
    }

    public void PutFood()
    {
        foreach (var food in carryingFoodList)
        {
            FoodSystem.Instance.StorageFood(food);
        }
        carryingFoodList.Clear();
        target = null;
        state = BugState.FREE;
    }

    public void SetTarget(System.Object target, bool stillFree=false)
    {
        state = stillFree ? BugState.FREE : BugState.TO_TARGET;
        this.target = target;
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckTrigger(other);
    }

    private void OnTriggerStay(Collider other)
    {
        CheckTrigger(other);
    }

    private void CheckTrigger(Collider other)
    {
        if (target is Queen && other.gameObject == ((Queen)target).gameObject)
        {
            // 放食物
            PutFood();
        }
        else if (target is FoodUnit && other.gameObject == ((FoodUnit)target).gameObject)
        {
            // 拿食物
            CarryFood((FoodUnit)target);
        }
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
        // 暂时移动只考虑x轴
        var moveDelta = Time.deltaTime * BugSystem.Instance.MoveSpeed * (targetPos.x - transform.position.x) / Mathf.Abs(targetPos.x - transform.position.x);
        transform.Translate(new Vector3(moveDelta, 0, 0), Space.World);
        // 更新背上的食物位置
        for (int i = 0; i < carryingFoodList.Count; i++)
        {
            carryingFoodList[i].transform.position = transform.position + new Vector3(0, 1 + 2 * i, 0);
        }

    }

}
