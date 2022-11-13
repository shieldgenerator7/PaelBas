using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(MessageChecker))]
public class MessageCheckerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Get Message"))
        {
            foreach (Object obj in targets)
            {
                MessageChecker mc = (MessageChecker)obj;
                getMessage(mc);
            }
        }
    }

    private void getMessage(MessageChecker mc)
    {
        mc.lblMessage.text = mc.message.Untext;
        EditorUtility.SetDirty(mc.lblMessage);
    }
}
