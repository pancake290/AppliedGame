using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static RoundManager;

public class InteractableManager : MonoBehaviour
{
    public List<RoomUnit> roomUnits;
    //public List<Enemy> enemies;

    private void Awake()
    {
        RoundManager.OnStateChanged += RoundManagerOnStateChanged;
    }
    private void OnDestroy()
    {
        RoundManager.OnStateChanged -= RoundManagerOnStateChanged;
    }
    private void RoundManagerOnStateChanged(RoundManager.GameState state)
    {
        if (state == RoundManager.GameState.EnemyTurn)
        {
            LockMouse();
            PerformEnemyActions();
        }

        if (state == RoundManager.GameState.PlayerTurn)
        {
            UnlockMouse();
        }
    }
    public void PerformEnemyActions()
      {
        foreach (RoomUnit room in roomUnits) 
        {
            room.UpdateList();
            foreach (EnemyManager enemy in room.enemiesInRoom)
            {
                enemy.turnLogic.PerformMove();
                //await Task.Delay(50);
                enemy.turnLogic.PerformReproduce();
                //await Task.Delay(50);
                enemy.turnLogic.PerformAttack();
                //await Task.Delay(50);
                enemy.turnLogic.PerformEndTurn();
                //await Task.Delay(50);
            }
        }
        RoundManager.Instance.UpdateGameState(GameState.PlayerTurn);

    }

    private void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked; // 锁定鼠标
        Cursor.visible = false; // 隐藏光标
    }
    private void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None; // 解除锁定
        Cursor.visible = true; // 显示光标
    }
}
