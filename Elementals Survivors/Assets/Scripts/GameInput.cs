using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    // Update is called once per frame
    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputDirection = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
            inputDirection.y += 1;
        if (Input.GetKey(KeyCode.S))
            inputDirection.y -= 1;
        if (Input.GetKey(KeyCode.A))
            inputDirection.x -= 1;
        if (Input.GetKey(KeyCode.D))
            inputDirection.x += 1;

        inputDirection.Normalize();
        return inputDirection;
    }
}
