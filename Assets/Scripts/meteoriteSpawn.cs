using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meteoriteSpawn : MonoBehaviour
{

    [SerializeField] GameObject meteor;
    [SerializeField] Transform metSpawner;
    [SerializeField] bool canSpawn = true;
    [SerializeField] float randomSeconds, randomSpeed;

    // Start is called before the first frame update
    void Start()
    {
        randomSpeed = 15;
        randomSeconds = 3;
        canSpawn = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(randomSpeed * Time.deltaTime, 0f, 0f, Space.Self);
        if (canSpawn)
        {
            canSpawn = false;
            StartCoroutine(Spawn());
        }
    }

    public IEnumerator Spawn()
    {
        randomSeconds = Random.Range(3,10);
        randomSpeed = Random.Range(-20, 21);
        yield return new WaitForSeconds(randomSeconds);
        Instantiate(meteor, metSpawner.transform.position, metSpawner.transform.rotation);
        canSpawn = true;
    }

}
