using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MultiPlayersBuildAndRun
{
    [MenuItem("Tools/Run/OSX/Singleplayer")]
    static void PerformOSXBuildSingle()
    {
        PerformBuild(1, BuildTarget.StandaloneOSX);
    }
    [MenuItem("Tools/Run/OSX/Multiplayer/2 Players")]
    static void PerformOSXBuildSingle2()
    {
        PerformBuild(2, BuildTarget.StandaloneOSX);
    }
    [MenuItem("Tools/Run/OSX/Multiplayer/3 Players")]
    static void PerformOSXBuildSingle3()
    {
        PerformBuild(3, BuildTarget.StandaloneOSX);
    }

    [MenuItem("Tools/Run/Windows64/Singleplayer")]
    static void PerformWindows64BuildSingle()
    {
        PerformBuild(1, BuildTarget.StandaloneWindows);
    }
    [MenuItem("Tools/Run/Windows64/Multiplayer/2 Players")]
    static void PerformWindows64Build2()
    {
        PerformBuild(2, BuildTarget.StandaloneWindows);
    }
    [MenuItem("Tools/Run/Windows64/Multiplayer/3 Players")]
    static void PerformWindows64Build3()
    {
        PerformBuild(3, BuildTarget.StandaloneWindows);
    }
    [MenuItem("Tools/Run/Windows64/Multiplayer/4 Players")]
    static void PerformWindows64Build4()
    {
        PerformBuild(4, BuildTarget.StandaloneWindows);
    }

    static void PerformBuild(int playerCount, BuildTarget target)
    {
        EditorUserBuildSettings.SwitchActiveBuildTarget(
            BuildTargetGroup.Standalone, target);

        for (int i = 1; i <= playerCount; ++i)
        {
            BuildPipeline.BuildPlayer(GetScenePaths(),
                "Builds/" + GetProjectName() + i.ToString() + "/" + GetProjectName() + i.ToString() + ".exe",
                target, BuildOptions.AutoRunPlayer
                );
        }
    }

    static string GetProjectName()
    {
        string[] s = Application.dataPath.Split('/');
        return s[s.Length - 2];
    }

    static string[] GetScenePaths()
    {
        string[] scenes = new string[1];

        for (int i = 0; i < 2; ++i)
        {
            string scene = EditorBuildSettings.scenes[i].path;
            if (scene != "")
                scenes[0] = scene;
        }
        return scenes;
    }
}
