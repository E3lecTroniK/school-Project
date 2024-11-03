using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsCounter : MonoBehaviour
{
    public static CoinsCounter Instance;

    public TMP_Text coinText;
    public int currentCoins = 0;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        coinText.text = "COINS: " + currentCoins.ToString();
    }

    public void IncreaseCoins(int v)
    {
        currentCoins += v;
        coinText.text = "COINS: " + currentCoins.ToString();
    }
}
