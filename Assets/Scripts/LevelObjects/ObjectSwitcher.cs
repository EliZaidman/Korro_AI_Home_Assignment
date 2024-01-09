using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ObjectSwitcher : MonoBehaviour
{
    public GameObject[] varientPrefabs;
    public int selectedIndex = 0;
    public bool shouldRetainRotation = true;

    public void SwitchObject()
    {
        if (varientPrefabs == null || varientPrefabs.Length == 0) return;

        // Calculate the next index, looping back to 0 if at the end of the array
        selectedIndex = (selectedIndex + 1) % varientPrefabs.Length;

        // Get the current sibling index and parent transform
        int siblingIndex = transform.GetSiblingIndex();
        Transform parentTransform = transform.parent;

        // Instantiate the prefab at the new index
        GameObject replacement = PrefabUtility.InstantiatePrefab(varientPrefabs[selectedIndex]) as GameObject;

        if (replacement != null)
        {
            // Set the new object's parent and sibling index to match the original object
            replacement.transform.SetParent(parentTransform, worldPositionStays: false);
            replacement.transform.SetSiblingIndex(siblingIndex);

            // Position and rotate the new object like the original
            replacement.transform.position = transform.position;
            if (shouldRetainRotation) { replacement.transform.rotation = transform.rotation; }



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
        newSwitcher.varientPrefabs = this.varientPrefabs;
        newSwitcher.selectedIndex = this.selectedIndex;
        newSwitcher.shouldRetainRotation = this.shouldRetainRotation;
    }

    public void SetSelectedIndex(int index)
    {
        if (index >= 0 && index < varientPrefabs.Length)
        {
            selectedIndex = index;
        }
    }
}
