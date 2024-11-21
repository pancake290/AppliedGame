using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider_Adult : EnemyTurnLogic
{
    private EnemyManager enemyManager;
    public override string enemyType => "Spider_Adult";
    public GameObject spiderEgg;

    private float foodValue = 0;

    private void Start()
    {
        enemyManager = this.GetComponent<EnemyManager>();
    }
    public override void PerformMove()
    {
        //蜘蛛不主动移动房间
    }

    public override void PerformAttack()
    {
        //蜘蛛会吃蟑螂。或其它虫子？
        EnemyManager targetFood = enemyManager.FindEnemyinMyRoom(enemyManager,"Spider_Adult");
        Debug.Log(targetFood);
        if (targetFood)
        {
            targetFood.GoDie();
            foodValue += 1;
        }
        
    }

    public override void PerformReproduce()
    {
        //繁殖逻辑
        if(foodValue > 0)
        {
            foodValue -= 1;
            Instantiate(spiderEgg, transform.position, transform.rotation);
        }
    }
}
