using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HealthSystem : MonoBehaviour, IHealable, IDamageable
{

    [Header("Health settings")]
    [SerializeField] float maxHealth;
    private float currentHealth;

    [Header("Regeneration settings")]
    [SerializeField] bool canRegenerate = true;
    [SerializeField] float regenerationRate;
    [SerializeField] float regenerationInterval;
    public float CurrentHealth => currentHealth;
    [Header("UI health")]
    [SerializeField] HealthBar _healthBar;
    private EnemyBase _enemyBase;

    void Awake()
    {

        currentHealth = maxHealth;
        if (_healthBar != null)
        {
            _healthBar.UpdateHealthBar(maxHealth, currentHealth);
        }
        if (canRegenerate)
        {
            StartCoroutine(RegenerateHealth());
        }

    }
    void Start()
    {
        _enemyBase = GetComponent<EnemyBase>();
    }
    public virtual void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (_healthBar != null)
        {
            _healthBar.UpdateHealthBar(maxHealth, currentHealth);
        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        if (_healthBar != null)
        {
            _healthBar.UpdateHealthBar(maxHealth, currentHealth);
        }
    }
    private IEnumerator RegenerateHealth()
    {
        while (true)
        {
            yield return new WaitForSeconds(regenerationRate);
            if (currentHealth < maxHealth)
            {
                currentHealth = Mathf.Min(CurrentHealth + regenerationRate, maxHealth);
                if (_healthBar != null)
                {
                    _healthBar.UpdateHealthBar(maxHealth, CurrentHealth);
                }
            }
        }
    }
    protected virtual void Die()
    {

        Destroy(gameObject);
    }
}