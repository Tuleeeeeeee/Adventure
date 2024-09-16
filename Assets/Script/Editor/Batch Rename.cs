using UnityEditor;
using UnityEngine;

public class BatchRename : EditorWindow
{
    private string baseName = "GameObject"; // Default name
    private int startNumber = 1; // Start numbering from here
    private int padding = 3; // Number padding (001, 002, etc.)

    [MenuItem("Tools/Batch Rename")]
    public static void ShowWindow()
    {
        GetWindow<BatchRename>("Batch Rename");
    }

    void OnGUI()
    {
        GUILayout.Label("Rename Settings", EditorStyles.boldLabel);
        baseName = EditorGUILayout.TextField("Base Name", baseName);
        startNumber = EditorGUILayout.IntField("Start Number", startNumber);
        padding = EditorGUILayout.IntField("Number Padding", padding);

        if (GUILayout.Button("Rename"))
        {
            RenameObjects();
        }
    }

    void RenameObjects()
    {
        GameObject[] selectedObjects = Selection.gameObjects;

        if (selectedObjects.Length == 0)
        {
            Debug.LogWarning("No GameObjects selected!");
            return;
        }

        for (int i = 0; i < selectedObjects.Length; i++)
        {
            string newName = baseName + (startNumber + i).ToString().PadLeft(padding, '0');
            selectedObjects[i].name = newName;
        }

        Debug.Log("Renaming complete!");
    }
}


