using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleSceneChanger : MonoBehaviour
{
    // Métodos que puedes usar directamente en los botones

    // Ir a NivelBase por nombre
    public void IrANivelBase()
    {
        SceneManager.LoadScene("NivelBase");
    }

    // Ir a MenuPrincipal1 por nombre  
    public void IrAMenuPrincipal()
    {
        SceneManager.LoadScene("MenuPrincipal1");
    }
    // Ir a NivelBase1 por nombre
    public void IrANivelBase1()
    {
        SceneManager.LoadScene("NivelBase1");
    }

    // Ir a NivelBase por índice
    public void IrANivelBasePorIndice()
    {
        SceneManager.LoadScene(1);
    }

    // Ir a MenuPrincipal por índice
    public void IrAMenuPorIndice()
    {
        SceneManager.LoadScene(0);
    }

    // Recargar escena actual
    public void RecargarEscena()
    {
        Scene escenaActual = SceneManager.GetActiveScene();
        SceneManager.LoadScene(escenaActual.name);
    }

    // Salir del juego
    public void SalirJuego()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}