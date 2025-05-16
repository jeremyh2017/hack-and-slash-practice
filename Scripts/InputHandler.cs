using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler
{
    public MovementFlags        MovementFlags       { get; private set; }
    public CombatFlags          CombatFlags         { get; private set; }
    public PlayerControls       PlayerControls      { get; private set; }
    public Vector2              MoveInput           { get; private set; }


    public InputHandler(MovementFlags movementFlags, CombatFlags combatFlags)
    {
        MovementFlags = movementFlags;
        CombatFlags   = combatFlags;

        PlayerControls = new PlayerControls();
        PlayerControls.Enable();

        PlayerControls.Player.Jump.performed += OnJumpPressed;
        PlayerControls.Player.Crouch.performed += OnCrouchPressed;
        PlayerControls.Player.DrawWeapon.performed += OnDrawWeaponPressed;
        PlayerControls.Player.StabAttack.performed += OnStabAttackPressed;
        PlayerControls.Player.SlashAttack.performed += OnSlashAttackPressed;
        PlayerControls.Player.Run.performed += OnRunPressed;
        PlayerControls.Player.Run.canceled += OnRunReleased;

    }

    public void ProcessPlayerInput()
    {
        MoveInput = GetMoveInput();
        MovementFlags.SetMoveInput(MoveInput);
    }

    public void Cleanup()
    {
        PlayerControls.Disable();
        PlayerControls.Player.Jump.performed -= OnJumpPressed;
        PlayerControls.Player.Crouch.performed -= OnCrouchPressed;
        PlayerControls.Player.DrawWeapon.performed -= OnDrawWeaponPressed;
        PlayerControls.Player.StabAttack.performed -= OnStabAttackPressed;
        PlayerControls.Player.SlashAttack.performed -= OnSlashAttackPressed;
        PlayerControls.Player.Run.performed -= OnRunPressed;
        PlayerControls.Player.Run.canceled -= OnRunReleased;
    }

    private Vector2 GetMoveInput()
    {
        return PlayerControls.Player.Move.ReadValue<Vector2>();
    }

    private void OnJumpPressed(InputAction.CallbackContext context)
    {
        if (MovementFlags.IsGrounded) 
            MovementFlags.SetJumpWasPressed(true);
    }

    private void OnCrouchPressed(InputAction.CallbackContext context)
    {
        if (!MovementFlags.CrouchIsActive)
            MovementFlags.SetCrouch(true);
        
        else
            MovementFlags.SetCrouch(false);
        
    }
    private void OnRunPressed(InputAction.CallbackContext context)
    {
        if (MovementFlags.CrouchIsActive)
            MovementFlags.SetCrouch(false);

        MovementFlags.SetRunIsPressed(true);
    }

    private void OnRunReleased(InputAction.CallbackContext context)
    {
        MovementFlags.SetRunIsPressed(false);
    }

    private void OnDrawWeaponPressed(InputAction.CallbackContext context)
    {
        CombatFlags.SetDrawWeaponPressed(true);
    }

    private void OnStabAttackPressed(InputAction.CallbackContext context)
    {
        if (CombatFlags.IsArmed)
            CombatFlags.SetStabAttackPressed(true);
    }

    private void OnSlashAttackPressed(InputAction.CallbackContext context)
    {
        if (CombatFlags.IsArmed)
            CombatFlags.SetSlashAttackPressed(true);
    }
}