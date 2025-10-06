using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SwordAnimator : MonoBehaviour
{
    [SerializeField] Collider _attackCollider;
    [SerializeField] float _attackDuration;
    [SerializeField] float _attackMoveSpeed;
    [Header("Sword position")]
    [SerializeField] Vector3 _restartPosition;
    [SerializeField] Quaternion _restartRotation;
    [SerializeField] Vector3 _attackPosition;
    [SerializeField] Quaternion _attackRotation;

    private bool _isAttacking = false;
    private void Start()
    {
        transform.localPosition = _restartPosition;
        transform.localRotation = _restartRotation;
        _attackCollider.enabled = false;
    }
    public void StartAttack()
    {
        if (!_isAttacking)
        {
            StartCoroutine(AttackCoroutine());
        }
    }
    private IEnumerator AttackCoroutine()
    {
        _isAttacking = true;
        transform.localPosition = _restartPosition;
        transform.localRotation = _restartRotation;
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * _attackMoveSpeed;
            transform.localPosition = Vector3.Lerp(_restartPosition, _attackPosition, t);
            transform.localRotation = Quaternion.Lerp(_restartRotation, _attackRotation, t);
            yield return null;
        }
        _attackCollider.enabled = true;
        yield return new WaitForSeconds(_attackDuration);
        _attackCollider.enabled = false;

        t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * _attackMoveSpeed;
            transform.localPosition = Vector3.Lerp(_attackPosition, _restartPosition, t);
            transform.localRotation = Quaternion.Lerp(_attackRotation, _restartRotation, t);
            yield return null;
        }
        _isAttacking = false;
    }
}
