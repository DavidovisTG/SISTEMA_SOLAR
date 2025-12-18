using System.Collections;
using System.Globalization;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    TextMeshProUGUI timerText;

    [SerializeField] bool running = false;
    [SerializeField] bool active = false;
    [SerializeField] float timeInSeconds;

    Coroutine textAnimation;

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
        timerText.color = Color.white;
        running = true;
    }
    public void Stop()
    {
        if (textAnimation != null)
        {
            StopCoroutine(textAnimation);
            textAnimation = null;
        }
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
                    if (textAnimation != null) StopCoroutine(textAnimation);
                    textAnimation = StartCoroutine(MakeTimerPing(10 - ((int)timeInSeconds)));
                }
            }
        }
        else
        {
            if (textAnimation != null)
            {
                StopCoroutine(textAnimation);
                textAnimation = null;
            }
        }
    }

    IEnumerator MakeTimerPing(int timesPerSecond)
    {
        float wait = 1F / timesPerSecond;
        while (true)
        {
            timerText.color = Color.red;
            yield return new WaitForSecondsRealtime(0.05F);
            timerText.color = Color.white;

            yield return new WaitForSecondsRealtime(wait - 0.05F - Time.deltaTime);
        }
    }

    private IEnumerator TimerTextColorPing(Color pingColor)
    {
        timerText.color = pingColor;
        yield return new WaitForSecondsRealtime(0.05F);
        timerText.color = Color.white;
    }
}
