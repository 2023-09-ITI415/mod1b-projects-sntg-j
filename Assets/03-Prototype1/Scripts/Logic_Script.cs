using UnityEngine.InputSystem;
using UnityEngine;
using TMPro;

public class Logic_Script : MonoBehaviour
{
    public TextMeshProUGUI countText;
    public TextMeshProUGUI HealthText;
    public GameObject Player;
    public GameObject winTextObject;
    public GameObject parentprefab;
    private int health;
    private int number;
    private int count;
    
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        parentprefab = GameObject.FindGameObjectWithTag("PickUpParent");
        number = parentprefab.GetComponent<Instantiator>().getInstanceNums();
        winTextObject.SetActive(false);
    }
    public void SetCountText()
    {
        count++;
        countText.text = "Count: " + count.ToString();
        if(count >= number / 2)
        {
            parentprefab.GetComponent<Instantiator>().Pulse();
        }
        if (count >= number)
        {
            winTextObject.SetActive(true);
        }
    }

    public void SetHealthText()
    {
        health = Player.GetComponent<Player_Controller>().Health;
        HealthText.text = "Health: " + health.ToString() + "/10";
        if ( health <= 0)
        {
            winTextObject.GetComponent<TextMeshProUGUI>().SetText("You Lost, Try Again?");
            winTextObject.SetActive(true);
        }
    }
}
