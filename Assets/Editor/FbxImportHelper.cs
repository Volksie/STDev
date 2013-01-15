using UnityEngine;
using UnityEditor;
using System.Collections;

public class FbxImportHelper : AssetPostprocessor
{
	#region Private data

	private ModelImporter mImporter;

	#endregion

	#region Interface
	#endregion

	#region Implementation of AssetPostprocessor

	void OnPreprocessModel()
	{
		Initialize();
		PreprocessModel();
	}

	#endregion

	#region Implementation

	private const string FbxExt = ".fbx";

	private void Initialize()
	{
		Debug.Log("Importing asset '" + assetPath + "'.");
		mImporter = assetImporter as ModelImporter;
	}

	private void PreprocessModel()
	{
		mImporter.materialName = ModelImporterMaterialName.BasedOnMaterialName;
		mImporter.importMaterials = true;
		mImporter.globalScale = 1;
	}

	#endregion
}
