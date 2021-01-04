using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText_tmp;
    public bool timerRunning_b;

    public float timeInSeconds_f;

    public void Constructor(float _value)
    {
        timeInSeconds_f = _value;
        timerRunning_b = true;
    }

    private void Start()
    {
        timerText_tmp = GetComponent<TextMeshProUGUI>();
        this.name = "Timer";
        this.gameObject.tag = "Timer";
    }

    private void Update()
    {
        if (timerRunning_b)
        {
            if (timeInSeconds_f > 0)
            {
                timeInSeconds_f -= Time.deltaTime;
                DisplayTime(timeInSeconds_f);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText_tmp.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void Pause()
    {
        timerRunning_b = false;
    }

    public void Resume()
    {
        timerRunning_b = true;
    }

    public void Delete()
    {
        Destroy(this.gameObject);
    }
}
