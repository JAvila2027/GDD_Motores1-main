using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void Reintentar()
    {
        Time.timeScale = 1f; // Reactiva el juego
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reinicia la escena actual
    }
}
