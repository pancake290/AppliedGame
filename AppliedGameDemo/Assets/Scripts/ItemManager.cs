using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public string itemType;
    public RoomUnit currentRoom;

    private void OnTriggerEnter(Collider other)//如果物体进到了房间范围，则把自身所处房间改为该房间
    {
        RoomUnit rooom = other.GetComponent<RoomUnit>();
        if (rooom != null)
        {
            this.currentRoom = rooom;
        }
    }

    public void GoDie()
    {
        currentRoom.RemoveItem(this);
        Destroy(this.gameObject);
        //其它逻辑（创建一个死亡特效等？）
    }
}
