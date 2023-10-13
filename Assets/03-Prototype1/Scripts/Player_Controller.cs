using UnityEngine.InputSystem;
using UnityEngine;
using TMPro;

public class Player_Controller : MonoBehaviour
{
    public float speed = 0;
    public int Health = 5;
    private Rigidbody rb;
    public Logic_Script Logic;
    private float movementX;
    private float movementY;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<Logic_Script>();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject collidedWith = other.gameObject;
        if(collidedWith.tag == "PickUp")
        {
            // Destroys the gameObject to quicken the runspeed of the game
            collidedWith.SetActive(false);
            Logic.SetCountText();
        }
    }
    void OnCollisionEnter(Collision other)
    {
        GameObject collidedWith = other.gameObject;
        if (collidedWith.tag == "Spikes")
        {
            Health = Health - 2;
            Logic.SetHealthText();
        }
    }
}
