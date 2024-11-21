using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyManager : MonoBehaviour
{
    public EnemyAIMovement movement;
    public EnemyTurnLogic turnLogic;

    public RoomUnit currentRoom;
    public string enemyType;

    public bool isPerformMove = false;
    public bool isPerformAttack = false;
    public bool isPerformReproduce = false;

    private void Update()
    {
        if (isPerformMove == true)
        {
            PerformTurnPhase(TurnPhase.Move);
            isPerformMove = false;
        }
        if (isPerformAttack == true)
        {
            PerformTurnPhase(TurnPhase.Attack);
            isPerformAttack = false;
        }
        if (isPerformReproduce == true)
        {
            PerformTurnPhase(TurnPhase.Reproduce);
            isPerformReproduce = false;
        }
    }

    void Awake()
    {
        // 获取所有需要的组件
        movement = GetComponent<EnemyAIMovement>();
        turnLogic = GetComponent<EnemyTurnLogic>();

        enemyType = turnLogic.enemyType;
    }

    private void OnTriggerEnter(Collider other)//如果敌人进到了房间范围，则把自身所处房间改为该房间
    {
        RoomUnit rooom = other.GetComponent<RoomUnit>();
        if (rooom != null)
        {
            this.currentRoom = rooom;
        }
    }

    public void EnableMovement()
    {
        if (movement == null) return;
        movement.EnableAI();
    }

    public void DisableMovement()
    {
        if (movement == null) return;
        movement.DisableAI();
    }

    public void PerformTurnPhase(TurnPhase phase)
    {
        switch (phase)
        {
            case TurnPhase.Move:
                turnLogic.PerformMove();
                break;
            case TurnPhase.Reproduce:
                turnLogic.PerformReproduce();
                break;
            case TurnPhase.Attack:
                turnLogic.PerformAttack();
                break;
        }
    }

    public void MoveToRoom(RoomUnit targetRoom)
    {
        if (targetRoom == currentRoom) return;
        movement.MoveToRoom(targetRoom);
    }
    public EnemyManager FindEnemyinMyRoom(EnemyManager mySelf, string type)//在自身所处房间里找第一个种类是type的enemy
    {
        foreach (var enemy in currentRoom.enemiesInRoom)
        {
            if (enemy.enemyType == type)
            {
                if (enemy == mySelf) continue;
                else return enemy; // 找到后立即返回
            }
        }
        return null; // 如果没有找到，返回 null
    }

    public void GoDie()
    {
        currentRoom.RemoveEnemy(this);
        Destroy(this.gameObject);
        //其它逻辑（创建一个死亡特效等？）
    }
}
