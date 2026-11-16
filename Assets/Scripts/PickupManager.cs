using UnityEngine;

public class PickupManager : MonoBehaviour
{
    private int pickupsRemaining;

    private void Start()
    {
        pickupsRemaining = FindObjectsByType<TimePickup>(
            FindObjectsSortMode.None).Length;
    }

    public void PickupCollected()
    {
        pickupsRemaining--;

        Debug.Log("Pickups remaining: " + pickupsRemaining);

        if (pickupsRemaining <= 0)
        {
            FindFirstObjectByType<GameManager>().Win();
        }
    }
}