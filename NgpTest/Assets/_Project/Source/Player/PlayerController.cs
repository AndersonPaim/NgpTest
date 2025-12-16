using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private WeaponController _weaponController;
    [SerializeField] private float _moveSpeed;

    private PlayerInputActions _input;
    private Rigidbody _rb;

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
