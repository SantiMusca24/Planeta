using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraRotation : MonoBehaviour
{
     [Header("Target")]
    public Transform target;

    [Header("Orbit Settings")]
    public float distance = 10f;
    public float xSpeed = 120f;
    public float ySpeed = 120f;

    [Header("Limits")]
    public float yMinLimit = 10f;
    public float yMaxLimit = 80f;

    [Header("Zoom")]
    public float scrollSpeed = 10f;
    public float minDistance = 5f;
    public float maxDistance = 20f;
    [Header("Initial Camera Angle")]
    public float initialXAngle = 0f;
    public float initialYAngle = 30f;
    public float initialDistance = 10f;
    private float x = 0f;
    private float y = 0f;

    void Start()
    {

        x = initialXAngle;
        y = Mathf.Clamp(initialYAngle, yMinLimit, yMaxLimit);
        distance = Mathf.Clamp(initialDistance, minDistance, maxDistance);

        UpdateCameraPosition();
    }

    void LateUpdate()
    {
        if (target)
        {
            if (Input.GetMouseButton(0))
            {
                x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
                y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;
                y = Mathf.Clamp(y, yMinLimit, yMaxLimit);
            }

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            distance = Mathf.Clamp(distance - scroll * scrollSpeed, minDistance, maxDistance);

            UpdateCameraPosition();
        }
    }

    private void UpdateCameraPosition()
    {
        Quaternion rotation = Quaternion.Euler(y, x, 0);
        Vector3 negDistance = new Vector3(0, 0, -distance);
        Vector3 position = rotation * negDistance + target.position;

        transform.rotation = rotation;
        transform.position = position;
    }
}
