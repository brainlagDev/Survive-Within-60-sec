using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerView : MonoBehaviour
{
    [SerializeField] private Text _text;

    private Timer _timer;    
    private float _criticalRange;

    public void Init(Timer timer)
    {
        _timer = timer;
        _timer.Tick += UpdateReadings;
        _timer.CountdownStopped += ClearReadings;
    }

    private void UpdateReadings(int pastTime, float duration)
    {
        if (_criticalRange == 0)
        {
            _criticalRange = 10;
        }

        if (pastTime < _criticalRange)
        {
            SetColorText(Color.red);
        }
        _text.text = Convert.ToString(pastTime);
    }

    private void ClearReadings()
    {
        _text.text = "";
        _criticalRange = 0;
        SetColorText(Color.black);
    }

    private void SetColorText(Color color)
    {
        _text.color = color;
    }
}