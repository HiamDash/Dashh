using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpForce = 15f;
    public int lungeImpulse = 500;



    Collider2D myCollider;
    public float spawnX, spawnY;
    private Rigidbody2D rb;

    public float move;

    //flip
    public bool facingRight = true;
    



    //doubledjump
    public bool grounded = false;
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    public bool isGrounded;
    [SerializeField] private int JumpCount;
    [SerializeField] private int JumpCountMax;


    //death helth
    public Image Bar;
    public float lives;

    //damage
    public float timeBtwAttack;
    public float startTimeBtwAttack;
    public Transform attackPos;
    public LayerMask enemy;
    public float attackRange;
    public int damage;

    private UnityEngine.Object explosion;

    public static Player Instance { get; set; }

    void Start ()
    {
     
        explosion = Resources.Load("Explosion");
        lives = 1f;
    }

    private void Awake()
    {
        spawnX = transform.position.x;
        spawnY = transform.position.y;
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        if (grounded)
        {
            JumpCount = 0;
        }
        move = Input.GetAxis("Horizontal");
        lives -= Time.deltaTime * 0.01F;
        Bar.fillAmount = lives;
        if (Bar.fillAmount <= 0f)
        {
            SceneManager.LoadScene(2);
        }
    }
    void Update()
    {
       
    
        isGrounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround);

        if (Input.GetKeyDown(KeyCode.Z))
            Dash();
        if (Input.GetButton("Horizontal"))
            Run();
        if (Input.GetButtonDown("Jump"))
            Jump();
        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();

        //damage
        if (timeBtwAttack <= 0)
        {
            if (Input.GetMouseButton(0)&& lives>=0F)
            {
                //anim.SetTrigger("attack");
                Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);
                for (int i = 0; i < enemies.Length; i++)
                {
                    enemies[i].GetComponent<Enemy>().TakeDamage(damage);
                }
                lives -= 0.01F;
                
               GameObject explosionRef = (GameObject)Instantiate(explosion);
                if (facingRight == true)
                {
                    explosionRef.transform.position = new Vector3(transform.position.x+6, transform.position.y+2, transform.position.z);
                }
                else
                {
                    explosionRef.transform.position = new Vector3(transform.position.x -6, transform.position.y + 2, transform.position.z);
                }
            }
            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }
    //damageradius
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    private void Run()
    {
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);

    }
    private void Jump()
    {
        if (JumpCount < JumpCountMax)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            JumpCount++;
        }
    }
    private void Dash()
    {
        rb.velocity = new Vector2(0,0);
        if(rb.transform.localScale.x < 0){rb.AddForce(Vector2.left * lungeImpulse);}
        else {rb.AddForce(Vector2.right * lungeImpulse);}

    }
    
    public void GetDamage()
    {
        lives -= 0.1f;
        Debug.Log(lives);
    }


    public void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    //death
    private void OnCollisionEnter2D(Collision2D other)
    {
        
    }

}
