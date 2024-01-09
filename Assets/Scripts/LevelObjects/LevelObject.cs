using UnityEngine;

public class LevelObject : MonoBehaviour
{
    public GameObject[] possibleObjects;

    public void SwitchObject(int index)
    {
        foreach (var obj in possibleObjects)
        {
            obj.SetActive(false);
        }

        if (index >= 0 && index < possibleObjects.Length && possibleObjects[index] != null)
        {
            possibleObjects[index].SetActive(true);
        }
    }
}
