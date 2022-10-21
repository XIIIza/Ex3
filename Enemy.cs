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
    private int _animatorHurt;
    private int _animatorIsDead;


    void Start()
    {
        _animatorHurt = Animator.StringToHash("Hurt");
        _animatorIsDead = Animator.StringToHash("IsDead");
        _rigidbody = GetComponent<Rigidbody2D>();
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        _animator.SetTrigger(_animatorHurt);

        _currentHealth -= damage;

        if(_currentHealth <= 0)
        {
            _animator.SetBool(_animatorIsDead, true);
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
