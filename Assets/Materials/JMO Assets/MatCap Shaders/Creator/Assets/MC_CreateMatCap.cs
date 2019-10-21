using UnityEngine;
using System.Collections;
using System;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class MC_CreateMatCap : MonoBehaviour
{
	public Camera screenshotCamera;
	public Material previewMaterial;
	[Tooltip("View the saved PNG in the file browser on save")]
	public bool revealOnSave = true;
}

#if UNITY_EDITOR
[CustomEditor(typeof(MC_CreateMatCap))]
public class MC_CreateMatCap_Editor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		GUILayout.Space(10);

		if (GUILayout.Button("Save MatCap PNG", GUILayout.Height(40)))
		{
			bool reveal = ((MC_CreateMatCap)target).revealOnSave;
			CaptureScreenshot("MatCap", reveal);
		}
	}

	void CaptureScreenshot(string filename, bool reveal)
	{
		var screenshotCamera = ((MC_CreateMatCap)target).screenshotCamera;
		Graphics.SetRenderTarget(screenshotCamera.targetTexture);
		var texture = new Texture2D(screenshotCamera.targetTexture.width, screenshotCamera.targetTexture.height, TextureFormat.ARGB32, false);
		texture.ReadPixels(new Rect(0, 0, screenshotCamera.targetTexture.width, screenshotCamera.targetTexture.height), 0, 0, false);
		texture.Apply(false, false);
		Graphics.SetRenderTarget(null);

		string path = EditorUtility.SaveFilePanelInProject("Save MatCap Texture", "MatCap", "png", "Save your MatCap image");
		if (!string.IsNullOrEmpty(path))
		{
			// Save the PNG file
			File.WriteAllBytes(path, texture.EncodeToPNG());
			AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceSynchronousImport);
			if (reveal)
			{
				EditorUtility.RevealInFinder(path);
			}

			// Disable compression by default
			var textureImporter = (TextureImporter)AssetImporter.GetAtPath(path);
			textureImporter.textureCompression = TextureImporterCompression.Uncompressed;
			textureImporter.wrapMode = TextureWrapMode.Clamp;

			// Apply to preview material if needed
			var previewMaterial = ((MC_CreateMatCap)target).previewMaterial;
			if (previewMaterial != null)
			{
				var generatedTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
				if (generatedTexture != null)
				{
					previewMaterial.SetTexture("_MatCap", generatedTexture);
				}
			}
		}

		DestroyImmediate(texture);
	}
}
#endif