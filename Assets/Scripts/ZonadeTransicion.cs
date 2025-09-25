using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioDeEscenaPorZona : MonoBehaviour
{
    public string nombreDeLaEscenaDestino = "NivelBase1";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(nombreDeLaEscenaDestino);
        }
    }
}