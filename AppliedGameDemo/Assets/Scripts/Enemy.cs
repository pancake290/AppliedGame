using UnityEngine;
using Pathfinding; // 引用 A* Pathfinding 命名空间

public class Enemy : MonoBehaviour
{
    public RoomUnit currentRoom;

    private RichAI ai; // 引用 RichAI 组件
    private float idleTime; // 敌人下一次移动的等待时间
    public float minIdleTime = 1f;
    public float maxIdleTime = 3f;

    private bool isMoving = false;
    public bool isPickedUp = false;

    private float pickUpHeight = 2f; // 抓起时增加的高度
    private float originalY; // 记录敌人原本的 Y 轴高度

    private void Start()
    {
        ai = GetComponent<RichAI>();
        if (ai == null)
        {
            Debug.LogError("敌人缺少 RichAI 组件！");
        }
        idleTime = Random.Range(minIdleTime, maxIdleTime);
    }

    private void Update()
    {
        
            // 敌人在房间内随机移动的逻辑（保持不变）
            if (!isMoving && currentRoom != null)
            {
                idleTime -= Time.deltaTime;
                if (idleTime <= 0)
                {
                    MoveToRandomPointInRoom(currentRoom);
                    idleTime = Random.Range(minIdleTime, maxIdleTime);
                }
            }

            // 检查敌人是否到达目的地
            if (isMoving && ai.reachedEndOfPath && !ai.pathPending)
            {
                isMoving = false;
            }

    }

    private void OnTriggerEnter(Collider other)
    {
        RoomUnit rooom = other.GetComponent<RoomUnit>();
        if (rooom != null)
        {
            this.currentRoom = rooom;
        }
    }

    // 设置当前所在的房间
    public void SetCurrentRoom(RoomUnit room)
    {
        currentRoom = room;
    }

    // 在房间内移动到随机点
    private void MoveToRandomPointInRoom(RoomUnit room)
    {
        Vector3 targetPoint = GetRandomPointInBounds(room.roomBounds);
        ai.destination = targetPoint;
        ai.SearchPath();
        isMoving = true;
    }

    // 获取房间范围内的随机点
    private Vector3 GetRandomPointInBounds(Bounds bounds)
    {
        Vector3 min = bounds.min;
        Vector3 max = bounds.max;
        float x = Random.Range(min.x, max.x);
        float z = Random.Range(min.z, max.z);
        float y = transform.position.y; // 保持当前的 Y 轴位置
        return new Vector3(x, y, z);
    }

    // 在回合切换时移动到其他房间
    public void MoveToRoom(RoomUnit targetRoom)
    {
        if (targetRoom != null)
        {
            Vector3 targetPoint = GetRandomPointInBounds(targetRoom.roomBounds);
            ai.destination = targetPoint;
            ai.SearchPath();
            isMoving = true;
        }
    }

    public void DisableAI()
    {
        ai.canMove = false;
        ai.isStopped = true;
        ai.enabled = false;
    }

    public void EnableAI()
    {
        ai.canMove = true;
        ai.isStopped = false;
        ai.enabled = true;
    }

    public void SetDestination(Vector3 position)
    {
        ai.destination = position;
        ai.SearchPath();
        isMoving = true;
    }
}