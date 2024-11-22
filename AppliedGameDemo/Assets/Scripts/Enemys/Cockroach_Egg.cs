using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cockroach_Egg : EnemyTurnLogic
{
    private EnemyManager enemyManager;
    public override string enemyType => "Cockroach_Egg";
    public GameObject cockroachAdult;

    private int incubateCD = 1;

    private void Start()
    {
        enemyManager = this.GetComponent<EnemyManager>();
    }
    public override void PerformMove()
    {
        //蛋不动
    }

    public override void PerformAttack()
    {
        //蛋不攻击
        
    }

    public override void PerformReproduce()
    {
        //蛋每回合cd-1
        incubateCD -= 1;
        if(incubateCD <= 0)
        {
            Instantiate(cockroachAdult, transform.position, transform.rotation);
            enemyManager.GoDie();
        }
    }
}
