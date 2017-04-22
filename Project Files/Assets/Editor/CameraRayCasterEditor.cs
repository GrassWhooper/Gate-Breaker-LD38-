using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(CameraRayCaster))]
public class CameraRayCasterEditor : Editor
{
    bool expanded = true;
    CameraRayCaster cameraCaster;
    SerializedProperty layerPriorities;
    SerializedProperty clickTimeDamp;

    const string layerPrioritiesName = "layerPriorities", clickTimeDampName = "clickTimeDamp", maxPlayerClickDistName = "maxPlayerClickDist";
    private void OnEnable()
    {
        cameraCaster = target as CameraRayCaster;
        if (cameraCaster == null || serializedObject == null)
        {
            DestroyImmediate(this);
            return;
        }
        layerPriorities = serializedObject.FindProperty(layerPrioritiesName);
        clickTimeDamp = serializedObject.FindProperty(clickTimeDampName);
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        expanded = EditorGUILayout.Foldout(expanded, "Layer Propreties");
        if (expanded == true)
        {
            EditorGUI.indentLevel++;

            ArrayResizer();
            ArrayPainter();

            EditorGUI.indentLevel--;
        }
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(clickTimeDamp);
        serializedObject.ApplyModifiedProperties();
    }

    void ArrayPainter()
    {
        for (int i = 0; i < layerPriorities.arraySize; i++)
        {
            SerializedProperty prop = layerPriorities.GetArrayElementAtIndex(i);
            prop.intValue = EditorGUILayout.LayerField(("Layer : " + (1 + i)), prop.intValue);
        }
    }

    void ArrayResizer()
    {
        int requiredArraySize = EditorGUILayout.IntField("Size", layerPriorities.arraySize);
        if (layerPriorities.arraySize != requiredArraySize)
        {
            layerPriorities.arraySize = requiredArraySize;
        }
    }
}