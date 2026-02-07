using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

namespace CodeRebirth.src.Content;

public class CRSyncedEvent : NetworkBehaviour
{
    [field: SerializeField]
    public bool RunEventOnStart { get; private set; }

    [field: SerializeField]
    public UnityEvent SyncedEvent { get; private set; }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (RunEventOnStart && NetworkManager.Singleton.IsServer)
        {
            InvokeSyncedEvent();
        }
    }

    public void InvokeSyncedEvent()
    {
        SyncedEvent.Invoke();
        InvokeSyncedEventRpc();
    }

    [Rpc(SendTo.NotMe, RequireOwnership = false)]
    public void InvokeSyncedEventRpc()
    {
        SyncedEvent.Invoke();
    }
}