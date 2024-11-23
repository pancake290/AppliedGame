using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cockroach_Adult : EnemyTurnLogic, IBreeding
{
    private EnemyManager enemyManager;
    public override string enemyType => "Cockroach_Adult";
    public GameObject cockroachEgg;
    public bool IsBreeding { get; set; } = false;

    private void Start()
    {
        enemyManager = this.GetComponent<EnemyManager>();
    }
    public override void PerformMove()
    {
        //蟑螂移动房间
        RoomUnit targetRoom = RoomSelector.GetRoomWithMostTargetItem(enemyManager, "Trash");
        if (targetRoom == enemyManager.currentRoom) return;
        enemyManager.MoveToRoom(targetRoom);
    }

    public override void PerformAttack()
    {
        //蟑螂不攻击其它生物
    }

    public override void PerformReproduce()
    {
        
        //繁殖逻辑
        if (IsBreeding == false)
        {
            foreach (var enemy in enemyManager.currentRoom.enemiesInRoom)
            {
                if (enemy.enemyType != enemyType) continue;
                if (enemy.turnLogic is IBreeding trueEnemy)
                {
                    if (enemy == enemyManager) continue;
                    if (trueEnemy.IsBreeding == false)//繁殖
                    {
                        trueEnemy.IsBreeding = true;
                        IsBreeding = true;
                        Instantiate(cockroachEgg, transform.position, transform.rotation);
                        return;
                    }
                    else continue; // 找到后立即返回
                }
            }
        }
    }
    public override void PerformEndTurn()
    {
        IsBreeding = false;
    }
}

public interface IBreeding
{
    bool IsBreeding { get; set; }
}
