using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawnPoint : MonoBehaviour
{
    private Coin _coinOnPoint;

    public bool TrySpawnCoin(Coin coin)
    {
        if (_coinOnPoint == null)
        {
            _coinOnPoint = coin;
            _coinOnPoint.transform.position = transform.position;
            _coinOnPoint.CoinUped += OnCoinUped;
            coin.transform.SetParent(transform);
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnEnable()
    {
        if(_coinOnPoint)
            _coinOnPoint.CoinUped += OnCoinUped;

    }
    private void OnDisable()
    {
        if(_coinOnPoint)
            _coinOnPoint.CoinUped -= OnCoinUped;
    }

    private void OnCoinUped(Coin coin)
    {
        _coinOnPoint.CoinUped -= OnCoinUped;
        _coinOnPoint = null;
    }
}
