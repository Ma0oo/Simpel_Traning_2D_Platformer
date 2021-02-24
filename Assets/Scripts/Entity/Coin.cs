using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour, IInterect
{
    public event UnityAction<Coin> CoinUped;

    public void Interect()
    {
        if (CoinUped!=null)
        {
            CoinUped(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
