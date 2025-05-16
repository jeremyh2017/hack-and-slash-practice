using UnityEngine;

public class MovementFlags
{
    public bool         IsGrounded          { get; private set; }
    public bool         JumpWasPressed      { get; private set; }
    public bool         CrouchIsActive      { get; private set; } = false;
    public bool         RunIsPressed        { get; private set; }
    public Vector2      MoveInput           { get; private set; }
    public float        VerticalVelocity    { get; private set; } = 0f;

    public void SetGroundedStatus(bool isGrounded)
    {
        IsGrounded = isGrounded;
    }

    public void SetJumpWasPressed(bool jumpWasPressed)
    {
        JumpWasPressed = jumpWasPressed;
    }
    public void SetCrouch(bool crouchIsActive)
    {
        if (!CrouchIsActive)
            CrouchIsActive = true;
        else
            CrouchIsActive = false;
    }

    public void SetRunIsPressed(bool runIsPressed)
    {
        RunIsPressed = runIsPressed;
    }

    public void SetMoveInput(Vector2 moveInput)
    {
        MoveInput = moveInput;
    }

    public void SetVerticalVelocity(float verticalVelocity)
    {
        VerticalVelocity = verticalVelocity;
    }
}
