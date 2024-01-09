using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    private GameObject player;
    public float topY; 
    private float bottomY; 
    public float speed = 1.0f; // Speed of the elevator

    private bool goingUp = true; 

    private void Start()
    {
        bottomY = transform.position.y;
    }
    void Update()
    {
        float targetY = goingUp ? topY : bottomY;

        transform.position = new Vector3(transform.position.x,
                                         Mathf.Lerp(transform.position.y, targetY, Time.deltaTime),
                                         transform.position.z);

        if (goingUp && transform.position.y >= topY - 0.5f)
        {
            goingUp = false; 
        }
        else if (!goingUp && transform.position.y <= bottomY + 0.5f)
        {
            goingUp = true; 
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject;
            player.transform.parent = transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.transform.parent = null;
        }
    }
}
