using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsSpawner : MonoBehaviour
{
    [Range(1, 10f)] [SerializeField] private float _timeBetweenSpawn;
    [Range(2, 20)] [SerializeField] private int _maxCoinOnLevel;
    [SerializeField] private Coin _template;
    [SerializeField] private List<CoinSpawnPoint> _points;

    private List<Coin> _activeCoin = new List<Coin>();
    private List<Coin> _unactiveCoin = new List<Coin>();

    private void Awake()
    {
        Init();
    }

    private void OnEnable()
    {
        foreach (var item in _activeCoin)
            item.CoinUped += OnCoinUped;
        foreach (var item in _unactiveCoin)
            item.CoinUped += OnCoinUped;
        StartCoroutine(Spawning());
    }

    private void OnDisable()
    {
        foreach (var item in _activeCoin)
            item.CoinUped -= OnCoinUped;
        foreach (var item in _unactiveCoin)
            item.CoinUped -= OnCoinUped;
    }

    private void Init()
    {
        for (int i = 0; i < _maxCoinOnLevel; i++)
        {
            Coin temp = Instantiate(_template, transform);
            temp.transform.position = Vector2.zero;
            temp.gameObject.SetActive(false);
            _unactiveCoin.Add(temp);
        }
    }

    private void OnCoinUped(Coin coin)
    {
        _activeCoin.Remove(coin);
        coin.gameObject.SetActive(false);
        _unactiveCoin.Add(coin);
    }

    private IEnumerator Spawning()
    {
        while (this.enabled)
        {
            yield return new WaitForSeconds(_timeBetweenSpawn);
            if (_unactiveCoin.Count > 0)
            {
                for (int i = 0; i < _maxCoinOnLevel; i++)
                {
                    Coin coinToSpawn = _unactiveCoin[Random.Range(0, _unactiveCoin.Count)];
                    if (_points[Random.Range(0, _points.Count)].TrySpawnCoin(coinToSpawn))
                    {
                        _unactiveCoin.Remove(coinToSpawn);
                        coinToSpawn.gameObject.SetActive(true);
                        _activeCoin.Add(coinToSpawn);
                        Debug.Log("Монета активирована");
                        break;
                    }
                }
            }
        }
    }
}
