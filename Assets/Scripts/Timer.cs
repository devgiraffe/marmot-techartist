using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour {

    private static Timer _instance;

    public static Timer Instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<Timer>();
            return _instance;
        }
    }

    public bool EndTimer
    {
        get
        {
            return endTimer;
        }
    }

    public float CurrentTime
    {
        get
        {
            return currentTime;
        }
    }

    public float totalSeconds = 120f;
    public Text displayText;

    private float currentTime;
    private bool isPlaying = false;
    private bool endTimer = false;

    private DateTime timer;

	void Start () {
        currentTime = totalSeconds;
    }

    public void Play()
    {
        isPlaying = true;
    }

	void FixedUpdate () {

        if (isPlaying)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0)
            {
                currentTime = 0;
                isPlaying = false;
                endTimer = true;
            }

            int minutes = Mathf.FloorToInt(currentTime / 60F);
            int seconds = Mathf.FloorToInt(currentTime - minutes * 60);
            int miliseconds = Mathf.FloorToInt(((float)currentTime * 100) % 100);


            string niceTime = string.Format("{0:00}:{1:00}.{2:0}", minutes, seconds, miliseconds);

            displayText.text = niceTime;
        }
	}
}
