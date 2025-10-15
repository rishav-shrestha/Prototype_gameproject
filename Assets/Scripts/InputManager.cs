using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public float tapThreshold = 0.2f;
    public float dragThreshold = 20f;

    private bool isDragging = false;
    private Vector2 startPos;
    private float startTime;

    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        HandleMouse();
#endif

#if UNITY_ANDROID || UNITY_IOS
        HandleTouch();
#endif
    }

    void HandleMouse()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            startPos = Mouse.current.position.ReadValue();
            startTime = Time.time;
            isDragging = false;
        }

        if (Mouse.current.leftButton.isPressed)
        {
            float distance = Vector2.Distance(startPos, Mouse.current.position.ReadValue());
            if (distance > dragThreshold) isDragging = true;
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            float duration = Time.time - startTime;
            if (!isDragging && duration <= tapThreshold)
            {
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                worldPos.z = 0;
                TryInteract(worldPos);
            }
        }
    }

    void HandleTouch()
    {
        var touch = Touchscreen.current.primaryTouch;
        if (touch.press.wasPressedThisFrame)
        {
            startPos = touch.position.ReadValue();
            startTime = Time.time;
            isDragging = false;
        }

        if (touch.press.isPressed)
        {
            float distance = Vector2.Distance(startPos, touch.position.ReadValue());
            if (distance > dragThreshold) isDragging = true;
        }

        if (touch.press.wasReleasedThisFrame)
        {
            float duration = Time.time - startTime;
            if (!isDragging && duration <= tapThreshold)
            {
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(touch.position.ReadValue());
                worldPos.z = 0;
                TryInteract(worldPos);
            }
        }
    }

    void TryInteract(Vector3 worldPos)
    {
        Collider2D hit = Physics2D.OverlapPoint(worldPos);
        if (hit != null && hit.TryGetComponent(out FarmTile tile))
        {
            tile.OnTap();
        }
    }
}
