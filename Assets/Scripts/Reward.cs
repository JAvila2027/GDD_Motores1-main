using UnityEngine;

public class Reward : MonoBehaviour
{
    [Header("Configuración de la Recompensa")]
    public int valor = 10;
    public RewardType tipoRecompensa = RewardType.Moneda;
    public AudioClip sonidoRecoleccion;
    public GameObject efectoParticulas;

    [Header("Animación")]
    public bool rotarConstantemente = true;
    public float velocidadRotacion = 90f;
    public bool flotar = true;
    public float alturaFlotacion = 0.5f;
    public float velocidadFlotacion = 1f;

    private Vector3 posicionInicial;
    private AudioSource audioSource;

    public enum RewardType
    {
        Moneda,
        Gema,
        VidaExtra,
        Puntos
    }

    private void Start()
    {
        posicionInicial = transform.position;
        audioSource = GetComponent<AudioSource>();

        // Si no hay AudioSource, agregar uno
        if (audioSource == null && sonidoRecoleccion != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
    }

    private void Update()
    {
        // Rotación constante
        if (rotarConstantemente)
        {
            transform.Rotate(0, velocidadRotacion * Time.deltaTime, 0);
        }

        // Efecto de flotación
        if (flotar)
        {
            float nuevaY = posicionInicial.y + Mathf.Sin(Time.time * velocidadFlotacion) * alturaFlotacion;
            transform.position = new Vector3(transform.position.x, nuevaY, transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si es el jugador (igual que tu ZonaPeligrosa)
        if (other.CompareTag("Player"))
        {
            RecolectarRecompensa(other.gameObject);
        }
    }

    private void RecolectarRecompensa(GameObject jugador)
    {
        // Reproducir sonido
        if (sonidoRecoleccion != null && audioSource != null)
        {
            audioSource.PlayOneShot(sonidoRecoleccion);
        }

        // Crear efecto de partículas
        if (efectoParticulas != null)
        {
            Instantiate(efectoParticulas, transform.position, transform.rotation);
        }

        // Aplicar la recompensa según el tipo
        AplicarRecompensa(jugador);

        // Mostrar en consola
        Debug.Log($"Recompensa recolectada: {tipoRecompensa} +{valor}");

        // Destruir la recompensa
        Destroy(gameObject);
    }

    private void AplicarRecompensa(GameObject jugador)
    {
        switch (tipoRecompensa)
        {
            case RewardType.VidaExtra:
                // Dar vida extra al jugador
                SaludJugador salud = jugador.GetComponent<SaludJugador>();
                if (salud != null)
                {
                    salud.vida += valor;
                    Debug.Log($"¡Vida extra! Ahora tienes {salud.vida} vidas");
                }
                break;

            case RewardType.Moneda:
                // Sumar monedas a la UI
                if (SimpleScoreUI.Instance != null)
                {
                    SimpleScoreUI.Instance.SumarMonedas(valor);
                }
                Debug.Log($"¡Moneda recolectada! +{valor} monedas");
                break;

            case RewardType.Gema:
                // Sumar gemas a la UI
                if (SimpleScoreUI.Instance != null)
                {
                    SimpleScoreUI.Instance.SumarGemas(valor);
                }
                Debug.Log($"¡Gema recolectada! +{valor} gemas");
                break;

            case RewardType.Puntos:
                // Sumar puntos directamente
                if (SimpleScoreUI.Instance != null)
                {
                    SimpleScoreUI.Instance.SumarPuntos(valor);
                }
                Debug.Log($"¡Puntos recolectados! +{valor} puntos");
                break;
        }
    }

    // Método para configurar la recompensa desde código
    public void ConfigurarRecompensa(RewardType tipo, int valorRecompensa, Vector3 posicion)
    {
        tipoRecompensa = tipo;
        valor = valorRecompensa;
        transform.position = posicion;
        posicionInicial = posicion;
    }
}