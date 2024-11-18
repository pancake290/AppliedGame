using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemLogic : MonoBehaviour
{
    public GameObject cockRoach;
    public GameObject tarantula;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y)) { SpawnCockrouach(); }
        if (Input.GetKeyDown(KeyCode.U)) { Spawntarantula(); }
    }

    void SpawnCockrouach() { Instantiate(cockRoach); }

    void Spawntarantula() {Instantiate(tarantula); }
}



