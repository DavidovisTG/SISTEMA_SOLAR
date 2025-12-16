using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDynamicElements : MonoBehaviour
{
    public static UIDynamicElements Instance;

    private GameObject UIPanel;

    [SerializeField] TextMeshProUGUI livesTextComponent;
    [SerializeField] TextMeshProUGUI targetNameTextComponent;
    [SerializeField] TextMeshProUGUI scoreTextComponent;
    [SerializeField] Button backToMenuButtonComponent;
    
    [SerializeField] GameObject dynamicUIBox;

    //OTRAS PROPIEDADES: VALORES POR DEFECTO
    private float initialDynamicUIBoxHeight;



    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("destroy user interface");
            Destroy(Instance.gameObject);
            Instance = null;
        }    
        if (Instance == null)
        {
            Instance = this;
        }
        DontDestroyOnLoad(Instance.gameObject);


        UIPanel = transform.GetChild(0).gameObject;
        initialDynamicUIBoxHeight = dynamicUIBox.GetComponent<RectTransform>().sizeDelta.y;
    }

    private void Start()
    {

    }


    ////////UI ANIMATION
    public IEnumerator startAnimation()
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
            TMPUI.text = ""+i;
            yield return StartCoroutine(DynamicBoxHeightAnimate(initialDynamicUIBoxHeight));
        }

        TMPUI.text = "¡Ya!";
        TR.sizeDelta = new Vector2(TR.sizeDelta.x, initialDynamicUIBoxHeight);
    }

    private IEnumerator DynamicBoxHeightAnimate(float height)
    {
        RectTransform box = dynamicUIBox.GetComponent<RectTransform>();
        float duration = 0.20f;

        float t = 0f;
        
        while (t < duration)
        {
            t += Time.deltaTime;
            float h = Mathf.Lerp( 0f, height, t / duration);
            box.sizeDelta = new Vector2(box.sizeDelta.x, h);
            yield return null; //PARECE INNECESARIO; SI SE BORRA NO ACTUALIZA FRAME
        }

        yield return new WaitForSecondsRealtime(1f - (duration*3)-Time.deltaTime);

        t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float h = Mathf.Lerp(height, 0f, t / duration);
            box.sizeDelta = new Vector2(box.sizeDelta.x, h);
            yield return null; //PARECE INNECESARIO; SI SE BORRA NO ACTUALIZA FRAME
        }

        yield return new WaitForSecondsRealtime(duration-Time.deltaTime);
    }
    ////////



    public void LivesNumberTextChange(string text)
    {
        livesTextComponent.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text;
    }
    public void TargetSupertextChange(string superText)
    {
        targetNameTextComponent.text = superText;
    }
    public void TargetNameTextChange(string targetName) 
    {
        targetNameTextComponent.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = targetName;
    }
    public void ScoreNumberTextChange(string scoreText)
    {
        scoreTextComponent.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = scoreText;
    }

    public GameObject ReturnUIPanel()
    {
        return UIPanel;
    }



    public void HideAllUI()
    {
        for (int c = 0; c < UIPanel.transform.childCount; c++)
        {
            UIPanel.transform.GetChild(c).gameObject.SetActive(false);
        }
    }
    public void ShowAllUI()
    {
        for (int c = 0; c < UIPanel.transform.childCount; c++)
        {
            UIPanel.transform.GetChild(c).gameObject.SetActive(true);
        }
    }

    public void ShowLives()
    {
        livesTextComponent.gameObject.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    
}
