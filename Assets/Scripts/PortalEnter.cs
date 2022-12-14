using UnityEngine;

public class PortalEnter : MonoBehaviour
{
    [SerializeField] private Collider _exitPortalCollider;

    private Vector3 RandomPointOnExitPortal()
    {
        return new Vector3(
            Random.Range(_exitPortalCollider.bounds.min.x, _exitPortalCollider.bounds.max.x),
            Random.Range(_exitPortalCollider.bounds.min.y, _exitPortalCollider.bounds.max.y),
            Random.Range(_exitPortalCollider.bounds.min.z, _exitPortalCollider.bounds.max.z));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Asteroid asteroid))
        {
            asteroid.CalculateNewMoveSettings(RandomPointOnExitPortal());
        }
    }
}
