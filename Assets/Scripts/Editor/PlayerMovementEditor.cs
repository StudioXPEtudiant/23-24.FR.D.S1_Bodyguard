using UnityEditor;

[CustomEditor(typeof(PlayerMovement))]
public class PlayerMovementEditor : Editor {
    private SerializedProperty _normalSpeedProperty;
    private SerializedProperty _runSpeedProperty;
    
    private SerializedProperty _floorMaskProperty;
    private SerializedProperty _feetTransformProperty;
    private SerializedProperty _jumpForceProperty;
    private SerializedProperty _radiusDetection;
    
    private void OnEnable()
    {
        _normalSpeedProperty = serializedObject.FindProperty("normalSpeed");
        _runSpeedProperty = serializedObject.FindProperty("runSpeed");
        
        _floorMaskProperty = serializedObject.FindProperty("floorMask");
        _feetTransformProperty = serializedObject.FindProperty("feetTransform");
        _jumpForceProperty = serializedObject.FindProperty("jumpForce");
        _radiusDetection = serializedObject.FindProperty("radiusDetection");
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();
        EditorGUILayout.LabelField("Movement", EditorStyles.boldLabel);
        EditorGUILayout.Slider(_normalSpeedProperty, 0,50, "Speed");
        EditorGUILayout.Slider(_runSpeedProperty, 0, 100, "Run Speed");
        
        EditorGUILayout.LabelField("Jump", EditorStyles.boldLabel);
        EditorGUILayout.Slider(_jumpForceProperty, 0, 50, "Force");
        EditorGUILayout.Slider(_radiusDetection, 0, 1, "Radius Detection");
        EditorGUILayout.PropertyField(_floorMaskProperty);
        EditorGUILayout.PropertyField(_feetTransformProperty);
        serializedObject.ApplyModifiedProperties();
    }
}
