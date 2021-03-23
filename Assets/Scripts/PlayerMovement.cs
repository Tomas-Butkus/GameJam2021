using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerController controller;
    private Animator animator;
    private Rigidbody2D rb2;

    public float runSpeed = 40f;

    float horizontalMove = 0f;

    private bool jump = false;

    public float DashSpeed;

    private float dashTime;

    public float startDashTime;

    private int direction;

    private void Start()
    {
        animator = GetComponent<Animator>();

        rb2 = GetComponent<Rigidbody2D>();

        dashTime = startDashTime;
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if(horizontalMove == 0)
		{
            animator.SetBool("isRunning", false);
        }
        else
		{
            animator.SetBool("isRunning", true);
        }
           
        if (Input.GetButtonDown("Jump"))
		{
            jump = true;
        }

        animator.SetFloat("yVelocity", rb2.velocity.y);


        if(direction==0)
		{
            if(Input.GetKeyDown(KeyCode.K))
			{
                if(horizontalMove < 0)
				{
                    direction = 1;
				}
                else if(horizontalMove>0)
				{
                    direction = 2;
				}
			}
		}
        else
		{
            if (dashTime <= 0)
			{
                direction = 0;
                dashTime = startDashTime;
                rb2.velocity = Vector2.zero;
			}
            else
			{
                dashTime -= Time.deltaTime;
                if(direction==1)
				{
                    rb2.velocity = Vector2.left * DashSpeed;
				}
                else if(direction==2)
				{
                    rb2.velocity = Vector2.right * DashSpeed;
                }
                animator.SetTrigger("Dash");
            }
		}
    }

	void FixedUpdate()
	{
        MovePlayer();
    }

	public void MovePlayer()
	{
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        animator.SetBool("Jump", true);
        jump = false;
    }
}
