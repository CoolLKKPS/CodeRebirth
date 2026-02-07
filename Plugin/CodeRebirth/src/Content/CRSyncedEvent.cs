using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

namespace CodeRebirth.src.Content;

public class CRSyncedEvent : NetworkBehaviour
{
    [field: SerializeField]
    public bool RunEventOnStart { get; private set; }

    [field: SerializeField]
    public UnityEvent SyncedSuccessEvent { get; private set; }

    [field: SerializeField]
    public UnityEvent SyncedFailureEvent { get; private set; }

    [field: SerializeField]
    public bool IsToggleEvent { get; private set; } = false;

    [field: SerializeField]
    public bool IsSuccessByDefault { get; private set; } = true;

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
        if (IsSuccessByDefault)
        {
            SyncedSuccessEvent.Invoke();
        }
        else
        {
            SyncedFailureEvent.Invoke();
        }

        if (IsToggleEvent)
        {
            IsSuccessByDefault = !IsSuccessByDefault;
        }
        InvokeSyncedEventRpc();
    }

    [Rpc(SendTo.NotMe, RequireOwnership = false)]
    public void InvokeSyncedEventRpc()
    {
        if (IsSuccessByDefault)
        {
            SyncedSuccessEvent.Invoke();
        }
        else
        {
            SyncedFailureEvent.Invoke();
        }

        if (IsToggleEvent)
        {
            IsSuccessByDefault = !IsSuccessByDefault;
        }
    }
}