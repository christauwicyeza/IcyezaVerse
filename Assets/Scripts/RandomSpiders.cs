using UnityEngine;

public class RandomSpider : MonoBehaviour
{
    private Animator animator;
    public float speed = 0.5f;
    public float detectionDistance = 0.2f;
    public LayerMask boxBoundaryLayer;
    private Vector3 moveDirection;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetBool("isWalking", true);
        }

        moveDirection = -transform.forward;
    }

    void Update()
    {
        if (IsNearBoundary())
        {
            TurnAround();
        }

        transform.position += moveDirection * speed * Time.deltaTime;
    }

    bool IsNearBoundary()
    {
        Vector3 origin = transform.position + Vector3.up * 0.1f;
        return Physics.Raycast(origin, moveDirection, detectionDistance, boxBoundaryLayer);
    }

    void TurnAround()
    {
        float randomTurn = Random.Range(120f, 180f);
        transform.Rotate(0, randomTurn, 0);
        moveDirection = -transform.forward;
    }
}
