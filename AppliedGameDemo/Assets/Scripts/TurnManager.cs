using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;
    public List<RoomUnit> rooms = new List<RoomUnit>();
    public RoomUnit destroyRoom;

    public int turnCount = 1;
    public int actionPoints = 2;

    public Stack<PickupAction> undoStack = new Stack<PickupAction>(); // 撤销操作栈
    public enum Phase { PlayerPhase, EnemyPhase }
    public Phase currentPhase;

    public bool anythingInHand = false;
    public GameObject goDiePoint;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            currentPhase = Phase.PlayerPhase;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnNextTurnButtonClicked()
    {
        //玩家回合结束
        actionPoints = 0;
        UIManager.Instance.UpdateEnergy();
        undoStack.Clear();

        List<EnemyManager> enemiesCopy = new List<EnemyManager>(destroyRoom.enemiesInRoom);
        foreach (EnemyManager enemy in enemiesCopy)
        {
            enemy.GoDie();
        }
        List<ItemManager> itemsCopy = new List<ItemManager>(destroyRoom.itemInRoom);
        foreach (ItemManager item in itemsCopy)
        {
            item.GoDie();
        }
        StartEnemyTurn();
    }

    public void OnUndoButtonClicked()
    {
        if(undoStack.Count == 0)
        {
            Debug.Log("没有可以撤回的操作");
            return;
        }

        PickupAction undoAction = undoStack.Pop();
        if(undoAction.isSetActiveFalse == true)
        {
            undoAction.Object.transform.position = undoAction.OriginalPosition;
            undoAction.Object.SetActive(true);
            actionPoints += 1;
            UIManager.Instance.UpdateEnergy();
            return;
        }

        undoAction.Object.transform.position = undoAction.OriginalPosition;
        EnemyAIMovement aImovement = undoAction.Object.GetComponent<EnemyAIMovement>();
        if (aImovement != null)
        {
            aImovement.DisableAI();
            aImovement.SetDestination(aImovement.gameObject.transform.position);
            aImovement.EnableAI();
        }
        actionPoints += 1;
        UIManager.Instance.UpdateEnergy();
    }

    public void StartEnemyTurn()
    {
        StartCoroutine(EnemyTurnSequence());
    }

    private IEnumerator EnemyTurnSequence()
    {
        currentPhase = Phase.EnemyPhase;
        UIManager.Instance.UpdatePhaseInfo(turnCount, currentPhase);

        // 敌人移动阶段
        yield return StartCoroutine(EnemyMovePhase());

        // 敌人攻击阶段
        yield return StartCoroutine(EnemyAttackPhase());

        // 敌人生蛋阶段
        yield return StartCoroutine(EnemyLayEggPhase());

        // 敌人回合结束阶段
        yield return StartCoroutine(EnemyEndTurnPhase());

        // 回合结束，进入玩家阶段
        actionPoints = 2;
        turnCount++;
        currentPhase = Phase.PlayerPhase;
        UIManager.Instance.UpdateEnergy();
        UIManager.Instance.UpdatePhaseInfo(turnCount, currentPhase);
        // 通知玩家可以行动
    }

    private IEnumerator EnemyMovePhase()
    {
        // 获取所有敌人
        List<EnemyManager> allEnemies = GetAllEnemies();

        // 开始移动所有敌人
        foreach (EnemyManager enemy in allEnemies)
        {
            if (enemy.movement != null)
            {
                enemy.turnLogic.PerformMove();
            }
        }

        // 等待所有敌人移动完成
        yield return new WaitUntil(() => AllEnemiesFinishedMoving(allEnemies));
    }

    private IEnumerator EnemyAttackPhase()
    {
        List<EnemyManager> allEnemies = GetAllEnemies();

        foreach (EnemyManager enemy in allEnemies)
        {
            enemy.turnLogic.PerformAttack();
        }

        // 如果需要等待攻击动画或特效完成，可以在这里加入等待逻辑
        yield return new WaitForSeconds(0.5f);
    }

    private IEnumerator EnemyLayEggPhase()
    {
        List<EnemyManager> allEnemies = GetAllEnemies();

        foreach (EnemyManager enemy in allEnemies)
        {
            enemy.turnLogic.PerformReproduce();
        }

        yield return new WaitForSeconds(0.5f);
    }

    private IEnumerator EnemyEndTurnPhase()
    {
        List<EnemyManager> allEnemies = GetAllEnemies();

        foreach (EnemyManager enemy in allEnemies)
        {
            enemy.turnLogic.PerformEndTurn();
        }

        yield return new WaitForSeconds(0.5f);
    }

    private List<EnemyManager> GetAllEnemies()
    {
        List<EnemyManager> allEnemies = new List<EnemyManager>();

        foreach (RoomUnit room in rooms)
        {
            allEnemies.AddRange(room.enemiesInRoom);
        }
        return allEnemies;
        
    }

    private bool AllEnemiesFinishedMoving(List<EnemyManager> enemies)
    {
        foreach (EnemyManager enemy in enemies)
        {
            if (enemy.movement != null && enemy.movement.isSpecialMoving)
            {
                return false;
            }
        }
        return true;
    }
}

public class PickupAction
{
    public GameObject Object;  // 被拾取的物体
    public RoomUnit OriginalRoom;  // 原始所在房间
    public Vector3 OriginalPosition; // 原始位置
    public bool isSetActiveFalse;//是不是给隐藏了
}
