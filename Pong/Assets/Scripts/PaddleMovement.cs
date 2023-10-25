using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    public float speed = 7f;
    public float boardLimit = 3.5f;

    private Rigidbody2D palletRB;
    public Player player;
    private string verticalInputAxis = "Vertical";
    private Vector2 initialPosition;


    // Start is called before the first frame update
    void Start()
    {
        initialPosition = gameObject.transform.position;
        palletRB = GetComponent<Rigidbody2D>();
        if (player == Player.PLAYER_1)
        {
            verticalInputAxis = "Vertical1";
        }
        else if (player == Player.PLAYER_2)
        {
            verticalInputAxis = "Vertical2";
        }
    }

    public void Restart()
    {
        gameObject.transform.position = initialPosition;
        palletRB.velocity = new Vector2(0,0);
    }

    // Update is called once per frame
    void Update()
    {
        float movement = Input.GetAxis(verticalInputAxis);
        /*
        Debug.Log(movement);
        if (
            (movement < 0 && palletRB.position.y > -boardLimit) ||
            (movement > 0 && palletRB.position.y < boardLimit)
        )
        {
            palletRB.velocity = new Vector2(palletRB.velocity.x, movement * speed);
        }
        else
        {
            palletRB.velocity = new Vector2(palletRB.velocity.x, 0);
        }
        */

        palletRB.velocity = new Vector2(palletRB.velocity.x, movement * speed);
        // palletRB.position = new Vector2(palletRB.position.x, Mathf.Clamp(palletRB.position.y, -boardLimit, boardLimit));


    }
}
