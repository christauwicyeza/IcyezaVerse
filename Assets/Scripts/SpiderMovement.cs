using UnityEngine;

public class SpiderMovement : MonoBehaviour
{
    private Animator animator;
    public float speed = 0.2f;
    public float turnInterval = 3f;
    public float turnAmount = 45f;
    public float detectionRadius = 2f;
    public LayerMask spiderLayer;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            Debug.LogWarning("Animator not found on " + gameObject.name);
        }
        InvokeRepeating("RandomTurn", turnInterval, turnInterval);
    }

    void Update()
    {
        if (!IsObstacleInFront())
        {
            transform.position += -transform.forward * speed * Time.deltaTime;
        }
        else
        {
            RandomTurn();
        }
    }

    void RandomTurn()
    {
        float randomTurn = Random.Range(-turnAmount, turnAmount);
        transform.Rotate(0, randomTurn, 0);
    }

    bool IsObstacleInFront()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, detectionRadius, transform.forward, out hit, detectionRadius, spiderLayer))
        {
            return true;
        }
        return false;
    }
}
