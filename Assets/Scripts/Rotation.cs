using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Rotatable : MonoBehaviour
{
    [SerializeField] private InputAction pressed, axis;
    [SerializeField] private float speed = 1;
    [SerializeField] private bool inverted;
    public float inertiaDamping = 2f; // Fricción de la inercia
    private Vector2 rotation;
    private Vector2 angularVelocity = Vector2.zero; // Velocidad angular (solo X e Y)
    private bool rotateAllowed;
    private Transform cam;

    private void Awake()
    {
        cam = Camera.main.transform;
        pressed.Enable();
        axis.Enable();
        pressed.performed += _ => { StartCoroutine(Rotate()); };
        pressed.canceled += _ => { rotateAllowed = false; };
        axis.performed += context => { rotation = context.ReadValue<Vector2>(); };
    }

    private IEnumerator Rotate()
    {
        rotateAllowed = true;
        while (rotateAllowed)
        {
            // Aplicar la rotación con velocidad
            rotation *= speed;
            angularVelocity.y = rotation.x;
            angularVelocity.x = rotation.y;

            transform.Rotate(Vector3.up * (inverted ? 1 : -1), angularVelocity.y, Space.World);
            transform.Rotate(cam.right * (inverted ? -1 : 1), angularVelocity.x, Space.World);

            yield return null;
        }

        // Simular inercia después de soltar el mouse
        while (angularVelocity.magnitude > 0.01f)
        {
            angularVelocity = Vector2.Lerp(angularVelocity, Vector2.zero, inertiaDamping * Time.deltaTime);
            transform.Rotate(Vector3.up * (inverted ? 1 : -1), angularVelocity.y, Space.World);
            transform.Rotate(cam.right * (inverted ? -1 : 1), angularVelocity.x, Space.World);
            yield return null;
        }
    }
}

