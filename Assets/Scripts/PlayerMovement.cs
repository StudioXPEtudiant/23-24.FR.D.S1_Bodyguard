using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float normalSpeed = 5f;
    [SerializeField] private float crouchSpeed = 2f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float radiusDetection = 0.1f;
    [SerializeField] private LayerMask floorMask;
    [SerializeField] private Transform feetTransform;
    private bool isCrouching = false;
    private float normalHeight;
    private float crouchHeight = 0.5f; // Change this value to your desired crouch height


    [SerializeField] private float dashDistance = 10f;
    [SerializeField] private float dashCooldown = 5f;
    private bool canDash = true;
    private float dashCooldownTimer = 0f;

    

    private float _initialPosition = 1;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true; // Lock rotation while dashing
        normalHeight = transform.localScale.y;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            Dash();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            ToggleCrouch();
        }

        // Update dash cooldown timer
        if (!canDash)
        {
            dashCooldownTimer -= Time.deltaTime;
            if (dashCooldownTimer <= 0f)
            {
                canDash = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouching = !isCrouching;
            AdjustPlayerHeight();
        }
    }
    private void FixedUpdate()
    {
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");

        if (isCrouching)
        {
            Crouch();
        }
        else
        {
            if (canDash)
            {
                Move(x, z, normalSpeed);
            }
        }
    }

    private void Move(float x, float z, float speed)
    {
        var move = transform.TransformDirection(new Vector3(x, 0, z)) * speed;
        _rigidbody.velocity = new Vector3(move.x, _rigidbody.velocity.y, move.z);
    }

    private void Jump()
    {
        if (!Physics.CheckSphere(feetTransform.position, radiusDetection, floorMask)) return;
        _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
   
    }
    private void AdjustPlayerHeight()
    {
        Vector3 newScale = transform.localScale;
        newScale.y = isCrouching ? crouchHeight : normalHeight;
        transform.localScale = newScale;
    }

private void Dash()
    {
        Vector3 dashDirection = transform.forward;
        float dashSpeed = 20f;
        float dashTime = 2f; // Set the desired dash time in seconds
        float dashDistance = dashSpeed * dashTime; // Calculate the dash distance based on time
        Vector3 dashVelocity = dashDirection * dashSpeed;

        _rigidbody.velocity = new Vector3(dashVelocity.x, _rigidbody.velocity.y, dashVelocity.z);

        canDash = false;
        dashCooldownTimer = dashCooldown;

        // Unlock rotation after dash
        StartCoroutine(WaitAndUnlockRotation(dashTime));
    }

    private void Crouch()
    {
        float targetHeight = isCrouching ? _initialPosition : crouchHeight;

        // Smoothly interpolate between current height and target height
        float newHeight = Mathf.Lerp(_rigidbody.position.y, targetHeight, 5 * Time.deltaTime);

        // Set the new height
        _rigidbody.position = new Vector3(_rigidbody.position.x, newHeight, _rigidbody.position.z);
        
    }

   
    private void ToggleCrouch()
    {
        isCrouching = !isCrouching;
        Crouch();
    }


    private IEnumerator WaitAndUnlockRotation(float dashTime)
    {
        yield return new WaitForSeconds(dashTime);
        _rigidbody.freezeRotation = false; // Unlock rotation after the dash
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        // Draw feet
        Gizmos.DrawWireSphere(feetTransform.position, radiusDetection);

        // Draw jump height
        var initialVelocity = jumpForce / 1;
        var jumpHeight = initialVelocity * initialVelocity / (2 * Mathf.Abs(Physics.gravity.y));

        if (Physics.CheckSphere(feetTransform.position, radiusDetection, floorMask))
        {
            _initialPosition = transform.localPosition.y;
        }

        Gizmos.DrawLine(new Vector3(transform.position.x, _initialPosition, transform.position.z), new Vector3(transform.position.x, _initialPosition + jumpHeight, transform.position.z));
    }
}