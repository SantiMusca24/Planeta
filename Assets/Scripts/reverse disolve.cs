using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class reversedisolve : MonoBehaviour
{
    [SerializeField] private float minY = -2f;
    [SerializeField] private float maxY = 0f;
    [SerializeField] private float duration = 1f;
    [SerializeField] private Vector3 minScale = new Vector3(0.5f, 0.5f, 0.5f);
    [SerializeField] private Vector3 maxScale = new Vector3(1f, 1f, 1f);

    private void Start()
    {
        
        Vector3 startPos = transform.localPosition;
        startPos.y = minY;
        transform.localPosition = startPos;

        
        transform.localScale = minScale;

        
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOLocalMoveY(maxY, duration));
        seq.Join(transform.DOScale(maxScale, duration));
    }
}
