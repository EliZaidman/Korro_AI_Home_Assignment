using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Animation anim;

    private void OnEnable()
    {
        Key.OnCollectingKey += Key_OnCollectingKey;
    }
    private void OnDisable()
    {
        Key.OnCollectingKey -= Key_OnCollectingKey;
    }
    private void Key_OnCollectingKey(int obj)
    {
        anim.Play();
    }
}
