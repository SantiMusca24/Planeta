using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meteorMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -4 * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
       if (other.name == "meteorKill") Destroy(gameObject);
    }

}
