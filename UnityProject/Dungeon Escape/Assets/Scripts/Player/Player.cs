using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using Debug = UnityEngine.Debug;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour, IDamageadle
{
    public int diamonds;
    
    private Rigidbody2D _rigid;
    
    [SerializeField] 
    private float _jumpForce = 5.0f;
    
    private bool _resetJump;
    
    [SerializeField]
    private float _speed = 5.0f;

    private PlayerAnimation _playerAnim;
    
    private SpriteRenderer _playerSprite;
    private SpriteRenderer _swordArcSprite;

    private bool _grounded = false;

    public int Health { get; set; }

    private void Start() {
        _rigid = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<PlayerAnimation>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
        _swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        Health = 4;
    }

    private void Update() 
    {
        Movement();

        if (CrossPlatformInputManager.GetButtonDown("A_Button") && IsGrounded() == true)
        {
            _playerAnim.Attack();
        }
    }
    void Movement()
    {
        float move = CrossPlatformInputManager.GetAxis("Horizontal"); //Input.GetAxisRaw("Horizontal");
        
        _grounded = IsGrounded();
        
        if (move > 0)
        {
            Flip(true);
        } else if (move < 0)
        {
            Flip(false);
        } 
        
        
        if (CrossPlatformInputManager.GetButtonDown("B_Button") && IsGrounded())
        {
            //Debug.Log("Jump");
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
            StartCoroutine(ResetJumpRoutine());
            _playerAnim.Jump(true);
        }
        _rigid.velocity = new Vector2(move * _speed, _rigid.velocity.y);
        _playerAnim.Move(move);
    }

    
    bool IsGrounded()
    {
        
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, .75f, 1<<8);
        Debug.DrawRay(transform.position, Vector3.down, Color.green);
        if (hitInfo.collider  != null) {
            if (_resetJump == false)
            {
                _playerAnim.Jump(false);
                return true;
            }
        }
        
        return false;
    }

    void Flip(bool facingRight)
    {
        if (facingRight == true)
        {
            _playerSprite.flipX = false;
            _swordArcSprite.flipX = false;
            _swordArcSprite.flipY = false;

            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = -.15f;

            _swordArcSprite.transform.localPosition = newPos;

        } else if (facingRight == false)
        {
            _playerSprite.flipX = true;
            _swordArcSprite.flipX = true;
            _swordArcSprite.flipY = true;

            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = -.3f;

            _swordArcSprite.transform.localPosition = newPos;
        } 
    }
    
    IEnumerator ResetJumpRoutine()
    {
        _resetJump = true;
        yield return new WaitForSeconds(0.1f);
        _resetJump = false;
    }

    public void Damage()
    {
        if (Health < 1)
        {
            return;
        }
        Debug.Log("Player:Damage()");
        Health--;
        UIManager.Instance.UpdateLives(Health);
        if (Health < 1)
        {
            _playerAnim.Death();
        }
    }

    public void AddGams(int amount)
    {
        diamonds += amount;
        UIManager.Instance.UpdateGemCount(diamonds);
    }
}

