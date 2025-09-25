using UnityEngine;

public class ZonaPeligrosa : MonoBehaviour
{
    public int da�o = 1;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SaludJugador salud = other.GetComponent<SaludJugador>();
            if (salud != null)
            {
                salud.RecibirDa�o(da�o);
            }
        }
    }
}
