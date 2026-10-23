using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{

    public TMP_Text timerText;
    public float maximumTime = 120f;

    private float startTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timerText.text = "0";
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        string timerString =  Mathf.Floor(maximumTime - (Time.time - startTime)).ToString();
        if (timerText != null) timerText.text = timerString;
    }
}
