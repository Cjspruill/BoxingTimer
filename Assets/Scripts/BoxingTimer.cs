using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BoxingTimer : MonoBehaviour
{

    [SerializeField] float startTime;
    [SerializeField] float elapsedTimeSeconds;

    [SerializeField] bool isActive =false;

    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI buttonText;
    [SerializeField] Image image;
    [SerializeField] Color startColor = Color.green;
    [SerializeField] Color stopColor = Color.red;

    [SerializeField] Button[] buttons;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip tenSecondsAudioClip;
    [SerializeField] AudioClip roundEndAudioClip;

    [SerializeField] bool playTenSecondsOnce;
    private void Start()
    {
        Application.runInBackground = true;
        image.color = startColor;
        ResetTimer();
        var timeSpan = TimeSpan.FromSeconds(startTime);
        var formattedTimeString = timeSpan.ToString(@"hh\:mm\:ss");
        timerText.text = formattedTimeString;
    }
    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            elapsedTimeSeconds -= Time.deltaTime;
            var timeSpan = TimeSpan.FromSeconds(elapsedTimeSeconds);
            var formattedTimeString = timeSpan.ToString(@"hh\:mm\:ss");
            timerText.text = formattedTimeString;

            if(elapsedTimeSeconds <= 11 && !playTenSecondsOnce)
            {
                playTenSecondsOnce = true;
                audioSource.clip = tenSecondsAudioClip;
                audioSource.Play();
            }

            if(elapsedTimeSeconds <= 0)
            {
                FlipIsActive();
                ResetTimer();
            }
        }
    }

    public void FlipIsActive()
    {
        isActive = !isActive;

        buttonText.text = (isActive) ? "Stop" :  "Start";
        image.color = (isActive) ? stopColor : startColor;

        if (isActive)
        {
            for (var i = 0; i < buttons.Length; i++)
            {
                buttons[i].gameObject.SetActive(false);
            }
        }
        else
        {
            if(elapsedTimeSeconds <= 0)
            {
            audioSource.clip = roundEndAudioClip;
            audioSource.Play();
            }

            for (var i = 0; i < buttons.Length; i++)
            {
                buttons[i].gameObject.SetActive(true);
            }
        }
    }

    public void ResetTimer()
    {
        playTenSecondsOnce = false;
        elapsedTimeSeconds = startTime;

        var timeSpan = TimeSpan.FromSeconds(elapsedTimeSeconds);
        var formattedTimeString = timeSpan.ToString(@"hh\:mm\:ss");
        timerText.text = formattedTimeString;
    }

    public void IncrementHours()
    {
        startTime += 3600;

        if (startTime >= 43200)
        {
            startTime = 43200;
        }

        ResetTimer();
    }

    public void DecrementHours()
    {
        startTime -= 3600;

        if(startTime <-0)
        {
            startTime = 0;
        }
        ResetTimer();
    }

    public void IncrementMinutes()
    {
        startTime += 60;

        if(startTime >= 43200)
        {
            startTime = 43200;
        }
        ResetTimer();
    }

    public void DecrementMinutes()
    {
        startTime -= 60;

        if (startTime <= 0)
        {
            startTime = 0;
        }
        ResetTimer();
    }

    public void IncrementSeconds()
    {
        startTime += 1;

        if (startTime >= 43200)
        {
            startTime = 43200;
        }
        ResetTimer();
    }

    public void DecrementSeconds()
    {
        startTime -= 1;

        if (startTime <= 0)
        {
            startTime = 0;
        }
        ResetTimer();
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
