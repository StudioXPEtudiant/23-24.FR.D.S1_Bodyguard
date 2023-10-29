using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] private float normalSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;
    
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float radiusDetection = 0.1f;
    [SerializeField] private LayerMask floorMask;
    [SerializeField] private Transform feetTransform;

    private float _initialPosition = 1;
    private Rigidbody _rigidbody;
    
    private void Start() {
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) 
            Jump();
    }

    private void FixedUpdate() {
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");
        
        if (Input.GetKey(KeyCode.LeftShift)) Move(x,z,runSpeed);
        else Move(x,z, normalSpeed);
    }

    private void Move(float x, float z, float speed) {
        var move = transform.TransformDirection(new float3(x, 0, z)) * speed;
        _rigidbody.velocity = new float3(move.x, _rigidbody.velocity.y, move.z);
    }

    private void Jump() {
        //Check if the player is touching the floor
        if (!Physics.CheckSphere(feetTransform.position, radiusDetection, floorMask)) return;
        _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Handles.color = Color.green;
        
        //Draw feet
        Gizmos.DrawWireSphere(feetTransform.position, radiusDetection);
        
        //Draw jump height
        var initialVelocity = jumpForce / 1;
        var jumpHeight = initialVelocity * initialVelocity / (2 * math.abs(Physics.gravity.y));
        
        if (Physics.CheckSphere(feetTransform.position, radiusDetection, floorMask)) _initialPosition = transform.localPosition.y;
        Handles.DrawLine(new float3(transform.position.x, _initialPosition, transform.position.z), new float3(transform.position.x, _initialPosition + jumpHeight, transform.position.z));
    }
}
