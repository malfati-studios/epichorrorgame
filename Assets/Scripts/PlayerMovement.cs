using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float currentSpeed;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Vector3 velocity;
    private bool isGrounded;

    public float originalFootstepDelay;
    public float footstepDelay;
    private float nextFootstep = 0;

    public enum MovingState
    {
        IDLE,
        WALKING,
        RUNNING,
        ONAIR
    }

    public MovingState state = MovingState.IDLE;

    private void Start()
    {
        currentSpeed = speed;
        originalFootstepDelay = footstepDelay;
    }

    void Update()
    {
        nextFootstep -= Time.deltaTime;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (!isGrounded)
        {
            state = MovingState.ONAIR;
            HandleGravity();
            controller.Move(GetMovementInput() * (currentSpeed * Time.deltaTime));
            return;
        }

        if (velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Vector3 move = GetMovementInput();

        if (Input.GetKey(KeyCode.LeftShift) && move.sqrMagnitude > 0)
        {
            if(state == MovingState.IDLE) nextFootstep = footstepDelay;
            currentSpeed = speed * 2;
            state = MovingState.RUNNING;
            
        }
        else if (move.sqrMagnitude > 0)
        {
            if(state == MovingState.IDLE) nextFootstep = footstepDelay;
            currentSpeed = speed;
            state = MovingState.WALKING;
        }
        else
        {
            state = MovingState.IDLE;
        }

        Vector3 moveVelocity = move * (currentSpeed * Time.deltaTime);
        controller.Move(moveVelocity);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        HandleGravity();
        PlayFootStepSound();
    }

    private Vector3 GetMovementInput()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        return transform.right * x + transform.forward * z;
    }

    private void HandleGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void PlayFootStepSound()
    {
        if (state == MovingState.IDLE) return;

        if (state == MovingState.WALKING)
        {
            footstepDelay = originalFootstepDelay;
        }

        if (state == MovingState.RUNNING)
        {
            footstepDelay = originalFootstepDelay / 2;
        }
        
        if (nextFootstep <= 0)
        {
            AudioController.instance.PlayFootstepSound();
            nextFootstep = 0;
            nextFootstep += footstepDelay;
        }
    }
}