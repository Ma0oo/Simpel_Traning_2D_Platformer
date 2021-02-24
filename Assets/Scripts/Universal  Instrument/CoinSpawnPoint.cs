using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawnPoint : MonoBehaviour
{
    private Coin _coin;

    public bool TrySpawnCoin(Coin coin)
    {
        if (_coin == null)
        {
            _coin = coin;
            _coin.transform.position = transform.position;
            _coin.CoinUped += OnCoinUped;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnEnable()
    {
        if(_coin)
            _coin.CoinUped += OnCoinUped;

    }
    private void OnDisable()
    {
        if(_coin)
            _coin.CoinUped -= OnCoinUped;
    }

    private void OnCoinUped(Coin coin)
    {
        _coin.CoinUped -= OnCoinUped;
        _coin = null;
    }
}
