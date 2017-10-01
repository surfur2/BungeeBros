using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ScoreManager))]
public class ScoreManagerDebug : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ScoreManager scoreMan = target as ScoreManager;
        
        if (GUILayout.Button("UpdateScores"))
        {
            //scoreMan.UpdateScores();
        }
    }
}
