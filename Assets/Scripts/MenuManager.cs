using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void IniciarJuego()
    {
        SceneManager.LoadScene("NivelBase"); // Cambi� "Nivel1" por el nombre real de tu primera escena
    }
}
