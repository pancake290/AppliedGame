using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RoundManager;

public class ChangeState : MonoBehaviour
{
    public void ChangeStateToPlayerTurn() 
    {
        RoundManager.Instance.UpdateGameState(GameState.EnemyTurn);
    }
}
