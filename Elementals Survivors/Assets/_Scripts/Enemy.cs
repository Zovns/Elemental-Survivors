using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float health = 100f;
    private float walkSpeed = 2f;
    private float turnSpeed = 25f;
    public float enemyWidth = .5f;
    public Vector3 movingDirection;
    private Rigidbody rb;
    [HideInInspector] public Transform target;
    public ElementSO element;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody not found on " + gameObject.name);
        }
    }
    private void ChaseTarget()
    {
        if (target == null)
            return;
        Vector3 targetPosition = target.transform.position;
        Vector3 distanceVector = new Vector3(targetPosition.x, transform.position.y, targetPosition.z) - transform.position;
        Vector3 directionToPlayer = distanceVector.normalized;
       
        rb.MovePosition(rb.position + directionToPlayer * walkSpeed * Time.fixedDeltaTime);
        movingDirection = directionToPlayer;
        rb.rotation = Quaternion.Slerp(rb.rotation, Quaternion.LookRotation(directionToPlayer), Time.fixedDeltaTime * turnSpeed);
        //to prevent collissions to miss with the movement
        rb.linearVelocity = Vector3.zero;
    }


    private void FixedUpdate()
    {
        ChaseTarget();
    }
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying || target == null) return;

        Vector3 direction = (target.position - transform.position).normalized;
        float distance = walkSpeed * Time.fixedDeltaTime;
        Vector3 halfExtents = new Vector3(0.5f, 0.5f, 0.5f);

        Gizmos.color = Color.red;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(direction * distance / 2, halfExtents * 2);
    }

}
