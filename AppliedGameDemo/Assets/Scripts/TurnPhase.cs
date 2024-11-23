using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurnPhase
{
    Move,//敌人移动
    Attack,//敌人攻击（主要是蜘蛛）
    Reproduce,//敌人生蛋
    EndTurn
}

public enum GamePhase
{
    PlayerPhase,
    EnemyPhase
}
