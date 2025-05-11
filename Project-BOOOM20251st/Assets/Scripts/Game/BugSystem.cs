using UnityEngine;
using UnityCommunity.UnitySingleton;
using System.Collections.Generic;
using System;
using System.Linq;

public class BugSystem : MonoBehaviour
{
    public GameObject bugPrefab;
    public int OriginHP { get { return MainLoop.Instance.UpgradeSystem.bugData.HP; } }
    public float MoveSpeed { get { return MainLoop.Instance.UpgradeSystem.bugData.MoveSpeed; } }
    public int CarryNum { get { return MainLoop.Instance.UpgradeSystem.bugData.CarryNum; } }

    private HashSet<BugUnit> bugs = new();

    public void AddBug(BugUnit bug)
    {
        bugs.Add(bug);
    }

    public void RemoveBug(BugUnit bug)
    {
        bugs.Remove(bug);
    }

    /// <summary>
    /// 创建新的虫子
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public void CreateNewBug(Vector3 position)
    {
        Instantiate(bugPrefab, position, Quaternion.identity, GameObject.Find("BugGroup").transform);
    }

    public void Decide()
    {
        DoCarry();
        DoFreeRandom();
    }

    private void DoCarry()
    {
        // 构建计算最优的搬运对象
        List<Tuple<float, BugUnit, FoodUnit>> distanceMap = new();
        foreach (var bug in bugs)
        {
            if (!bug.CanCarryMore)
                continue;
            foreach (var food in MainLoop.Instance.FoodSystem.StayingFoods)
            {
                if (food.BeingTargeted)
                    continue;
                float distanceQ = (bug.transform.position - food.transform.position).magnitude;
                distanceMap.Add(new(distanceQ, bug, food));
            }
        }
        distanceMap = distanceMap.OrderBy(x => x.Item1).ToList();
        foreach (var item in distanceMap)
        {
            var bug = item.Item2;
            var food = item.Item3;
            if (bug.CanCarryMore && !food.BeingTargeted)
            {
                food.BeingTargeted = true;
                bug.SetTarget(food);
            }
        }
    }

    private void DoFreeRandom()
    {

    }
}


