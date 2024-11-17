using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public Room room;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) // 按下 R 键刷新房间物品
        {
            room.RefreshRoomItems();
            Debug.Log("房间内物品已刷新，共计物品：" + room.itemCount);
        }
    }

    private void OnDrawGizmos()
    {
        if (room != null)
        {
            Gizmos.color = new Color(0, 1, 0, 0.2f);
            Gizmos.DrawCube(Vector3.zero, room.roomSize);
        }
    }
}