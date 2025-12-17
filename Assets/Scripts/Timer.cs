using System;
using System.Collections;
using System.Globalization;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    TextMeshProUGUI timerText;

    [SerializeField] bool running = false;
    [SerializeField] bool active = false;
    [SerializeField] float timeInSeconds;

    private void Awake()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        timerText.text = timeInSeconds.ToString("00.00", CultureInfo.InvariantCulture);
    }

    private void Start()
    {
        
    }

    public void Run()
    {
        running = true;
    }
    public void Stop()
    {
        running = false; 
    }

    public void SetSeconds(float seconds)
    {
        timeInSeconds = seconds;
    }

    // Update is called once per frame
    void Update()
    {
        if (running)
        {
            float oldTime = timeInSeconds;
            timeInSeconds -= Time.deltaTime;
            
            if (timeInSeconds <= 0)
            {
                Stop();
                timeInSeconds = 0;
                timerText.color = Color.red;
                if (active) GameController.Instance.TimerEnded();
            }

            timerText.text = timeInSeconds.ToString("00.00", CultureInfo.InvariantCulture);

            if (timeInSeconds <= 10)
            {
                if (((int)oldTime) > ((int)timeInSeconds))
                {
                    StartCoroutine(MakeTimerPing(10 - ((int)timeInSeconds)));
                }
            }

            
        }
    }

    IEnumerator MakeTimerPing(int timesPerSecond)
    {
        for (int i = 0; i < timesPerSecond; i++)
        {
            StartCoroutine(TimerTextColorPing(Color.red));
            yield return new WaitForSecondsRealtime(1F/timesPerSecond);
        }
    }

    private IEnumerator TimerTextColorPing(Color pingColor)
    {
        timerText.color = pingColor;
        yield return new WaitForSecondsRealtime(0.05F);
        timerText.color = Color.white;
    }
}
