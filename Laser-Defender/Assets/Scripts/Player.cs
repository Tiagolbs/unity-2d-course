using System;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
internal struct Padding
{
    public float left;
    public float right;
    public float top;
    public float bottom;
}

public class Player : MonoBehaviour
{
    [SerializeField] private float shipSpeed = 10f;
    [SerializeField] private Padding padding;

    private Vector2 rawInput;
    private Vector2 minBounds;
    private Vector2 maxBounds;
    private Shooter shooter;

    private void Awake()
    {
        shooter = GetComponent<Shooter>();
    }

    private void Start()
    {
        InitBounds();
    }

    private void Update()
    {
        Move();
    }

    private void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    private void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    private void Move()
    {
        Vector2 delta = rawInput * (shipSpeed * Time.deltaTime);
        
        Vector2 position = transform.position;
        Vector2 newPosition = new Vector2
        (
            Mathf.Clamp(position.x + delta.x, minBounds.x + padding.left, maxBounds.x - padding.right),
            Mathf.Clamp(position.y + delta.y, minBounds.y + padding.bottom, maxBounds.y - padding.top)
        );
        
        transform.position = newPosition;
    }

    private void OnFire(InputValue value)
    {
        if (shooter != null)
        {
            shooter.isFiring = value.isPressed;
        }
    }
}
