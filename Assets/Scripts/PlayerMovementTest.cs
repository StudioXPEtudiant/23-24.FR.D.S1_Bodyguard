using UnityEngine;

public class PlayerMovementTest : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float crouchSpeed = 2f;
    [SerializeField] private float jumpForce = 5f;

    private bool isCrouching = false;
    private float _initialHeight;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _initialHeight = transform.localScale.y;
    }

    private void Update()
    {
        // Crouch input
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleCrouch();
        }
    }

    private void FixedUpdate()
    {
        // Movement based on crouch state
        float moveSpeed = isCrouching ? crouchSpeed : walkSpeed;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical) * moveSpeed * Time.fixedDeltaTime;
        _rigidbody.MovePosition(_rigidbody.position + transform.TransformDirection(movement));

        // Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isCrouching)
            {
                Jump();
            }
        }
    }

    private void ToggleCrouch()
    {
        isCrouching = !isCrouching;
        AdjustColliderHeight();
    }

    private void AdjustColliderHeight()
    {
        Vector3 newScale = transform.localScale;
        newScale.y = isCrouching ? _initialHeight / 2f : _initialHeight;
        transform.localScale = newScale;
    }

    private void Jump()
    {
        _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}

