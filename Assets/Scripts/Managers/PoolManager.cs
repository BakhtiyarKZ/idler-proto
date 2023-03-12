using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{

    [SerializeField] private List<RectTransform> pooledCoins; 
    [SerializeField] private GameObject animatedCoinPrefab; 
    [SerializeField] private int amountToPool;
    [SerializeField] private Canvas mainCamvas;

    private void Start()
    {
        pooledCoins= new List<RectTransform>();

        GameObject newCoin;

        for (int i = 0; i < amountToPool; i++)
        {
            newCoin = Instantiate(animatedCoinPrefab);
            RectTransform coinRect = newCoin.GetComponent<RectTransform>();
            newCoin.transform.SetParent(mainCamvas.transform);
            newCoin.transform.localPosition = Vector3.zero;
            newCoin.SetActive(false);
            pooledCoins.Add(coinRect);
        }
    }

    public RectTransform GetPooledCoins()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledCoins[i].gameObject.activeInHierarchy)
            {             
                return pooledCoins[i];
            }
        }

        GameObject newCoin = Instantiate(animatedCoinPrefab);
        RectTransform coin = newCoin.GetComponent<RectTransform>();
        return coin;
    }

    public void ReturnCoinToPool(RectTransform _coin)
    {
        _coin.gameObject.SetActive(false);
        _coin.gameObject.transform.localPosition = Vector3.zero;
    }
}
