//using System;
//using System.Collections;
//using UnityEditor;
//using UnityEngine;

//public class DebugWindow : EditorWindow
//{
//    [MenuItem("Window/Debuger")]
//    public static void ShowWindow()
//    {
//        GetWindow<DebugWindow>("Debuger");
//    }

//    private void OnGUI()
//    {
//        if (GUILayout.Button("Log out"))
//        {
//            DBManager.LogOut();
//        }

//        if (GUILayout.Button("Get db default keybinds"))
//        {
//            Debug.Log(Main.instance.Web.keys.ToJson());
//        }

//        if (GUILayout.Button("Get db default settings"))
//        {
//            Debug.Log(Main.instance.MenuUIManager.SettingsToJson());
//        }

//        EditorGUILayout.Space(10);
//        GUILayout.Label("Settings", EditorStyles.boldLabel);
//        EditorGUILayout.Space(5);
//        if (GUILayout.Button("Save Settings"))
//        {
//            if (EditorApplication.isPlaying)
//            {
//                Main.instance.MenuUIManager.SaveSettings();
//                Debug.Log("Saved settings");
//            }
//            else
//                Debug.Log("Enter play mode to use this feature!");
//        }

//        EditorGUILayout.Space(10);

//        if (GUILayout.Button("Load Settings"))
//        {
//            if (EditorApplication.isPlaying)
//            {
//                Main.instance.MenuUIManager.DebugLoadSettings();
//                Debug.Log("Loaded settings");
//            }
//            else
//                Debug.Log("Enter play mode to use this feature!");
//        }
//        EditorGUILayout.Space(15);
//        GUILayout.Label("Keybinds", EditorStyles.boldLabel);
//        EditorGUILayout.Space(5);
//        if (GUILayout.Button("Save Keybinds"))
//        {
//            if (EditorApplication.isPlaying)
//            {
//                Main.instance.MenuUIManager.DebugSaveKeybinds();
//                Debug.Log("Saved Keybinds");
//            }
//            else
//                Debug.Log("Enter play mode to use this feature!");
//        }

//        EditorGUILayout.Space(10);

//        if (GUILayout.Button("Load Keybinds"))
//        {
//            if (EditorApplication.isPlaying)
//            {
//                Main.instance.MenuUIManager.DebugLoadKeybinds();
//                Debug.Log("Loaded Keybinds");
//            }
//            else
//                Debug.Log("Enter play mode to use this feature!");
//        }
//    }

//    private void StartCoroutine(IEnumerator enumerator)
//    {
//        throw new NotImplementedException();
//    }
//}
