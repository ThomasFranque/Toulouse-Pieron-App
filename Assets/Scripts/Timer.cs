using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerTxt;
    private int _sessionMinutes;
    private float _sessionSeconds;
    public float TimeRemaining { get; private set; }
    private bool _started;

    private Action OnEnd;

    public float Minutes;
    public float Seconds;
    
    public void Begin(int sessionMinutes, Action onEnd)
    {
        _sessionMinutes = sessionMinutes;
        _sessionSeconds = _sessionMinutes * 60;
        TimeRemaining = _sessionSeconds;
        _started = true;
        OnEnd = onEnd;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_started) return;
        TimeRemaining -= Time.deltaTime;
        if (TimeRemaining < 0)
        {
            TimeRemaining = 0;
            _started = false;
            OnEnd?.Invoke();
        }
        DisplayTime(TimeRemaining);

    }

    private void DisplayTime(float timeToDisplay)
    {
        Minutes = Mathf.FloorToInt(timeToDisplay / 60);
        Seconds = Mathf.FloorToInt(timeToDisplay % 60);

        _timerTxt.text = string.Format("{0:00}:{1:00}", Minutes, Seconds);
    }
}