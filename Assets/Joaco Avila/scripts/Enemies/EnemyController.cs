using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] float _attackRange;



    private Transform _playerTransform;
    private UnityEngine.AI.NavMeshAgent _agent;
    private EnemyAttacker _attacker;
    private IDamageable _targetDamageable;


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

            IDamageable playerHealth = _playerTransform.GetComponent<IDamageable>();
            if (playerHealth != null)
            {
                _attacker.Attack(playerHealth);
            }
        }

    }
}
