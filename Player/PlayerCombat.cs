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

    public void Attack()
    {
        _animator.SetTrigger("Attack");

        Collider2D[] hitEnemys =  Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyLayers);

        foreach (var enemy in hitEnemys)
        {
            enemy.GetComponent<Enemy>().TakeDamage(_attackPower);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null)
            return;

        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange); 
    }
}
