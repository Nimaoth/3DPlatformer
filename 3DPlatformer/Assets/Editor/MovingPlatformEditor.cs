using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(MovingPlatform)), CanEditMultipleObjects]
public class MovingPlatformEditor : Editor {

    public SerializedProperty stateProp;
    public SerializedProperty smoothProp;
    public SerializedProperty speedProp;

    public SerializedProperty pointAProp;
    public SerializedProperty pointBProp;

    public SerializedProperty transformAProp;
    public SerializedProperty transformBProp;

    public SerializedProperty platformParentProp;

    void OnEnable()
    {
        stateProp = serializedObject.FindProperty("Type");
        smoothProp = serializedObject.FindProperty("Smooth");
        speedProp = serializedObject.FindProperty("Speed");

        pointAProp = serializedObject.FindProperty("PointA");
        pointBProp = serializedObject.FindProperty("PointB");

        transformAProp = serializedObject.FindProperty("TransformA");
        transformBProp = serializedObject.FindProperty("TransformB");

        platformParentProp = serializedObject.FindProperty("PlatformParent");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(smoothProp);
        EditorGUILayout.PropertyField(speedProp);
        EditorGUILayout.PropertyField(platformParentProp);
        EditorGUILayout.PropertyField(stateProp);

        MovingPlatform.PositionType pt = (MovingPlatform.PositionType)stateProp.enumValueIndex;

        switch (pt)
        {
            case MovingPlatform.PositionType.Vector:
                EditorGUILayout.PropertyField(pointAProp);
                EditorGUILayout.PropertyField(pointBProp);
                break;
            case MovingPlatform.PositionType.Transform:
                EditorGUILayout.PropertyField(transformAProp);
                EditorGUILayout.PropertyField(transformBProp);
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }

}
