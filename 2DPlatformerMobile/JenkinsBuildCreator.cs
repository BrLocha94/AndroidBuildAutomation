public
class JenkinsBuildCreator {
  SwitchPlatformIfNeeded();  // this method will switch to android platform if
                             // current platform is not android
  static string[] SCENES = FindEnabledEditorScenes();
  static void PerformAndroidBuild() {
    string[] args = System.Environment.GetCommandLineArgs();
    for (int i = 0; i < args.Length; i++) {
      UnityEngine.Debug.Log(
          "PerformAndroidBuild GetCommandLineArgs args: " + "i val: " + i +
          " : " +
          args[i]);  // whatever variables we pass from the command line using
                     // unity3D Jenkins plugin we would capture those values here
    }
    BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
    buildPlayerOptions.locationPathName = "MyGame.APK";  // APK name
    buildPlayerOptions.scenes = SCENES;                  // list of scenes
    buildPlayerOptions.target = BuildTarget.Android;     // target platform
    BuildReport report =
        BuildPipeline.BuildPlayer(buildPlayerOptions);  // generates APK
    BuildSummary summary = report.summary;
    if (summary.result == BuildResult.Succeeded) {
      UnityEngine.Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
    }
    if (summary.result == BuildResult.Failed) {
      UnityEngine.Debug.Log("Build failed");
    }
  }
 private
  static string[] FindEnabledEditorScenes() {
    List<string> EditorScenes = new List<string>();
    foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes) {
      if (!scene.enabled) continue;
      EditorScenes.Add(scene.path);
    }
    return EditorScenes.ToArray();
  }
} static void SwitchPlatformIfNeeded() {
  // EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone,
  // BuildTarget.StandaloneWindows);
  BuildTarget buildTarget = EditorUserBuildSettings.activeBuildTarget;
  UnityEngine.Debug.Log("SwitchPlatformIfNeeded Current buildTarget: " +
                        buildTarget);
  if (buildTarget != BuildTarget.Android) {
    UnityEngine.Debug.Log("SwitchPlatformIfNeeded Switching to Android Target");
    EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android,
                                                    BuildTarget.Android);
  }
}