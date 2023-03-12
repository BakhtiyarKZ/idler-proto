using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Coins")]
    [SerializeField] private float coinsCount;
    [SerializeField] private TextMeshProUGUI coinsCountText;
    [SerializeField] private RectTransform targetRect;
    [SerializeField] private float shakeAnimationDuration;

    [Header("Wheat")]
    [SerializeField] private TextMeshProUGUI wheatBlockCountText;
    [SerializeField] private TextMeshProUGUI wheatMaxBlockCountText;
    [SerializeField] private int amount;

    [Header("Dependency")]
    [SerializeField] private PoolManager poolManager;
    
 

    public void AnimateSelling(int _deliveredCount)
    {
        amount = _deliveredCount;
        shakeAnimationDuration = amount * 0.3f;
        StartCoroutine(AnimateCoins());

    }


    IEnumerator AnimateCoins()
    {
        StartCoroutine(ShakeAnimation());
        while (amount > 0)
        {
            yield return new WaitForSeconds(0.2f);
            amount--;
            RectTransform coin = poolManager.GetPooledCoins();
            coin.gameObject.SetActive(true);
            coin.DOMove(targetRect.position, 0.3f).OnComplete(() => {
                    poolManager.ReturnCoinToPool(coin);
                    AnimateCoinCount();
                });            
        }
       
    }


    public void AnimateCoinCount()
    {
        float newVal = coinsCount + 15f;
        DOVirtual.Float(Mathf.Ceil(coinsCount), newVal, 0.2f, (x) => { coinsCountText.text = Mathf.Ceil(x).ToString(); });
        coinsCount = newVal;
    }

    IEnumerator ShakeAnimation()
    {
        yield return new WaitForSeconds(0.2f);
        coinsCountText.transform.DOShakePosition(shakeAnimationDuration,3f);
    }

    public void WheatBlockCountIncrease(int _count)
    {
        wheatBlockCountText.text = _count.ToString();
    }

    public void WheatBlockCountDecrease(int _count)
    {
        wheatBlockCountText.text = _count.ToString();        
    }

    public void SetMaxWheatCount(int _maxWheatCount)
    {
        wheatMaxBlockCountText.text = "/"+_maxWheatCount.ToString();
    }


}
