using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class reversedisolve : MonoBehaviour
{
    [SerializeField] private float fallDistance = 2f;
    [SerializeField] private float duration = 1f;
    [SerializeField] private Ease easing = Ease.OutBounce;
    [SerializeField] private ParticleSystem particleOnLand;
    [SerializeField] private float particleDelay = 0.7f; 

    private Vector3 originalLocalPosition;

    void Awake()
    {
        originalLocalPosition = transform.localPosition;
    }

    public void OnEnable()
    {
        Vector3 startPos = originalLocalPosition;
        startPos.y += fallDistance;
        transform.localPosition = startPos;

        transform.DOLocalMoveY(originalLocalPosition.y, duration).SetEase(easing);

        Invoke(nameof(ActivateLandParticle), particleDelay); 
    }

    private void ActivateLandParticle()
    {
        if (particleOnLand != null)
            particleOnLand.Play();
    }
}
