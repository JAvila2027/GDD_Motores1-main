using UnityEngine;
using System.Collections;

public class ProjectileDamage : MonoBehaviour
{
    [SerializeField] float damageAmount;
    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageableTarget = other.gameObject.GetComponent<IDamageable>();
        if (damageableTarget != null)
        {
            damageableTarget.TakeDamage(damageAmount);
        }
        Destroy(gameObject);
    }
    public void SetDamage(float newDamage)
    {
        damageAmount = newDamage;
    }
}
