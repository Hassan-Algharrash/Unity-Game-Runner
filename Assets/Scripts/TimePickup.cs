using UnityEngine;

public class TimePickup : MonoBehaviour
{
    public float bonusTime = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TimerSystem timer = FindFirstObjectByType<TimerSystem>();

            if (timer != null)
            {
                timer.timeRemaining += bonusTime;
            }

            Destroy(gameObject);
        }
    }

    void Update()
{
    transform.Rotate(0, 90 * Time.deltaTime, 0);
}

}
