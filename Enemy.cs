using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(WayPointMovement))]

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private float _maxHealth = 100;
    private float _currentHealth;
    private Rigidbody2D _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        _animator.SetTrigger("Hurt");

        _currentHealth -= damage;

        if(_currentHealth <= 0)
        {
            _animator.SetBool("IsDead", true);
            GetComponent<WayPointMovement>().enabled = false;
            Die();
        }
    }

    private void Die()
    {
        _rigidbody.bodyType = RigidbodyType2D.Static;
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        Debug.Log("Enemy dead");
    }
}
