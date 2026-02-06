using Unity.Netcode;
using Unity.Netcode.Components;

namespace CodeRebirth.src.Util.Extensions;

public static class NetworkAnimatorExtensions
{
    public static void SetTriggerOnServer(this NetworkAnimator animator, string triggerName)
    {
        if (!NetworkManager.Singleton.IsServer)
        {
            return;
        }

        animator.SetTrigger(triggerName);
    }

    public static void SetTriggerOnServer(this NetworkAnimator animator, int triggerHash)
    {
        if (!NetworkManager.Singleton.IsServer)
        {
            return;
        }

        animator.SetTrigger(triggerHash);
    }
}