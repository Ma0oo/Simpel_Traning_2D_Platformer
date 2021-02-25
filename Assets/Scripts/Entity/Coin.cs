using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Coin : MonoBehaviour, IInterect
{
    public event UnityAction<Coin> CoinUped;

    public void Interect()
    {
        if (CoinUped!=null)
            CoinUped(this);
        else
            Destroy(gameObject);
    }
}
