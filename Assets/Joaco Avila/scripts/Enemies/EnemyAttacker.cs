using UnityEngine;

public class EnemyAttacker : MonoBehaviour
{
    [Header("Attack settings")]
    [SerializeField] float _attackDamage;
    [SerializeField] float _attackCooldown;
    private float _lastAttackTime;
    private Transform _playerTransform;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            _playerTransform = playerObject.transform;
        }
    }

    public void Attack(IDamageable target)
    {
        if (Time.time >= _lastAttackTime + _attackCooldown)
        {
            target.TakeDamage(_attackDamage);
            _lastAttackTime = Time.time;
        }
    }
}