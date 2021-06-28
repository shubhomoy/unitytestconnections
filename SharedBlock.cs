using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;

public class SharedBlock : NetworkBehaviour
{
    bool isMovable = false;
    GameObject syncManager;

    void Start() {
        syncManager = GameObject.Find("SyncManager");
        transform.position = new Vector3(0, transform.localScale.y / 2f, 0);
    }

    void Update() {
        ToggleMovable();
        Move();

        transform.position = syncManager.GetComponent<SyncManager>().SharedBlockPosition.Value;
    }

    void Move() {
        if(isMovable) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitData;
            
            if (Physics.Raycast(ray, out hitData, 100)) {
                if(hitData.transform.tag == "base") {
                    syncManager.GetComponent<SyncManager>().SharedBlockPosition.Value = hitData.point + new Vector3(0, transform.localScale.y / 2f, 0);
                }
            }
        }
    }

    void ToggleMovable() {
        if(Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitData;
            
            if (Physics.Raycast(ray, out hitData, 100)) {
                if(hitData.transform.tag == "shared_block") {
                    isMovable = !isMovable;
                }
            }
        }
    }
}
