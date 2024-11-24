using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomIll : MonoBehaviour
{
    private RoomUnit roomUnit;
    public CardDisplayManager cardDisplayManager;
    private void Start()
    {
        roomUnit = GetComponent<RoomUnit>();
    }
    private void OnTriggerEnter(Collider other)
    {
        cardDisplayManager.ShowCard(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        cardDisplayManager.HideCard();
    }

    public void HideCard()
    {
        cardDisplayManager.HideCard();
    }
}
