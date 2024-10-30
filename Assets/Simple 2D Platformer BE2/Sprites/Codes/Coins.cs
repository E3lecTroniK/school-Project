
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public Text MyscoreText;
    private int ScoreNumber;

    // Start is called before the first frame update
    void Start()
    {
        ScoreNumber = 0;
        MyscoreText.text = "Score: " + ScoreNumber;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D Coin)
    {
        if (Coin.tag == "Coin")
        {
            ScoreNumber++;
            Destroy(Coin.gameObject);//destroy when the player touches the coin
            MyscoreText.text = "Score: " + ScoreNumber;
        }
    }

}