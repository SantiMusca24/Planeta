using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Rotatable : MonoBehaviour
{
    [SerializeField] private InputAction pressed, axis;
    [SerializeField] private float speed = 1;
    [SerializeField] private bool inverted;
    public float inertiaDamping = 2f; 
    private Vector2 rotation;
    private Vector2 angularVelocity = Vector2.zero; 
    private bool rotateAllowed;
    private Transform cam;
    private int speedCounter = 0;

    private void Awake()
    {
        cam = Camera.main.transform;
        pressed.Enable();
        axis.Enable();
        pressed.performed += ctx =>
        {
            Vector2 screenPos = ctx.control.device is Pointer pointer
                ? pointer.position.ReadValue()
                : Vector2.zero;

            if (WasPressedOnPlanet(screenPos))
                StartCoroutine(Rotate());
        };
        pressed.canceled += _ => { rotateAllowed = false; };
        axis.performed += context => { rotation = context.ReadValue<Vector2>(); };
    }
    private bool WasPressedOnPlanet(Vector2 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform == transform)
            {
                return true;
            }
        }

        return false;
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

            // Verificar si la velocidad supera 1 y sumar al contador
            if (Mathf.Abs(angularVelocity.x) > 40 || Mathf.Abs(angularVelocity.y) > 40)
            {
                speedCounter++; 
                Debug.Log("Speed counter: " + speedCounter); 
            }

            // Rotación del objeto
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

