using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Message))]
public class MessageEditor : Editor
{
    static bool showFolds = true;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Message message = (Message)target;

        showFolds = EditorGUILayout.Foldout(showFolds, "Obfuscated");
        if (showFolds)
        {
            GUILayout.TextArea(message.Untext);
        }

        showFolds = EditorGUILayout.Foldout(showFolds, "Unobfuscated");
        if (showFolds)
        {
            GUILayout.TextArea(message.UnobfuscatedText);
        }
    }
}
