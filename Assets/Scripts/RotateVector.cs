using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateVector : MonoBehaviour
{
    public Transform Target;

    private void Update()
    {
        transform.RotateAround(Target.position,Vector3.up,50*Time.deltaTime);
    }
}
