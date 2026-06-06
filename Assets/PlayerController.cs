using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float sprintSpeed = 6f;
    public float mouseSensitivity = 2f;
    public float gravity = -20f;
    public float jumpHeight = 1f;
    public float highJumpHeight = 2f;

    public bool HasCompletionTool { get; private set; }

    private CharacterController controller;
    private Transform playerCamera;

    private float verticalVelocity;
    private float cameraPitch;
    private float lockedHorizontalSpeed = 3f;
    private bool hasHighJump;

    private void Start()
    {
        controller = GetComponent<CharacterController>();

        Camera cameraObject = GetComponentInChildren<Camera>();
        playerCamera = cameraObject.transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        lockedHorizontalSpeed = moveSpeed;
    }

    private void Update()
    {
        LookAround();
        Move();
    }

    public void EnableHighJump()
    {
        hasHighJump = true;
        jumpHeight = highJumpHeight;

        UnityEngine.Debug.Log("Abyss Shard acquired.");
    }

    public void ObtainCompletionTool()
    {
        HasCompletionTool = true;

        UnityEngine.Debug.Log("Completion tool acquired.");
    }

    private void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, -80f, 80f);

        playerCamera.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
    }

    private void Move()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        Vector3 horizontalMove = transform.right * inputX + transform.forward * inputZ;

        if (horizontalMove.sqrMagnitude > 1f)
        {
            horizontalMove.Normalize();
        }

        bool isGrounded = controller.isGrounded;

        if (isGrounded)
        {
            lockedHorizontalSpeed = moveSpeed;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                lockedHorizontalSpeed = sprintSpeed;
            }
        }

        if (isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = -2f;
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            lockedHorizontalSpeed = moveSpeed;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                lockedHorizontalSpeed = sprintSpeed;
            }

            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        verticalVelocity += gravity * Time.deltaTime;

        Vector3 finalMove = horizontalMove * lockedHorizontalSpeed;
        finalMove.y = verticalVelocity;

        CollisionFlags flags = controller.Move(finalMove * Time.deltaTime);

        if ((flags & CollisionFlags.Above) != 0 && verticalVelocity > 0f)
        {
            verticalVelocity = 0f;
        }
    }

    private void OnGUI()
    {
        if (hasHighJump)
        {
            GUI.Label(new Rect(30, 60, 500, 40), "Abyss Shard Acquired");
        }

        if (HasCompletionTool)
        {
            GUI.Label(new Rect(30, 90, 500, 40), "Completion Tool Acquired");
        }
    }
}