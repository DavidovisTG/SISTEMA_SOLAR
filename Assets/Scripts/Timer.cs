using System.Globalization;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{

    public float timeInSeconds = 30;
    public TextMeshProUGUI timerText;

    // Update is called once per frame
    void Update()
    {
        timeInSeconds -= Time.deltaTime;
        timerText.text = timeInSeconds.ToString("00.0", CultureInfo.InvariantCulture);
    }
}
