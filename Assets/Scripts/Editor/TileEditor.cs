using UnityEngine;
using System.Collections;
using UnityEditor;

//[CustomEditor(typeof(TileBase))]
public class TileEditor : Editor {

	public override void OnInspectorGUI()
	{
		//TileBase myTarget = (TileBase)target;
		
		//myTarget.test  = EditorGUILayout.FloatField("Experience", myTarget.test);
		//EditorGUILayout.LabelField("Level", myTarget.test.ToString());
	}
}
