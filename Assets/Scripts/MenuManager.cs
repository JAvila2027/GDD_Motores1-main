using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void IniciarJuego()
    {
        SceneManager.LoadScene("NivelBase"); // Cambiá "Nivel1" por el nombre real de tu primera escena
    }
}
