using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class RewardManager : MonoBehaviour
{
    [Header("Prefabs de Recompensas")]
    public GameObject prefabMoneda;
    public GameObject prefabGema;
    public GameObject prefabVidaExtra;
    public GameObject prefabPuntos;

    [Header("Configuración de Distribución")]
    public int cantidadMonedas = 15;
    public int cantidadGemas = 5;
    public int cantidadVidasExtra = 2;
    public int cantidadPuntos = 8;

    [Header("Área de Distribución")]
    public Vector3 areaMinima = new Vector3(-50, 1, -50);
    public Vector3 areaMaxima = new Vector3(50, 5, 50);
    public LayerMask capasTerreno = 1; // Para verificar que no estén flotando

    [Header("Valores de Recompensas")]
    public int valorMoneda = 10;
    public int valorGema = 50;
    public int valorVidaExtra = 1;
    public int valorPuntos = 25;

    private List<GameObject> recompensasCreadas = new List<GameObject>();

    private void Start()
    {
        GenerarRecompensas();
    }

    public void GenerarRecompensas()
    {
        // Limpiar recompensas anteriores si existen
        LimpiarRecompensasExistentes();

        // Generar cada tipo de recompensa
        GenerarTipoRecompensa(prefabMoneda, cantidadMonedas, Reward.RewardType.Moneda, valorMoneda);
        GenerarTipoRecompensa(prefabGema, cantidadGemas, Reward.RewardType.Gema, valorGema);
        GenerarTipoRecompensa(prefabVidaExtra, cantidadVidasExtra, Reward.RewardType.VidaExtra, valorVidaExtra);
        GenerarTipoRecompensa(prefabPuntos, cantidadPuntos, Reward.RewardType.Puntos, valorPuntos);

        Debug.Log($"Recompensas generadas: {recompensasCreadas.Count} en total");
    }

    private void GenerarTipoRecompensa(GameObject prefab, int cantidad, Reward.RewardType tipo, int valor)
    {
        if (prefab == null) return;

        for (int i = 0; i < cantidad; i++)
        {
            Vector3 posicion = ObtenerPosicionAleatoria();
            GameObject nuevaRecompensa = Instantiate(prefab, posicion, Quaternion.identity);

            // Configurar la recompensa
            Reward reward = nuevaRecompensa.GetComponent<Reward>();
            if (reward != null)
            {
                reward.ConfigurarRecompensa(tipo, valor, posicion);
            }

            // Hacer hijo de este GameObject para organización
            nuevaRecompensa.transform.SetParent(this.transform);
            recompensasCreadas.Add(nuevaRecompensa);
        }
    }

    private Vector3 ObtenerPosicionAleatoria()
    {
        Vector3 posicionAleatoria;
        int intentos = 0;
        int maxIntentos = 50;

        do
        {
            posicionAleatoria = new Vector3(
                Random.Range(areaMinima.x, areaMaxima.x),
                Random.Range(areaMinima.y, areaMaxima.y),
                Random.Range(areaMinima.z, areaMaxima.z)
            );
            intentos++;
        }
        while (PosicionOcupada(posicionAleatoria) && intentos < maxIntentos);

        // Ajustar altura al terreno si es necesario
        posicionAleatoria = AjustarAlturaAlTerreno(posicionAleatoria);

        return posicionAleatoria;
    }

    private bool PosicionOcupada(Vector3 posicion)
    {
        // Verificar si hay algún objeto muy cerca
        Collider[] objetosCercanos = Physics.OverlapSphere(posicion, 2f);
        foreach (Collider obj in objetosCercanos)
        {
            if (obj.CompareTag("Reward") || obj.CompareTag("Obstacle"))
                return true;
        }
        return false;
    }

    private Vector3 AjustarAlturaAlTerreno(Vector3 posicion)
    {
        RaycastHit hit;
        if (Physics.Raycast(posicion + Vector3.up * 100, Vector3.down, out hit, 200f, capasTerreno))
        {
            return hit.point + Vector3.up * 1f; // 1 unidad por encima del suelo
        }
        return posicion;
    }

    private void LimpiarRecompensasExistentes()
    {
        foreach (GameObject recompensa in recompensasCreadas)
        {
            if (recompensa != null)
                DestroyImmediate(recompensa);
        }
        recompensasCreadas.Clear();
    }

    // Método para regenerar recompensas (útil para testing)
    [ContextMenu("Regenerar Recompensas")]
    public void RegenerarRecompensas()
    {
        GenerarRecompensas();
    }

    // Obtener estadísticas
    public int GetRecompensasRestantes()
    {
        int contador = 0;
        foreach (GameObject recompensa in recompensasCreadas)
        {
            if (recompensa != null) contador++;
        }
        return contador;
    }
}
