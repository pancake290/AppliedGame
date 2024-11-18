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
        if (isPickedUp)
        {
            // 敌人跟随鼠标移动
            FollowMouse();

            // 检测玩家是否点击鼠标左键放下敌人
            if (Input.GetMouseButtonUp(0))
            {
                // 从摄像机向鼠标位置发射射线
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    // 检查射线击中的是否是房间的 Collider
                    RoomUnit room = hit.collider.GetComponent<RoomUnit>();
                    if (room != null)
                    {
                        // 放下敌人到点击的位置
                        PutDown(hit.point);
                    }
                    else
                    {
                        // 点击的位置不是房间，不能放下敌人
                        Debug.Log("无法在此位置放下敌人，请选择房间内的位置。");
                    }
                }
            }
        }
        else
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

    // 抓起敌人
    public void PickUp()
    {
        if (isPickedUp)
            return;

        isPickedUp = true;
        ai.canMove = false;
        ai.isStopped = true;
        ai.enabled = false; // 禁用 AI 移动

        // 记录原本的 Y 轴高度
        originalY = transform.position.y;

        // 增加 Y 轴高度
        transform.position = new Vector3(transform.position.x, originalY + pickUpHeight, transform.position.z);
        this.GetComponent<BoxCollider>().enabled = false;
    }

    // 放下敌人
    public void PutDown(Vector3 position)
    {
        isPickedUp = false;
        ai.canMove = true;
        ai.isStopped = false;
        ai.enabled = true; // 启用 AI 移动

        // 更新敌人的位置，保持 Y 轴高度不变
        transform.position = new Vector3(position.x, originalY, position.z);
        this.GetComponent<BoxCollider>().enabled = true;
        ai.destination = this.transform.position;
    }

    // 敌人跟随鼠标移动
    private void FollowMouse()
    {
        // 从摄像机向鼠标位置发射射线
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, new Vector3(0, originalY + pickUpHeight, 0));
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            Vector3 point = ray.GetPoint(distance);
            transform.position = point;
        }
    }

    // 当鼠标点击敌人时，抓起敌人
    private void OnMouseDown()
    {
        if (!isPickedUp)
        {
            PickUp();
        }
        else
        {
            // 如果已经抓着敌人，点击其他敌人不会抓起新的敌人，而是放下当前的敌人
            // 此处不需要处理，因为在 Update 中已经处理了放下敌人的逻辑
        }
    }
}