using UnityEngine;

public class DisableCollider : MonoBehaviour
{
    public Collider targetCollider;

    public void DisableCollide()
    {
        if (targetCollider != null)
        {
            targetCollider.enabled = false;
        }
    }
}
