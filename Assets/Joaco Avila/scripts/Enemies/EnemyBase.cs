using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] float _maxHealth;
    [SerializeField] float _moveSpeed;
    [SerializeField] float _attackDamage;
    [SerializeField] float _pointsValue;
    [SerializeField] float _healthValue;

    public float MoveSpeed => _moveSpeed;
    public float PointsValue => _pointsValue;
}
