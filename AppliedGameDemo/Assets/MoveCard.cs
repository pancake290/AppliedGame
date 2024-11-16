using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCard : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask layerMask;

    private void OnMouseDrag()
    {
        if (this.tag == "Card")
        {
            //this.GetComponent<Rigidbody>().isKinematic = true;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerMask))
            {
                transform.position = new Vector3(raycastHit.point.x, raycastHit.point.y + 1.4f, raycastHit.point.z);
            }
        }

    }
}