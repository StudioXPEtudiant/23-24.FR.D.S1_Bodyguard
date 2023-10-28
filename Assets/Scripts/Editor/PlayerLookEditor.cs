using UnityEditor;

[CustomEditor(typeof(PlayerLook))]
public class PlayerLookEditor : Editor {
    private SerializedProperty _mouseSensitivity;
    private SerializedProperty _minLimitsProperty;
    private SerializedProperty _maxLimitsProperty;
    private float _minLimits;
    private float _maxLimits;
    
    private void OnEnable() {
        _mouseSensitivity = serializedObject.FindProperty("mouseSensitivity");
        
        _minLimitsProperty = serializedObject.FindProperty("minLimits");
        _minLimits = _minLimitsProperty.floatValue;
        
        _maxLimitsProperty = serializedObject.FindProperty("maxLimits");
        _maxLimits = _maxLimitsProperty.floatValue;
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();
        EditorGUILayout.Slider(_mouseSensitivity, 1, 1000, "Sensitivity");
        
        //Limits Area
        EditorGUILayout.MinMaxSlider("Limits",ref _minLimits, ref _maxLimits, -180,180); 
        EditorGUILayout.LabelField($"Min : {_minLimitsProperty.floatValue}, Max : {_maxLimitsProperty.floatValue}");
        
        _minLimitsProperty.floatValue = _minLimits;
        _maxLimitsProperty.floatValue = _maxLimits;
        serializedObject.ApplyModifiedProperties();
    }
}