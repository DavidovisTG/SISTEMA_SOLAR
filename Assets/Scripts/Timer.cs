using System.Globalization;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer Instance;

    public TextMeshProUGUI timerText;

    [SerializeField] bool running = false;
    [SerializeField] float timeInSeconds;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            Instance = this;
        }
    }

    private void Start()
    {
        timerText.text = timeInSeconds.ToString("00.00", CultureInfo.InvariantCulture);
    }

    public void RunTimer()
    {
        running = true;
    }
    public void StopTimer()
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
            timeInSeconds -= Time.deltaTime;
            timerText.text = timeInSeconds.ToString("00.00", CultureInfo.InvariantCulture);
        }
    }
}
