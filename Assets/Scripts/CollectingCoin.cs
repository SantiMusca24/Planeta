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
   
    
    public async void CollectCoin()
    {
        for (int i = 0; i < coins.Count; i++)
        {
            Destroy(coins[i]);
        }
        coins.Clear();
        for (int i = 0; i < coinAmount; i++)
        {
            GameObject coinInstance = Instantiate(coinPrefab, coinParent);
            float xPostion = spawnLocation.position.x + Random.Range(minX, maxX);
            float yPosition = spawnLocation.position.y + Random.Range(minY, maxY);

            coinInstance.transform.position = new Vector3(xPostion, yPosition);
            coinInstance.transform.DOPunchPosition(new Vector3(0, 30, 0), Random.Range(0, 1f)).SetEase(Ease.InOutElastic).ToUniTask();
            coins.Add(coinInstance);
            await UniTask.Delay(TimeSpan.FromSeconds(0.01f));
        }
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        await MoveCoins();
       
    }
    private async UniTask ReactToCollectionCoin()
    {
        if (coinReactionTween == null)
        {
          coinReactionTween = endPosition.DOPunchScale(new Vector3(0.3f, 0.3f, 0.1f), 0.1f).SetEase(Ease.InOutElastic);
          await coinReactionTween.ToUniTask();
          coinReactionTween = null;
            
        }
        
    }
    
    private async UniTask MoveCoins()
    {
        List<UniTask> moveCoinTask = new List<UniTask>();
        for (int i = coins.Count - 1; i >= 0; i--)
        {
            moveCoinTask.Add(MoveCoinTask(coins[i]));
            await UniTask.Delay(TimeSpan.FromSeconds(0.05f));
        }
       
    }
    private async UniTask MoveCoinTask(GameObject coinInstance)
    {
        await coinInstance.transform.DOMove(endPosition.position, duration).SetEase(Ease.InBack).ToUniTask();
        GameObject temp = coinInstance;
        Destroy(temp);
        ReactToCollectionCoin();
    }
   
}
