using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    
    public int count = 0;
    public static GameManager Instance;
    private void Awake()
    {
        
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void OnEnable()
    {
       spinDetect.OnPlanetRotated += RotateAction;
        StartCoroutine(AutoIncrementCoroutine());
    }

    void OnDisable()
    {
        spinDetect.OnPlanetRotated -= RotateAction;
    }

    public void RotateAction()
    {
        count++;
        
    }

    
    IEnumerator AutoIncrementCoroutine()
    {
        while (true) 
        {
            yield return new WaitForSeconds(1f); 
            RotateAction(); 
        }
    }
}
