using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    NavMeshAgent agent;
    private EnemyBase _enemyBase;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        _enemyBase = GetComponent<EnemyBase>();
        if (agent != null)
        {
            agent.updateRotation = false;
            agent.updateUpAxis = false;
        }
        if (agent != null && _enemyBase != null)
        {
            agent.speed = _enemyBase.MoveSpeed;
        }
    }
    private void Start()
    {

    }
    public void MoveToTarget(Transform target)
    {
        if (target != null && agent != null)
        {
            agent.SetDestination(target.position);
        }
    }
}