using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] float _moveSpeed;
    [SerializeField] float _pointsValue;

    public float MoveSpeed => _moveSpeed;
    public float PointsValue => _pointsValue;
}
