using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class PlayerLook : MonoBehaviour {
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private float minLimits = -100f;
    [SerializeField] private float maxLimits = 100f;
    
    private float _h;
    private float _v;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() {
        // Get axis mouse position
        _h -= Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        _v -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        
        // Checks if the value does not exceed the limits
        if (_v <= minLimits) _v = minLimits;
        else if (_v >= maxLimits) _v = maxLimits;
        
        // Update rotation
        transform.localRotation = Quaternion.Euler(new float3(_v, 0,0));
        transform.parent.localRotation = Quaternion.Euler(new float3(0,_h,0));
    }

    private void OnDrawGizmos() {
        Handles.color = Color.red;
        Handles.DrawWireArc(transform.position, transform.parent.right, transform.parent.forward, minLimits, 1);
        Handles.DrawWireArc(transform.position, transform.parent.right, transform.parent.forward, maxLimits, 1);
    }
}