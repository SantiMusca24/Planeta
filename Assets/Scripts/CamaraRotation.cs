using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraRotation : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Orbit Settings")]
    public float xSpeed = 120f;
    public float ySpeed = 120f;

    [Header("Angle Limits")]
    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    [Header("Zoom")]
    public float scrollSpeed = 10f;
    public float minDistance = 5f;
    public float maxDistance = 20f;

    [Header("Initial Settings")]
    public float initialXAngle = 0f;
    public float initialYAngle = 30f;
    public float initialDistance = 10f;

    [Header("Smooth Settings")]
    public bool useSmoothRotation = true;
    public float positionSmoothTime = 0.15f;
    public float rotationSmoothTime = 0.1f;

    private float x;
    private float y;
    private float distance;

    private Vector3 currentVelocity;
    private Quaternion currentRotation;

    void Start()
    {
        if (target == null)
        {
            Debug.LogWarning("No target assigned to OrbitCamera.");
            return;
        }

        x = initialXAngle;
        y = Mathf.Clamp(initialYAngle, yMinLimit, yMaxLimit);
        distance = Mathf.Clamp(initialDistance, minDistance, maxDistance);
        currentRotation = Quaternion.Euler(y, x, 0);

        UpdateCameraPosition(true);
    }

    void LateUpdate()
    {
        if (target == null) return;

        if (Input.GetMouseButton(0))
        {
            x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
            y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;
            y = Mathf.Clamp(y, yMinLimit, yMaxLimit);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance = Mathf.Clamp(distance - scroll * scrollSpeed, minDistance, maxDistance);

        UpdateCameraPosition(false);
    }

    private void UpdateCameraPosition(bool instant)
    {
        Quaternion targetRotation = Quaternion.Euler(y, x, 0);
        Vector3 desiredPosition = targetRotation * new Vector3(0, 0, -distance) + target.position;

        // Posición (siempre suavizada, a menos que sea instant)
        if (instant)
        {
            transform.position = desiredPosition;
            transform.rotation = targetRotation;
            currentRotation = targetRotation;
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, positionSmoothTime);

            if (useSmoothRotation)
            {
                currentRotation = Quaternion.Slerp(currentRotation, targetRotation, Time.deltaTime / rotationSmoothTime);
                transform.rotation = currentRotation;
            }
            else
            {
                transform.rotation = targetRotation;
                currentRotation = targetRotation;
            }
        }
    }
}
