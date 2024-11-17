using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public Room room;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) // ���� R ��ˢ�·�����Ʒ
        {
            room.RefreshRoomItems();
            Debug.Log("��������Ʒ��ˢ�£�������Ʒ��" + room.itemCount);
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