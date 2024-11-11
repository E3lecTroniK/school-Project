using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField] private int value;
    private bool hasTriggerd;

    private CoinsCounter coinsManager;

    private void Start()
    {
        coinsManager = CoinsCounter.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasTriggerd)
        {
            hasTriggerd = true;
            coinsManager.ChangeCoins(value);
            Destroy(gameObject);
        }
    }
}
