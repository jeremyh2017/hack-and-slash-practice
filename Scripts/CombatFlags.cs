using UnityEngine;

public class CombatFlags
{
    public bool     IsArmed             { get; private set; }
    public bool     DrawWeaponPressed   { get; private set; }
    public bool     StabAttackPressed   { get; private set; }
    public bool     SlashAttackPressed  { get; private set; }
    public bool     IsAttacking         { get; private set; }
    public bool     IsBlocking          { get; private set; }
    public bool     IsStunned           { get; private set; }
    public bool     IsDead              { get; private set; }
    
    public void SetIsArmed(bool isArmed)
    {
        IsArmed = isArmed;
    }

    public void SetDrawWeaponPressed(bool drawWeaponPressed)
    {
        DrawWeaponPressed = drawWeaponPressed;
    }

    public void SetStabAttackPressed(bool stabAttackPressed)
    {
        StabAttackPressed = stabAttackPressed;
    }

    public void SetSlashAttackPressed(bool slashAttackPressed)
    {
        SlashAttackPressed = slashAttackPressed;
    }

    public void SetIsAttacking(bool isAttacking)
    {
        IsAttacking = isAttacking;
    }

    public void SetIsBlocking(bool isBlocking)
    {
        IsBlocking = isBlocking;
    }

    public void SetIsStunned(bool isStunned)
    {
        IsStunned = isStunned;
    }

    public void SetIsDead(bool isDead)
    {
        IsDead = isDead;
    }

}
