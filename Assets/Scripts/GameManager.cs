using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject winPanel;
    public GameObject failPanel;
    public GameObject player;
    public GameObject enemy;

    private bool gameEnded = false;

    public bool IsGameEnded => gameEnded;
    


    public void Win()
{
    if (gameEnded) return;
    gameEnded = true;

    winPanel.SetActive(true);
    FreezeGame();

    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;
}

public void Fail()
{
    if (gameEnded) return;
    gameEnded = true;

    failPanel.SetActive(true);
    FreezeGame();

    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;
}


    void FreezeGame()
{
    // stop player movement
    player.GetComponent<CharacterController>().enabled = false;
    player.GetComponent<StarterAssets.FirstPersonController>().enabled = false;

    // stop enemy movement
    if (enemy.TryGetComponent(out UnityEngine.AI.NavMeshAgent agent))
    {
        agent.isStopped = true;
    }
}


    public void Restart()
    {
        Debug.Log("RESTART PRESSED");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
