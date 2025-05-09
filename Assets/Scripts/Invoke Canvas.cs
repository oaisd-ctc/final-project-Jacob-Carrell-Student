using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvokeCanvas : MonoBehaviour
{
    public GameObject canvas;
    public float time = 5f;

    void Start()
    {
        Invoke("EnableObject", time);
    }

    private void EnableObject()
    {
        canvas.SetActive(true);
    }
}
