using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
public class SwordDamageDealer : MonoBehaviour
{
    [SerializeField] float _swordDamage;
    private void OnTriggerEnter(Collider other)
    {
        IDamageable target = other.GetComponent<IDamageable>();
        if (target != null)
        {
            target.TakeDamage(_swordDamage);
        }
    }
}
