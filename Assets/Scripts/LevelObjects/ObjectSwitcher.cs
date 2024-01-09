using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ObjectSwitcher : MonoBehaviour
{
    public GameObject[] replacementPrefabs;
    public int selectedIndex = 0;

    public void SwitchObject()
    {
        if (replacementPrefabs == null || replacementPrefabs.Length == 0) return;

        // Calculate the next index, looping back to 0 if at the end of the array
        selectedIndex = (selectedIndex + 1) % replacementPrefabs.Length;

        // Instantiate the prefab at the new index
        GameObject replacement = PrefabUtility.InstantiatePrefab(replacementPrefabs[selectedIndex]) as GameObject;

        if (replacement != null)
        {
            // Position the new object at the same place as the current one
            replacement.transform.position = transform.position;
            replacement.transform.rotation = transform.rotation;

            // Add and configure the ObjectSwitcher script on the new object
            ConfigureObjectSwitcher(replacement);

#if UNITY_EDITOR
            // Select the new object in the editor
            Selection.activeGameObject = replacement;
#endif
        }

        // Destroy the current object
        DestroyImmediate(gameObject);
    }

    private void ConfigureObjectSwitcher(GameObject newObject)
    {
        ObjectSwitcher newSwitcher = newObject.AddComponent<ObjectSwitcher>();
        newSwitcher.replacementPrefabs = this.replacementPrefabs;
        newSwitcher.selectedIndex = this.selectedIndex; // Keep the updated index
        // Copy any other properties you need
    }

    // This method can be used in the editor script to set the index manually
    public void SetSelectedIndex(int index)
    {
        if (index >= 0 && index < replacementPrefabs.Length)
        {
            selectedIndex = index;
        }
    }
}
