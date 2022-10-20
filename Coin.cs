using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D),typeof(Coin))]

public class Coin : MonoBehaviour
{
    public void Collected()
    {
        Destroy(gameObject);
    }
}