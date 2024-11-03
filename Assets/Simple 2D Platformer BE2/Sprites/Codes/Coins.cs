using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    [SerializeField] private int value;
    private bool hasTriggered;

    private CoinsManager coinsManager;

    private void Start()
    {
        CoinsManager instance = coinsManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            coinsManager.ChangeCoins(value);
            Destroy(gameObject);
        }
    }
}