using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IDamageable
{
    [SerializeField] private WeaponController _weaponController;
    [SerializeField] private DamageFeedback _damageFeedback;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private int _hp;

    private PlayerInputActions _input;
    private Rigidbody _rb;

    public void TakeDamage(int damage)
    {
        _hp -= damage;

        if (_hp <= 0)
        {
            _damageFeedback.Kill();
        }
        else
        {
            _damageFeedback.Damage();
        }
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
        }
    }

    private void PrimaryAttack(InputAction.CallbackContext ctx)
    {
        _weaponController.Shoot();
    }
}
