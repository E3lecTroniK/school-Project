using UnityEngine;
using TMPro;

public class CoinsCounter : MonoBehaviour
{
    public static CoinsCounter Instance;

    private int coins;
    [SerializeField] private TMP_Text coinsDisplay;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    private void OnGUI()
    {
        coinsDisplay.text = coins.ToString();
    }

    public void ChangeCoins(int amount)
    {
        coins += amount;
    }
}