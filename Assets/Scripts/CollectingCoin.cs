using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using System;
using Random = UnityEngine.Random;


public class CollectingCoin : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private Transform coinParent;
    [SerializeField] private Transform spawnLocation;
    [SerializeField] private Transform endPosition;
    [SerializeField] private float duration;
    [SerializeField] private int coinAmount;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    List<GameObject> coins = new List<GameObject>();
    private Tween coinReactionTween;

    public void CollectCoin()
    {
        for (int i = 0; i < coins.Count; i++)
        {
            Destroy(coins[i]);
        }
        coins.Clear();
        StartCoroutine(CollectCoinCoroutine());
    }

    private IEnumerator CollectCoinCoroutine()
    {
        for (int i = 0; i < coinAmount; i++)
        {
            GameObject coinInstance = Instantiate(coinPrefab, coinParent);
            float xPosition = spawnLocation.position.x + Random.Range(minX, maxX);
            float yPosition = spawnLocation.position.y + Random.Range(minY, maxY);

            coinInstance.transform.position = new Vector3(xPosition, yPosition);
            coinInstance.transform.DOPunchPosition(new Vector3(0, 30, 0), Random.Range(0, 1f)).SetEase(Ease.InOutElastic);
            coins.Add(coinInstance);
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(0.5f);
        StartCoroutine(MoveCoins());
    }

    private IEnumerator ReactToCollectionCoin()
    {
        if (coinReactionTween == null)
        {
            coinReactionTween = endPosition.DOPunchScale(new Vector3(0.3f, 0.3f, 0.1f), 0.1f).SetEase(Ease.InOutElastic);
            yield return coinReactionTween.WaitForCompletion();
            coinReactionTween = null;
        }
    }

    private IEnumerator MoveCoins()
    {
        for (int i = coins.Count - 1; i >= 0; i--)
        {
            StartCoroutine(MoveCoinTask(coins[i]));
            yield return new WaitForSeconds(0.05f);
        }
    }

    private IEnumerator MoveCoinTask(GameObject coinInstance)
    {
        yield return coinInstance.transform.DOMove(endPosition.position, duration).SetEase(Ease.InBack).WaitForCompletion();
        Destroy(coinInstance);
        StartCoroutine(ReactToCollectionCoin());
    }
}
