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
            }
        }
        RoundManager.Instance.UpdateGameState(GameState.PlayerTurn);

    }

    private void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked; // �������
        Cursor.visible = false; // ���ع��
    }
    private void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None; // �������
        Cursor.visible = true; // ��ʾ���
    }
}
