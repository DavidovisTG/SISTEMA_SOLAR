using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDynamicElements : MonoBehaviour
{
    private GameObject UIPanel;

    [SerializeField] TextMeshProUGUI livesCounter;
    [SerializeField] TextMeshProUGUI targetSuperiorText;
    [SerializeField] TextMeshProUGUI targetNameText;
    [SerializeField] TextMeshProUGUI scoreCounter;
    [SerializeField] TextMeshProUGUI timerText;

    [SerializeField] GameObject dynamicUIBox;

    //OTRAS PROPIEDADES: VALORES POR DEFECTO
    private float initialDynamicUIBoxHeight;

    private void Awake()
    {
        UIPanel = transform.GetChild(0).gameObject;
        initialDynamicUIBoxHeight = dynamicUIBox.GetComponent<RectTransform>().sizeDelta.y;
    }
    private void Start()
    {

    }

    ////////UI ANIMATION
    public IEnumerator StartAnimation()
    {
        dynamicUIBox.SetActive(true);
        yield return StartCoroutine(StartCountdown(3));
    }

    private IEnumerator StartCountdown(int seconds)
    {
        RectTransform TR = dynamicUIBox.GetComponent<RectTransform>();
        TextMeshProUGUI TMPUI = dynamicUIBox.GetComponentInChildren<TextMeshProUGUI>();

        for (int i = seconds; i > 0; i--)
        {
            TMPUI.text = "" + i;
            yield return StartCoroutine(DynamicBoxHeightAnimate(initialDynamicUIBoxHeight));
        }

        TMPUI.text = "¡Ya!";
        TR.sizeDelta = new Vector2(TR.sizeDelta.x, initialDynamicUIBoxHeight);

        StartCoroutine(DelayedTextHide(0.7F));
    }



    private IEnumerator DynamicBoxHeightAnimate(float height)
    {
        RectTransform box = dynamicUIBox.GetComponent<RectTransform>();
        float duration = 0.20f;

        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float h = Mathf.Lerp(0f, height, t / duration);
            box.sizeDelta = new Vector2(box.sizeDelta.x, h);
            yield return null; //PARECE INNECESARIO; SI SE BORRA NO ACTUALIZA FRAME
        }

        yield return new WaitForSecondsRealtime(1f - (duration * 3) - Time.deltaTime);

        t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float h = Mathf.Lerp(height, 0f, t / duration);
            box.sizeDelta = new Vector2(box.sizeDelta.x, h);
            yield return null; //PARECE INNECESARIO; SI SE BORRA NO ACTUALIZA FRAME
        }

        yield return new WaitForSecondsRealtime(duration - Time.deltaTime);
    }
    ////////

    private IEnumerator DelayedTextHide(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        dynamicUIBox.SetActive(false);
    }



    public void LivesNumberTextChange(string text)
    {
        livesCounter.text = text;
    }
    public void TargetSupertextChange(string superText)
    {
        targetSuperiorText.text = superText;
    }
    public void TargetNameTextChange(string targetName)
    {
        targetNameText.text = targetName;
    }
    public void ScoreNumberTextChange(string scoreText)
    {
        scoreCounter.text = scoreText;
    }

    public void DynamicTextChange(int fontSize, string text)
    {
        RectTransform TR = dynamicUIBox.GetComponent<RectTransform>();
        TextMeshProUGUI TMPUI = dynamicUIBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        if (TMPUI == null) Debug.LogError("ASJDFLKANFKLQNWRF");

        TR.sizeDelta = new Vector2(TR.sizeDelta.x, initialDynamicUIBoxHeight*2);
        TMPUI.fontSize = fontSize;
        TMPUI.text = text;
        TMPUI.alignment = TextAlignmentOptions.Center;
    }


    public void AllUIVisible(bool visible)
    {
        for (int c = 0; c < UIPanel.transform.childCount; c++)
        {
            UIPanel.transform.GetChild(c).gameObject.SetActive(visible);
        }
    }
    public void LivesVisible(bool visible)
    {
        livesCounter.GetComponentInParent<GameObject>().SetActive(visible);
    }
    public void ScoreVisible(bool visible)
    {
        scoreCounter.GetComponentInParent<GameObject>().SetActive(visible);
    }
    public void TargetVisible(bool visible)
    {
        targetNameText.GetComponentInParent<GameObject>().SetActive(visible);
    }
    public void DynamicBoxVisible(bool visible)
    {
        dynamicUIBox.SetActive(visible);
    }

    public void SetDynBoxBGColor(Color color)
    {
        dynamicUIBox.GetComponent<Image>().color = color;
    }

    public void BackToMainMenu()
    {
        GameController.Instance.ReturnToMainMenu();
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}


}
