using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class TimedButton : MonoBehaviour
{
    // Start is called before the first frame update
    private float currentTime;
    [SerializeField] public Button button;
    public void Start()
    {
        currentTime = 0;
        button.interactable = false;
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= 15)
        {
            button.interactable = true;
        }
    }
}
