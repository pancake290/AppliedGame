using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider_Egg : EnemyTurnLogic
{
    private EnemyManager enemyManager;
    public override string enemyType => "Spider_Egg";
    public GameObject spiderAdult;

    private int incubateCD = 1;

    private void Start()
    {
        enemyManager = this.GetComponent<EnemyManager>();
    }
    public override void PerformMove()
    {
        //蜘蛛蛋不动
    }

    public override void PerformAttack()
    {
        //蜘蛛蛋不攻击
        
    }

    public override void PerformReproduce()
    {
        //蜘蛛蛋每回合cd-1
        incubateCD -= 1;
        if(incubateCD <= 0)
        {
            Instantiate(spiderAdult, transform.position, transform.rotation);
            enemyManager.GoDie();
        }
    }
}
