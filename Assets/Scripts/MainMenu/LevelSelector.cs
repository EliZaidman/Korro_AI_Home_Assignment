using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class LevelSelector : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform buttonParent;
    public Sprite[] levelImages; // Array to hold the images for each level
    void Start()
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;

        for (int i = 1; i < sceneCount; i++)
        {
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
            GameObject button = Instantiate(buttonPrefab, buttonParent);
            button.GetComponentInChildren<TextMeshProUGUI>().text = sceneName;

            // Set the image for the button
            if (i < levelImages.Length)
            {
                Image imageComponent = button.GetComponentInChildren<Image>();
                if (imageComponent != null)
                {
                    imageComponent.sprite = levelImages[i];
                }
            }

            int index = i;
            button.GetComponent<Button>().onClick.AddListener(() => LoadScene(index));
        }
    }

    void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
