using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyTurnLogic : MonoBehaviour
{
    public abstract string enemyType { get; }
    public virtual void PerformMove() { /* 默认移动逻辑 */ }
    public virtual void PerformReproduce() { /* 默认繁殖逻辑 */ }//繁殖通常是用来由自身产生另一个生物。比如从蛋里孵出来虫子或者虫子生下单
    public virtual void PerformAttack() { /* 默认攻击逻辑 */ }//理论上是类似于杀死其它生物的逻辑
    public virtual void PerformEndTurn() { /* 默认回合结束逻辑 */ }//回合结束的时候可能要把自己身上的一些状态重置等
}
