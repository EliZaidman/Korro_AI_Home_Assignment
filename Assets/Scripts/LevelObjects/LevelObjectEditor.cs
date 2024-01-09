using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelObject))]
public class LevelObjectEditor : Editor
{
    int selectedIndex = 0;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        LevelObject levelObject = (LevelObject)target;

        // Create a dropdown to select the object to switch to
        string[] options = new string[levelObject.possibleObjects.Length];
        for (int i = 0; i < options.Length; i++)
        {
            options[i] = levelObject.possibleObjects[i] != null ? levelObject.possibleObjects[i].name : "None";
        }
        selectedIndex = EditorGUILayout.Popup("Select Object", selectedIndex, options);

        // Button to switch to the selected object
        if (GUILayout.Button("Switch Object"))
        {
            levelObject.SwitchObject(selectedIndex);
        }
    }
}
