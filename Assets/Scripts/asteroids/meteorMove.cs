using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meteorMove : MonoBehaviour
{
   
    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, cloud1.meteorSpd * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
       if (other.name == "meteorKill") Destroy(gameObject);
    }

    public void Tapped()
    {
        GameManager.tapped = true;
        Destroy(gameObject);
    }

}
