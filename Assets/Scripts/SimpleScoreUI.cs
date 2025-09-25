using UnityEngine;
using TMPro;

public class SimpleScoreUI : MonoBehaviour
{
    [Header("UI Textos")]
    public TextMeshProUGUI textoPuntos;
    public TextMeshProUGUI textoMonedas;
    public TextMeshProUGUI textoGemas;

    [Header("Contadores")]
    public int puntos = 0;
    public int monedas = 0;
    public int gemas = 0;

    // Singleton para acceso fácil desde las recompensas
    public static SimpleScoreUI Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ActualizarUI();
    }

    // Método para sumar puntos (llamado desde Reward)
    public void SumarPuntos(int cantidad)
    {
        puntos += cantidad;
        ActualizarUI();
    }

    public void SumarMonedas(int cantidad)
    {
        monedas += cantidad;
        puntos += cantidad; // Las monedas también dan puntos
        ActualizarUI();
    }

    public void SumarGemas(int cantidad)
    {
        gemas += cantidad;
        puntos += cantidad * 2; // Las gemas valen doble
        ActualizarUI();
    }

    void ActualizarUI()
    {
        if (textoPuntos != null)
            textoPuntos.text = "Puntos: " + puntos;

        if (textoMonedas != null)
            textoMonedas.text = "Monedas: " + monedas;

        if (textoGemas != null)
            textoGemas.text = "Gemas: " + gemas;
    }
}