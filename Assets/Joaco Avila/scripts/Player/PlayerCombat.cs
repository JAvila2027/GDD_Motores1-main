using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] SwordAnimator _swordAnimator;
    public void StartAttack()
    {
        _swordAnimator.StartAttack();
    }
}
