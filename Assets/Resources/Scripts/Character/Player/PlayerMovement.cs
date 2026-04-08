using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private PlayerData playerData;

    [SerializeField] private Transform cameraTransform;

    public Joystick joystick;

    private CharacterController controller;
    private Vector3 velocity;
    private Animator animator;
    private bool isGrounded;
    private bool isJumping;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleMovement();
        HandleJumping();
        UpdateAnimator();

        if (transform.position.y < -10f)
        {
            GameStats.Instance.AddDeath();
            RespawnBlink.wasRespawned = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void HandleMovement()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = playerData.GroundStickForce;
            isJumping = false;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (joystick != null && joystick.Direction.magnitude > 0.1f)
        {
            x = joystick.Horizontal;
            z = joystick.Vertical;
        }

        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 move = camForward * z + camRight * x;

        float speed = move.magnitude;

        animator.SetFloat("Speed", speed);

        if (move != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        controller.Move(move * playerData.MoveSpeed * Time.deltaTime);

        velocity.y += playerData.Gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void HandleJumping()
    {
        bool jumpInput = false;

        if (Input.GetButtonDown("Jump"))
        {
            jumpInput = true;
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began &&
                touch.position.x > Screen.width / 2)
            {
                jumpInput = true;
            }
        }

        if (jumpInput && isGrounded)
        {
            velocity.y = Mathf.Sqrt(playerData.JumpHeight * -2f * playerData.Gravity);
            isJumping = true;
        }
    }

    private void UpdateAnimator()
    {
        animator.SetBool("IsJumping", isJumping);
    }

    public void PlayFootstep()
    {
        SoundManager.Instance.PlaySFX("footstep");
    }

    public void PlayLanding()
    {
        SoundManager.Instance.PlaySFX("landing");
    }

     public void PlayRowing()
    {
        SoundManager.Instance.PlaySFX("rowing");
    }
}
