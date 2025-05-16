using UnityEngine;

public class MovementHandler
{
    public MovementFlags        MovementFlags           { get; private set; }
    public CharacterController  CharacterController     { get; private set; }

    private float               walkSpeed               = 5f;
    private float               runSpeed                = 10f;
    private float               crouchSpeed             = 2.5f;
    private float               gravity                 = -20f;
    private float               jumpForce               = 10f;
    private float               jumpCooldown            = 1f;
    private float               verticalVelocity;
    private float               rotationSpeed           = 8f;

    public MovementHandler(MovementFlags movementFlags, CharacterController characterController)
    {
        MovementFlags           = movementFlags;
        CharacterController = characterController;
    }

    public void MovePlayer()
    {
        HandleJumps();
        ApplyGravity();
        Vector3 moveDirection = PrepareMove();
        CharacterController.Move(moveDirection);
    }

    private Vector3 GetCameraRelativeMovement()
    {
        Vector2 input = MovementFlags.MoveInput;
        if (input == Vector2.zero)
        {
            return Vector3.zero;
        }

        // Get the camera's forward and right directions
        Transform cameraTransform = Camera.main.transform;
        if (cameraTransform == null)
        {
            Debug.LogWarning("MovementHandler: Main camera not found");
            return new Vector3(input.x, 0, input.y).normalized;
        }

        Vector3 cameraForward = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up).normalized;
        Vector3 cameraRight = Vector3.ProjectOnPlane(cameraTransform.right, Vector3.up).normalized;
        Vector3 moveDirection = (cameraRight * input.x) + (cameraForward * input.y);
        moveDirection = Vector3.ClampMagnitude(moveDirection, 1f);

        // Rotate player to face the move direction
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            CharacterController.transform.rotation = Quaternion.Slerp(
                CharacterController.transform.rotation,
                targetRotation,
                Time.deltaTime * rotationSpeed);
        }

        return moveDirection;
    }

    private void HandleJumps()
    {
        if (MovementFlags.JumpWasPressed && MovementFlags.IsGrounded && jumpCooldown <= 0)
        {
            Debug.Log($"JumpCheck: JumpPressed={MovementFlags.JumpWasPressed}, IsGrounded={MovementFlags.IsGrounded}");

            verticalVelocity = jumpForce;
            MovementFlags.SetVerticalVelocity(verticalVelocity);
            MovementFlags.SetJumpWasPressed(false);
            jumpCooldown = 1.5f; // Reset cooldown
            Debug.Log("MovementHandler: Jump triggered, VerticalVelocity=" + MovementFlags.VerticalVelocity);
        }
        else if (jumpCooldown > 0)
        {
            jumpCooldown -= 0.1f;
        }
    }
    private Vector3 PrepareMove()
    {
        Vector3 moveDirection = GetCameraRelativeMovement();
        float speed = DetermineSpeed();
        moveDirection = moveDirection * speed * Time.deltaTime;
        moveDirection.y = MovementFlags.VerticalVelocity * Time.deltaTime;
        return moveDirection;
    }
    private void ApplyGravity()
    {
        verticalVelocity = MovementFlags.VerticalVelocity;
        verticalVelocity += gravity * Time.deltaTime;
        MovementFlags.SetVerticalVelocity(verticalVelocity);
    }

    private float DetermineSpeed()
    {
        if (MovementFlags.CrouchIsActive)
        {
            return crouchSpeed;
        }
        else if (MovementFlags.RunIsPressed)
        {
            return runSpeed;
        }
        else
        {
            return walkSpeed;
        }
    }
}