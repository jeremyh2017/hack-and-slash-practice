using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    private CharacterController     CharacterController;
    public  bool                    IsGrounded              => CheckIfGrounded();
    private float                   groundCheckDistance     = 0.1f;
    private float                   sphereCastRadius        = 0.3f; 
    private LayerMask               groundLayer             = 1 << 6; // Assuming layer 6 is the ground layer, adjust as needed

    void Awake()
    {
        CharacterController = GetComponent<CharacterController>();
        if (CharacterController == null)
        {
            Debug.LogError("CharacterController not found on the GameObject.");
            enabled = false;
        }
    }

    public bool CheckIfGrounded()
    {         
        // Perform a raycast downwards to check if the player is grounded
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance, groundLayer))
            return true;
        
        /*else if (Physics.CheckSphere(transform.position, sphereCastRadius, groundLayer))
            return true;*/
        
        else
            return false;
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        Vector3 rayStart = transform.position;
        Vector3 rayDirection = Vector3.down * groundCheckDistance;

        // Draw Ray
        Gizmos.color = IsGrounded ? Color.green : Color.red;
        Gizmos.DrawRay(rayStart, rayDirection);

        // Draw Sphere
        Gizmos.color = IsGrounded ? new Color(0, 1, 0, 0.3f) : new Color(1, 0, 0, 0.3f);
        Gizmos.DrawSphere(transform.position, sphereCastRadius);
    }
}
