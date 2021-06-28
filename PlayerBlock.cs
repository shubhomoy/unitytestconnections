using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;

public class PlayerBlock : NetworkBehaviour
{
    GameObject syncManager;

    bool isMovalble;
    
    void Start() {
        syncManager = GameObject.Find("SyncManager");
    }

    public override void NetworkStart() {
        
    }

    void Update() {
        ToggleMovable();
        Move();
        // transform.position = syncManager.GetComponent<SyncManager>().BlockPosition.Value;
    }

    void ToggleMovable() {
        if(Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitData;
            
            if (Physics.Raycast(ray, out hitData, 100)) {
                if(hitData.transform.gameObject == gameObject) {
                    if(!GetComponent<NetworkObject>().IsOwner) {
                        syncManager.GetComponent<SyncManager>().changeOwnership(gameObject.name, GetComponent<NetworkObject>().NetworkObjectId);
                    }
                        
                    isMovalble = !isMovalble;
                }
            }
        }
    }

    void Move() {
        if(isMovalble) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitData;
            
            if (Physics.Raycast(ray, out hitData, 100)) {
                if(hitData.transform.tag == "base") {
                    transform.position = (hitData.point + new Vector3(0, transform.localScale.y / 2f, 0));
                }
            }
        }
    }
}
