using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IDamageable
{
    public Action OnDeath;
    
    private const string SpeedAnimationName = "Speed";
    private const string RotationAnimationName = "Rotation";
    
    [SerializeField] private WeaponController _weaponController;
    [SerializeField] private PlayerHPUI _hpUI;
    [SerializeField] private DamageFeedback _damageFeedback;
    [SerializeField] private Animator _anim;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private int _hp;

    private PlayerInputActions _input;
    private Rigidbody _rb;
    private Quaternion _previousRotation;
    private int _initialHp;

    public void TakeDamage(int damage)
    {
        _hp -= damage;

        if (_hp <= 0)
        {
            _damageFeedback.Kill();
            OnDeath?.Invoke();
        }
        else
        {
            _damageFeedback.Damage();
        }
        
        _hpUI.UpdateHpBar(_hp, _initialHp);
    }

    public void Heal(int amount)
    {
        _hp = Mathf.Clamp(_hp + amount, 0, _initialHp);
        _hpUI.UpdateHpBar(_hp, _initialHp);
    }

    private void Start()
    {
        Initialize();
        SetupEvents();
    }

    private void FixedUpdate()
    {
        Movement(_input.Player.Move.ReadValue<Vector2>());
        Look(_input.Player.Look.ReadValue<Vector2>());
    }

    private void OnDestroy()
    {
        DestroyEvents();
    }

    private void Initialize()
    {
        _rb = GetComponent<Rigidbody>();
        _input = new PlayerInputActions();
        _input.Enable();
        _initialHp = _hp;
    }

    private void SetupEvents()
    {
        _input.Player.Attack.performed += PrimaryAttack;
    }

    private void DestroyEvents()
    {
        _input.Player.Attack.performed -= PrimaryAttack;
    }

    private void Movement(Vector2 movement)
    {
        _anim.SetFloat(SpeedAnimationName, movement.magnitude);
        movement *= _moveSpeed;
        _rb.linearVelocity = new Vector3(movement.x, _rb.linearVelocity.y, movement.y);
    }

    private void Look(Vector3 mousePosition)
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(mousePosition);
        Plane plane = new(Vector3.up, transform.position);

        if (plane.Raycast(mouseRay, out float hitDist))
        {
            Vector3 hitPoint = mouseRay.GetPoint(hitDist);
            transform.LookAt(hitPoint);
            _anim.SetFloat(RotationAnimationName, _previousRotation != transform.rotation ? 1 : 0);
            _previousRotation = transform.rotation;
        }
    }

    private void PrimaryAttack(InputAction.CallbackContext ctx)
    {
        _weaponController.Shoot();
    }
}
