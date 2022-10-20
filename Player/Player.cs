using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D), typeof(PlayerCombat))]

public class Player : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _speed = 0f;
    [SerializeField] private float _jumpForce = 0f;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private LayerMask IsGround;
    [SerializeField] private float GroundRadius = 0.5f;

    private bool _facingRight = true;
    private bool OnGround = false;
    private Rigidbody2D _rigidbody;
    private PlayerCombat _playerCombat;
    private int _animatorIsJumpHash;
    private int _animatorSpeedHash;
    private int _animatorVerticalSpeedHash;

    void Start()
    {
        _animatorIsJumpHash = Animator.StringToHash("isJump");
        _animatorSpeedHash = Animator.StringToHash("Speed");
        _animatorVerticalSpeedHash = Animator.StringToHash("VerticalSpeed");
        _playerCombat = GetComponent<PlayerCombat>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    
    void Update()
    {      
        float inputAxisY = _rigidbody.velocity.y;
        float movement = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _playerCombat.Attack();
        }

        if (Input.GetKeyDown(KeyCode.Space) && OnGround)
        {
            _rigidbody.AddForce(new Vector3(0, _jumpForce, 0));
        }

        OnGround = Physics2D.OverlapCircle(GroundCheck.position, GroundRadius, IsGround);

        _animator.SetBool(_animatorIsJumpHash, !OnGround);
        _animator.SetFloat(_animatorSpeedHash, Mathf.Abs(movement));
        _animator.SetFloat(_animatorVerticalSpeedHash, inputAxisY);

        transform.position += new Vector3(movement, 0, 0) * _speed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        float inputHorizontal = Input.GetAxisRaw("Horizontal");

        if (inputHorizontal > 0 && !_facingRight)
        {
            Flip();
        }

        if (inputHorizontal < 0 && _facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        _facingRight = !_facingRight;
    }
}