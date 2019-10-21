using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
	using UnityEditor;

	[CustomEditor(typeof(pointAt))]
	public class pointAtEditor : Editor {

		public override void OnInspectorGUI() {
			DrawDefaultInspector();

			pointAt myScript = (pointAt)target;
			if(GUILayout.Button("Connect"))
			{
				myScript.connectLine();
			}
		}
	}
#endif
