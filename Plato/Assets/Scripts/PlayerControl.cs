using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{

    //Movement variables
    public float speed = 0.5f;
    Rigidbody2D rb; // create refrence for rigid body b/c jump requires physics
    public float jumpForce; //the force that willl be added to vertical component of velocity
    public float dashForce;

    //Ground Check Variables
    public LayerMask groundLayer;
    public Transform groundCheck;
    public bool isGrounded;

    //Dashing Variables
    public bool hasDashed;
    public int direction = 1;

    //Animator variables
    Animator anim;
    public bool Moving = false;
    public bool Dashing = false;

    //Text Variable
    public Text finishText;
   

    SpriteRenderer sprite;
    public int score = 0;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, .5f, groundLayer);

        Vector3 newPosition = transform.position;
        Vector3 newScale = transform.localScale;
        float currentScale = Mathf.Abs(transform.localScale.x);
        
        
        // Move left
        if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
        {
            if (rb.velocity.x > 0)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            newPosition.x -= speed;           
            newScale.x = -1 * currentScale;
            Moving = true;
            direction = -1;
            

        }
        // Move right
        if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
        {
            if (rb.velocity.x < 0)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            
            newPosition.x += speed;
            newScale.x = currentScale;
            Moving = true;
            direction = 1;
            

        }
        // Stops Moving
        if(Input.GetKeyUp("a") || Input.GetKeyUp("d") || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            Moving = false;
        }
        //Jump
        if ((Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow)) && (isGrounded == true))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        //Dash
        if(Input.GetKey(KeyCode.Space))
        {
            if (isGrounded == false)
            {
                if (hasDashed == false)
                {
                    //Dash
                    rb.velocity = new Vector2((dashForce * direction), 3);
                    hasDashed = true;
                    Dashing = true;
                } else
                {
                    //Do not dash
                }
            }
        }
        if (isGrounded == true)
        {
            hasDashed = false;
            Dashing = false;
        }
       

        

        anim.SetBool("isMoving", Moving);
        anim.SetBool("isDashing", Dashing);
        transform.position = newPosition;
        transform.localScale = newScale;

        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Collect"))
        {
            Debug.Log("Ouh strawberry!");
            score += 100;
        }
        if (collision.gameObject.tag.Equals("Finish"))
        {
            SceneManager.LoadScene(1);
        }
        if (collision.gameObject.tag.Equals("GameOver"))
        {
            score -= 100;
            SceneManager.LoadScene(2);
        }
    }
}
