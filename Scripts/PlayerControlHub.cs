using UnityEngine;

public class PlayerControlHub : MonoBehaviour
{
    public MovementFlags        MovementFlags           { get; private set; }
    public CombatFlags          CombatFlags             { get; private set; }
    public InputHandler         InputHandler            { get; private set; }
    public CombatEngine         CombatEngine            { get; private set; }
    public StateMachine         StateMachine            { get; private set; }
    public MovementHandler      MovementHandler         { get; private set; }
    public CharacterController  CharacterController     { get; private set; }
    public Animator             Animator                { get; private set; }
    public GroundDetection      GroundDetection         { get; private set; }

    void Awake()
    {
        MovementFlags       =   new MovementFlags();
        CombatFlags         =   new CombatFlags();
        InputHandler        =   new InputHandler(MovementFlags, CombatFlags);
        CombatEngine        =   new CombatEngine(CombatFlags, Animator);
        CharacterController =   GetComponent<CharacterController>();
        Animator            =   GetComponent<Animator>();
        GroundDetection     =   GetComponent<GroundDetection>();
        StateMachine        =   new StateMachine(MovementFlags, CombatFlags, CharacterController, Animator, GroundDetection);
        MovementHandler     =   new MovementHandler(MovementFlags, CharacterController);

        StateMachine.InitializeCachedStates();

        if (CharacterController == null)
            Debug.LogError("CharacterController missing on " + gameObject.name);
        if (Animator == null)
            Debug.LogError("Animator missing on " + gameObject.name);
        if (StateMachine == null)
            Debug.LogError("StateMachine missing on " + gameObject.name);
    }

    void Update()
    {
        MovementFlags.SetGroundedStatus(GroundDetection.IsGrounded);
        if (CombatFlags.IsArmed)
        {
            CombatEngine.HandleCombatInput();
        }
        InputHandler.ProcessPlayerInput();
        StateMachine.HandleState();
        MovementHandler.MovePlayer();
    }

    void OnDestroy()
    {
        InputHandler.Cleanup();
    }
}
