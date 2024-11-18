using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    // 抓起时增加的高度
    public float pickUpHeight = 2f;

    // 放下时的固定高度
    public float dropHeight = 0f;

    private bool isPickedUp = false;

    // 记录物体的原始高度
    private float originalY;

    // 引用敌人组件（如果有）
    private Enemy enemyComponent;

    private void Start()
    {
        // 获取敌人组件（如果存在）
        enemyComponent = GetComponent<Enemy>();

        // 初始化物体的原始高度
        originalY = transform.position.y;
    }

    private void Update()
    {
        if (isPickedUp)
        {
            // 物体跟随鼠标移动
            FollowMouse();

            // 检测玩家是否松开鼠标左键放下物体
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
                        // 放下物体到点击的位置
                        PutDown(hit.point);
                    }
                    else
                    {
                        // 点击的位置不是房间，不能放下物体
                        Debug.Log("无法在此位置放下物体，请选择房间内的位置。");
                    }
                }
            }
        }
    }

    // 抓起物体
    public void PickUp()
    {
        if (isPickedUp)
            return;

        isPickedUp = true;

        // 如果物体有敌人组件，禁用敌人的 AI 移动
        if (enemyComponent != null)
        {
            enemyComponent.DisableAI();
        }

        // 增加物体的 Y 轴高度
        transform.position = new Vector3(transform.position.x, originalY + pickUpHeight, transform.position.z);
        this.GetComponent<BoxCollider>().enabled = false;
    }

    // 放下物体
    public void PutDown(Vector3 position)
    {
        isPickedUp = false;

        // 更新物体的位置，Y 轴高度为固定的 dropHeight
        transform.position = new Vector3(position.x, dropHeight, position.z);

        // 如果物体有敌人组件，启用敌人的 AI 移动，并更新目的地
        if (enemyComponent != null)
        {
            enemyComponent.EnableAI();

            // 设置敌人的目的地为当前的位置，防止其返回原来的位置
            enemyComponent.SetDestination(transform.position);
        }
        this.GetComponent<BoxCollider>().enabled = true;
    }

    // 物体跟随鼠标移动
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

    // 当鼠标点击物体时，抓起物体
    private void OnMouseDown()
    {
        if (!isPickedUp)
        {
            PickUp();
        }
    }
}
