
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    void OnMouseEnter()
    {
        
    }
    [SerializeField] private float walkSpeed = 7f;
    [SerializeField] private float turnSpeed = 25f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] Vector3 elementsOffset = new Vector3(0, 1, 0);
    [SerializeField] Vector3 cameraOffset = new Vector3(0, 10, -4);
    [SerializeField] private float stopSharpness = 1.2f; // 1.1 stops slowly 2 stops fast
    private float playerHeight = 2.2f;
    private float playerWidth = .65f;
    private bool isWalking = false;
    private Rigidbody rb;
    [SerializeField] private Camera camera;

    private bool isSimulatingForce = false;
    private float powerSeconds;
    private Vector3 simulaingForce = Vector3.zero;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
     
    }
    private void Update()
    {
        Vector2 inputDirection = gameInput.GetMovementVectorNormalized();
        Vector3 movingDirection = new Vector3(inputDirection.x, 0, inputDirection.y);
        Vector3 amountToAdd = movingDirection * walkSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + amountToAdd);
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 lookingDirection = new Vector3(hit.point.x, transform.position.y, hit.point.z) - transform.position;
            rb.rotation = Quaternion.Slerp(rb.rotation, Quaternion.LookRotation(lookingDirection), Time.deltaTime * turnSpeed);
        }
        
        if (!isSimulatingForce && powerSeconds <= 0)
        {
            rb.linearVelocity /= stopSharpness;
            rb.angularVelocity = Vector3.zero;
        }
        else
        {
            powerSeconds -= Time.deltaTime;
            if (powerSeconds <= 0)
            {
                isSimulatingForce = false;
            }
            if (simulaingForce != Vector3.zero)
            {
                rb.AddForce(simulaingForce, ForceMode.Impulse);
                simulaingForce = Vector3.zero;
            }
            
            simulaingForce = Vector3.zero;
        }

            isWalking = inputDirection != new Vector2(0, 0);
        /*
        if (isWalking && CanMove(movingDirection, amountToAdd))
        {
           
        }
        else
        {
            Vector3 newMovingDirection = new Vector3(inputDirection.x, 0, 0).normalized;
            amountToAdd = newMovingDirection * walkSpeed * Time.deltaTime;
            if (CanMove(newMovingDirection, amountToAdd))
            {
                transform.position += amountToAdd;
            }
            else
            {
                newMovingDirection = new Vector3(0, 0, inputDirection.y).normalized;
                amountToAdd = newMovingDirection * walkSpeed * Time.deltaTime;
                if (CanMove(newMovingDirection, amountToAdd))
                {
                    transform.position += amountToAdd;
                }
            }
        }
        */
        
        //transform.forward = Vector3.Slerp(transform.forward, movingDirection, Time.deltaTime * turnSpeed);
        
       camera.transform.position = transform.position + cameraOffset;
    }
    private bool CanMove(Vector3 direction, Vector3 positionToAdd)
    {
        RaycastHit hit;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + new Vector3(0, playerHeight, 0), playerWidth, direction, out hit, positionToAdd.magnitude);

        return canMove;
    }
    public bool IsWalking()
    {
        return isWalking;
    }

    public void addForce(Vector3 force, float forceLastingTime)
    {
        isSimulatingForce = true;
        powerSeconds = forceLastingTime;
        simulaingForce = force;
    }
}
