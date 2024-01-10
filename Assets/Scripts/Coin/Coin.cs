using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static event Action<int> OnCoinCollected; // Event for grounded state change
    public float speed = 8;
    public int coinValue;
    public static int coinCount = 0; // Static to keep track across all coins
    public TextMeshProUGUI coinText; // Assign this in the inspector, possibly via a static reference to your UI manager
    public float shrinkSpeed = 1f;
    private Camera mainCamera;
    Animation anim;
    void Start()
    {
        mainCamera = Camera.main;
        anim = GetComponent<Animation>();
        coinText = CoinsUI.Instance.getCoinCounter();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(MoveToUI());
        }
    }

    IEnumerator MoveToUI()
    {
        anim["Coin Anim"].speed = 20f;
        Vector3 startPosition = transform.position;
        Vector3 endPosition = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.WorldToScreenPoint(startPosition).z)); // Top right of the screen
        Vector3 startScale = transform.localScale;
        float journeyLength = Vector3.Distance(startPosition, endPosition);
        float startTime = Time.time;

        while (Vector3.Distance(transform.position, endPosition) > 0.5f)
        {
            float distCovered = (Time.time - startTime) * speed; // Speed of movement
            float fractionOfJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(startPosition, endPosition, fractionOfJourney);
            transform.localScale = Vector3.Lerp(startScale, startScale /1.5f, fractionOfJourney * shrinkSpeed);
            yield return null;
        }

        coinCount++;
        UpdateCoinCount();
        Destroy(gameObject);
    }

    void UpdateCoinCount()
    {
        if (coinText != null)
        {
            OnCoinCollected?.Invoke(coinValue);
        }
    }
}
