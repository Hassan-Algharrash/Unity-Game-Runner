using UnityEngine;
using TMPro;

public class TimerSystem : MonoBehaviour
{
    public float timeRemaining = 60f;
    public TMP_Text timerText;
    private bool timerRunning = true;

    void Update()
    {
        if (!timerRunning) return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining < 0)
        {
            timeRemaining = 0;
            timerRunning = false;
            FindFirstObjectByType<GameManager>().Fail();
        }

        timerText.text = Mathf.Ceil(timeRemaining).ToString();
    }
}
