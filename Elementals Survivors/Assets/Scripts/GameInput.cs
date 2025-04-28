using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{

   

    private PlayerInput playerInput;
    private void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.Player.Move.Enable();
       
    }
    // Update is called once per frame
    public Vector2 GetMovementVectorNormalized()
    {

        /*if (Input.GetKey(KeyCode.W))  
            inputDirection.y += 1;
        if (Input.GetKey(KeyCode.S))
            inputDirection.y -= 1;
        if (Input.GetKey(KeyCode.A))
            inputDirection.x -= 1;
        if (Input.GetKey(KeyCode.D))
            inputDirection.x += 1;

        inputDirection.Normalize();
        */
        Vector2 inputDirection = playerInput.Player.Move.ReadValue<Vector2>();

        inputDirection.Normalize();
        return inputDirection;
    }
}
