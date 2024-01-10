using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(ObjectSwitcher))]
public class ObjectSwitcherEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ObjectSwitcher switcher = (ObjectSwitcher)target;

        string[] options = new string[switcher.varientPrefabs.Length];
        for (int i = 0; i < options.Length; i++)
        {
            options[i] = switcher.varientPrefabs[i] ? switcher.varientPrefabs[i].name : "None";
        }
        int newSelectedIndex = EditorGUILayout.Popup("Select Replacement Prefab", switcher.selectedIndex, options);
        switcher.SetSelectedIndex(newSelectedIndex);

        if (GUILayout.Button("Switch Object"))
        {
            switcher.SwitchObject();
        }
    }
}
#endif

