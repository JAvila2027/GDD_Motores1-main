using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class HostileAi : MonoBehaviour
{
    [Header("References")]
    [SerializeField] NavMeshAgent navAgent;
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject projectilePrefab;
    [Header("Layers")]
    [SerializeField] LayerMask terrainLayer;
    [SerializeField] LayerMask playerLayerMask;
    [Header("Patrol Settings")]
    [SerializeField] float patrolRadius = 10f;
    private Vector3 currentPatrolPoint;
    private bool hasPatrolPoint;
    [Header("Combat Settings")]
    [SerializeField] float attackCooldown = 1f;
    [SerializeField] float projectileDamageValue;
    private bool isOnAttackCooldown;
    [SerializeField] float forwardShotForce = 10f;
    [SerializeField] float verticalShotForce = 5f;
    [Header("Detection Ranges")]
    [SerializeField] float visionRange = 20f;
    [SerializeField] float engagementRange = 10f;

    private bool isPlayerVisible;
    private bool isPlayerInRange;

    private void Awake()
    {
        if (playerTransform == null)
        {
            GameObject playerObj = GameObject.Find("Player");
            if (playerObj != null)
            {
                playerTransform = playerObj.transform;
            }
        }

        if (navAgent == null)
        {
            navAgent = GetComponent<NavMeshAgent>();
        }
    }

    private void Update()
    {
        DetectPlayer();
        UpdateBehaviourState();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, engagementRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }

    private void DetectPlayer()
    {
        isPlayerVisible = Physics.CheckSphere(transform.position, visionRange, playerLayerMask);
        isPlayerInRange = Physics.CheckSphere(transform.position, engagementRange, playerLayerMask);
    }

    private void FireProjectile()
    {
        if (projectilePrefab == null || firePoint == null) return;

        GameObject projectileGO = Instantiate(projectilePrefab, firePoint.position,Quaternion.identity);
        Rigidbody projectileRb = projectileGO.GetComponent<Rigidbody>();
        ProjectileDamage projectileScript = projectileGO.GetComponent<ProjectileDamage>();

        if (projectileScript != null)
        {
            projectileScript.SetDamage(projectileDamageValue);
        }
        if (projectileRb != null)
        {
            projectileRb.AddForce(transform.forward * forwardShotForce, ForceMode.Impulse);
            projectileRb.AddForce(transform.up * verticalShotForce, ForceMode.Impulse);
        }
        //Rigidbody projectileRb = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        //projectileRb.AddForce(transform.forward * forwardShotForce, ForceMode.Impulse);
        //projectileRb.AddForce(transform.up * verticalShotForce, ForceMode.Impulse);
        //Destroy(projectileRb.gameObject, 3f);

        Destroy(projectileGO, 3f);
    }

    private void FindPatrolPoint()
    {
        float randomX = Random.Range(-patrolRadius, patrolRadius);
        float randomZ = Random.Range(-patrolRadius, patrolRadius);
        Vector3 potentialPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(potentialPoint, -transform.up, 2f, terrainLayer))
        {
            currentPatrolPoint = potentialPoint;
            hasPatrolPoint = true;
        }
    }

    private IEnumerator AttackCooldownRoutine()
    {
        isOnAttackCooldown = true;
        yield return new WaitForSeconds(attackCooldown);
        isOnAttackCooldown = false;
    }
    private void PerformPatrol()
    {
        navAgent.updateRotation = true;
        if (!hasPatrolPoint)
            FindPatrolPoint();
        if (hasPatrolPoint)
            navAgent.SetDestination(currentPatrolPoint);
        if (Vector3.Distance(transform.position, currentPatrolPoint) < 1f)
            hasPatrolPoint = false;
    }

    private void PerformChase()
    {
        navAgent.updateRotation = true;
        if (playerTransform != null)
        {
            navAgent.SetDestination(playerTransform.position);
        }
    }
    private void PerformAttack()
    {
        navAgent.SetDestination(transform.position);
        navAgent.updateRotation = false;

        if (playerTransform != null)
        {
            Vector3 direction = playerTransform.position - transform.position;
            direction.y = 0;
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            float rotationSpeed = 5f;

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                lookRotation,
                Time.deltaTime * rotationSpeed
            );
        }
        if (!isOnAttackCooldown)
        {
            FireProjectile();
            StartCoroutine(AttackCooldownRoutine());
        }
    }
    private void UpdateBehaviourState()
    {
        if (!isPlayerVisible && !isPlayerInRange)
        {
            PerformPatrol();
        }
        else if (isPlayerVisible && !isPlayerInRange)
        {
            PerformChase();
        }
        else if (isPlayerVisible && isPlayerInRange)
        {
            PerformAttack();
        }
    }
}
