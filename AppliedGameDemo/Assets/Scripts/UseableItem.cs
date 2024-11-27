using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseableItem : MonoBehaviour
{
    public string ItemType;

    public void Action(GameObject hit)
    {
        if(ItemType == "Pai")
        {
            
            EnemyManager enemyManager = hit.GetComponent<EnemyManager>();
            if(enemyManager != null)
            {
                
                if (TurnManager.Instance.actionPoints <= 0) return;

                enemyManager.PrepareGoDie();
                TurnManager.Instance.actionPoints -= 1;
                UIManager.Instance.UpdateEnergy();
            }
        }
    }
}
