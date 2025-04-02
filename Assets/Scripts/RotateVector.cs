using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateVector : MonoBehaviour
{
    public Transform Target;
    private float rotation = 20f;

    private void Update()
    {
        transform.RotateAround(Target.position,Vector3.up,rotation*Time.deltaTime);
    }
}
