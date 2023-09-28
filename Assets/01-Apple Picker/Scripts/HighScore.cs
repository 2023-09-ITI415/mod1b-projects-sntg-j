using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    static public int score = 1000;
    // Start is called before the first frame update
    private void Awake()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            score = PlayerPrefs.GetInt("HighScore");
        }
        PlayerPrefs.SetInt("HighScore", score);
    }

    // Update is called once per frame
    void Update()
    {
        TMP_Text gt = this.GetComponent<TMP_Text>();
        gt.text = "High Score: " + score;
    }
}
