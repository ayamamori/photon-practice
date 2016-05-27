using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BatchBuild : MonoBehaviour {

    static private string ApplicationName = "Photon-Practice";
	//for Android
	static void BuildProject_Android(){

		Debug.Log ("AndroidBuild...");


		string[]		scenePaths	= GetEnabledScenePaths();
		string			outputPath	= Application.dataPath + "/../"+ApplicationName+".apk";
		BuildTarget		target		= BuildTarget.Android;
		BuildOptions	opt			= BuildOptions.None;

		ApplicationBuild(scenePaths,outputPath,target,opt);
	}

	//for iOS Device
	static void BuildProject_iOS(){

		Debug.Log ("iOSBuild...");

		string[]		scenePaths	= GetEnabledScenePaths();
		string			outputPath	= "Device";
		BuildTarget		target		= BuildTarget.iOS;
		BuildOptions	opt			= BuildOptions.SymlinkLibraries;

		//ProjectDataSize Reduction
		EditorUserBuildSettings.symlinkLibraries = true;
		//Prevent SimulatorBuild
		PlayerSettings.iOS.sdkVersion = iOSSdkVersion.DeviceSDK;

		ApplicationBuild(scenePaths,outputPath,target,opt);
	}

	//RunBuild
	static void ApplicationBuild(string[] scenePaths,string outputPath,BuildTarget target,BuildOptions opt){

		string error = BuildPipeline.BuildPlayer(scenePaths,outputPath,target,opt);

		if (!string.IsNullOrEmpty(error))
			Debug.LogError(error);		//BuildFailed
		EditorApplication.Exit(string.IsNullOrEmpty(error) ? 0 : 1);

	}

	//return Enabled Scene in BuildSettings's Scenes In Build 
	static string[] GetEnabledScenePaths() {

		List<string> sceneList = new List<string>();

		//Check Scenes Enabled
		foreach(var scene in EditorBuildSettings.scenes){
			if(scene.enabled)
				sceneList.Add (scene.path);
		}
		return sceneList.ToArray();
	}

}