using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;

public class SyncManager : NetworkBehaviour
{
    public GameObject objectPrefab;

    public NetworkVariableVector3 SharedBlockPosition = new NetworkVariableVector3(new NetworkVariableSettings
    {
        WritePermission = NetworkVariablePermission.Everyone,
        ReadPermission = NetworkVariablePermission.Everyone
    });

    public void createObject() {
        if (NetworkManager.Singleton.ConnectedClients.TryGetValue(NetworkManager.Singleton.LocalClientId, out var networkedClient)) {
            var player = networkedClient.PlayerObject.GetComponent<Player>();
            if (player) {
                player.createObject();
            }
        }
    }

    public void changeOwnership(string objName, ulong networkId) {
        if (NetworkManager.Singleton.ConnectedClients.TryGetValue(NetworkManager.Singleton.LocalClientId, out var networkedClient)) {
            var player = networkedClient.PlayerObject.GetComponent<Player>();
            if (player) {
                player.changeOwnershipServerRpc(NetworkManager.Singleton.LocalClientId, objName, networkId);
            }
        }
    }
}
