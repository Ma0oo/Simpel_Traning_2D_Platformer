using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, IInterect
{
    public void Interect()
    {
        Destroy(gameObject);
    }
}
