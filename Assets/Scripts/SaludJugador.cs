/*using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SaludJugador : MonoBehaviour
{
    public int vida = 3;
    public TextMeshProUGUI textoVida;

    void Start()
    {
        ActualizarTextoVida();
    }

    public void RecibirDa�o(int da�o)
    {
        vida -= da�o;
        ActualizarTextoVida();

        if (vida <= 0)
        {
            Morir();
        }
    }

    void ActualizarTextoVida()
    {
        if (textoVida != null)
        {
            textoVida.text = "Vidas: " + vida;
        }
    }

    void Morir()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void Update()
    {
        if (transform.position.y < -10f) // Ajust� el valor seg�n tu escena
        {
            Morir();
        }
    }

}*/
/*using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SaludJugador : MonoBehaviour
{
    public int vida = 3;
    public TextMeshProUGUI textoVida;
    public GameObject panelGameOver;

    private Animator anim;
    private AudioSource audioSource;

    public AudioClip sonidoImpacto;

    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        ActualizarTextoVida();

        if (panelGameOver != null)
        {
            panelGameOver.SetActive(false); // Oculta el panel al inicio
        }
    }

    void Update()
    {
        if (transform.position.y < -10f)
        {
            Morir();
        }
    }

    public void RecibirDa�o(int da�o)
    {
        vida -= da�o;
        ActualizarTextoVida();

        if (sonidoImpacto != null && audioSource != null)
        {
            audioSource.PlayOneShot(sonidoImpacto);
        }

        if (vida <= 0)
        {
            Morir();
        }
    }

    void ActualizarTextoVida()
    {
        if (textoVida != null)
        {
            textoVida.text = "Vidas: " + vida;
        }
    }

    void Morir()
    {
        if (anim != null)
        {
            anim.SetTrigger("Morir");
        }

        if (panelGameOver != null)
        {
            panelGameOver.SetActive(true);
        }

        Time.timeScale = 0f; // Pausa el juego
    }
}*/
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SaludJugador : MonoBehaviour
{
    [Header("Configuraci�n de Salud")]
    public int vida = 3;
    public int vidaMaxima = 10; // L�mite m�ximo de vidas
    public TextMeshProUGUI textoVida;
    public GameObject panelGameOver;

    [Header("Efectos")]
    private Animator anim;
    private AudioSource audioSource;
    public AudioClip sonidoImpacto;
    public AudioClip sonidoVidaExtra; // Nuevo sonido para cuando gana vida

    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        ActualizarTextoVida();

        if (panelGameOver != null)
        {
            panelGameOver.SetActive(false);
        }
    }

    void Update()
    {
        if (transform.position.y < -10f)
        {
            Morir();
        }
    }

    public void RecibirDa�o(int da�o)
    {
        vida -= da�o;
        ActualizarTextoVida();

        if (sonidoImpacto != null && audioSource != null)
        {
            audioSource.PlayOneShot(sonidoImpacto);
        }

        if (vida <= 0)
        {
            Morir();
        }
    }

    // NUEVO: M�todo para recibir vidas extra desde las recompensas
    public void GanarVida(int vidasExtra)
    {
        int vidasAntes = vida;
        vida = Mathf.Min(vida + vidasExtra, vidaMaxima); // No exceder el m�ximo

        // Solo reproducir sonido si realmente gan� vidas
        if (vida > vidasAntes)
        {
            if (sonidoVidaExtra != null && audioSource != null)
            {
                audioSource.PlayOneShot(sonidoVidaExtra);
            }

            Debug.Log($"�Vida extra! Ahora tienes {vida} vidas");
        }

        ActualizarTextoVida();
    }

    // Hacer p�blico para que las recompensas puedan actualizar la UI
    public void ActualizarTextoVida()
    {
        if (textoVida != null)
        {
            textoVida.text = "Vidas: " + vida;
        }
    }

    void Morir()
    {
        if (anim != null)
        {
            anim.SetTrigger("Morir");
        }

        if (panelGameOver != null)
        {
            panelGameOver.SetActive(true);
        }

        Time.timeScale = 0f;
    }

    // NUEVO: M�todo para obtener la vida actual (�til para otras scripts)
    public int GetVidaActual()
    {
        return vida;
    }

    // NUEVO: M�todo para verificar si est� vivo
    public bool EstaVivo()
    {
        return vida > 0;
    }
}
