using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] float _attackRange;

    private Transform _playerTransform;
    private UnityEngine.AI.NavMeshAgent _agent;
    private EnemyAttacker _attacker;
    private IDamageable _playerDamageable;

    private void Awake()
    {
        _agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        _attacker = GetComponent<EnemyAttacker>();
    }

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            _playerTransform = playerObject.transform;
            _playerDamageable = _playerTransform.GetComponent<IDamageable>();
        }
    }

    private void Update()
    {
        if (_playerTransform == null)
        {
            return;
        }

        _agent.SetDestination(_playerTransform.position);
        float distanceToPlayer = Vector3.Distance(transform.position, _playerTransform.position);

        if (distanceToPlayer <= _attackRange)
        {
            if (_playerDamageable != null)
            {
                _agent.SetDestination(transform.position);
                _attacker.Attack(_playerDamageable);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (_attackRange > 0)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _attackRange);
        }
    }
}
