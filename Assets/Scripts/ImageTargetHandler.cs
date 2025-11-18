using UnityEngine;
using Vuforia;

public class ImageTargetHandler : MonoBehaviour
{
    private ObserverBehaviour ob;
    private GameController gc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ob = GetComponent<ObserverBehaviour>();
        gc = FindFirstObjectByType<GameController>();

        if (ob)
        {
            ob.OnTargetStatusChanged += OnTargetStatusChanged;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        if (status.Status == Status.TRACKED)
        {
            gc.OnImageTargetFound(behaviour.TargetName);
        }
    }
}
