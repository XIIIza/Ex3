using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _attackPower;
    [Space]
    [SerializeField] private Animator _animator;
    [SerializeField] private LayerMask _enemyLayers;

    public void Attack(int attackHash)
    {
        _animator.SetTrigger(attackHash);

        Collider2D[] hitEnemys = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyLayers);

        foreach (Collider2D hitEnemy in hitEnemys)
        {
            if (hitEnemy.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(_attackPower);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null)
            return;

        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange); 
    }
}
