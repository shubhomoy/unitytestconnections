using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;

public class Player : NetworkBehaviour
{

    public GameObject objectPrefab;

    public void createObject() {
        if(NetworkManager.Singleton.IsServer || NetworkManager.Singleton.IsHost) {
            GameObject newObj = Instantiate(objectPrefab, new Vector3(0, objectPrefab.transform.localScale.y / 2f, 0), Quaternion.identity);
            newObj.name = newObj.GetInstanceID().ToString();
            newObj.GetComponent<NetworkObject>().Spawn();
        }else{
            spawnObjServerRpc();
        }
    }

    [ServerRpc]
    void spawnObjServerRpc() {
        createObject();
    }

    [ServerRpc]
    public void changeOwnershipServerRpc(ulong clientId, string selectedObjName, ulong networkId) {
        Debug.Log("Changing ownership of " + selectedObjName);
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("player_block")) {
            if(obj.GetComponent<NetworkObject>().NetworkObjectId == networkId) {
                obj.GetComponent<NetworkObject>().ChangeOwnership(clientId);
                break;
            }
        }
    }
}
