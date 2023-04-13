using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameOverHandler gameOverHandler;

    //turns off the player gameobject and ends the game
    public void Crash()
    {
        gameObject.SetActive(false);
        gameOverHandler.EndGame();
    }

    //sets player visible again
    public void Revive()
    {
        gameObject.SetActive(true);
    }
}
