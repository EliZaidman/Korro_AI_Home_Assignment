using System;
using UnityEngine;
using UnityEngine.UI;

public class DynamicSpacing : MonoBehaviour
{
    public RectTransform panel;
    public GameObject imagePrefab;
    public int imageCount;
    public float spacing = 5f;
    private void OnEnable()
    {
        PlayerHealth.SentHpOnGameStart += InisiateHP;
        PlayerHealth.OnGettingHit += OnGettingHit;
    }

    private void InisiateHP(int hp)
    {
        ClearAllChildren();
        imageCount = hp;
        for (int i = 0; i < imageCount; i++)
        {
            GameObject newInstance = Instantiate(imagePrefab);
            newInstance.transform.SetParent(gameObject.transform, false);
        }
    }

    private void OnDisable()
    {
        PlayerHealth.SentHpOnGameStart -= InisiateHP;
        PlayerHealth.OnGettingHit -= OnGettingHit;
    }

    private void OnGettingHit(int obj)
    {
        imageCount -= obj;
        ResizePanel();
        InisiateHP(imageCount);
    }

    void Start()
    {
        ResizePanel();
    }

    void ResizePanel()
    {
        // Assuming all images are the same size
        float imageWidth = imagePrefab.GetComponent<RectTransform>().sizeDelta.x;
        float totalWidth = (imageWidth + spacing) * imageCount - spacing; // Calculate total width

        // Set the size of the panel
        panel.sizeDelta = new Vector2(totalWidth, panel.sizeDelta.y);
    }

    public void ClearAllChildren()
    {
        // Iterate through all children and destroy them
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
