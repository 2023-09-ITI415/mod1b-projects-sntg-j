using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Basket : MonoBehaviour
{
    [Header("Set Dynamically")]
    public static TextMeshProUGUI scoreGT;

    void Start()
    {
        GameObject scoreGO = GameObject.Find("ScoreCounter");
        scoreGT = scoreGO.GetComponent<TextMeshProUGUI>();
        scoreGT.text = "0";
    }

    void Update()
    {
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        this.transform.position = pos;
    }

    private void OnCollisionEnter(Collision coll)
    {
        GameObject collidedWith = coll.gameObject;

        if(collidedWith.tag == "Apple")
        {
            Destroy(collidedWith);

            int score = int.Parse(scoreGT.text);
            score += 100;
            scoreGT.text = score.ToString();

            if(score > HighScore.score)
            {
                HighScore.score = score;
            }
        }
    }
}
