using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    public int MaxHealth = 100;
    private int currentHealth;
    private Animator animator;
    Rigidbody2D myRigidbody;

    public GameObject Drop;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentHealth = MaxHealth;
    }

    void Update()
    {
        if(IsFacingRight())
		{
            myRigidbody.velocity = new Vector2(moveSpeed, 0f);
		}
        else
		{
            myRigidbody.velocity = new Vector2(-moveSpeed, 0f);
        }
    }

    public void TakeDamage(int damage)
	{
        currentHealth -= damage;

        animator.SetTrigger("Hurt");

        if(currentHealth<=0)
		{
            Die();  
		}
	}

    private void Die()
	{
        animator.SetBool("isDead", true);

        GetComponent<Collider2D>().enabled = false;

        Destroy(gameObject, 0.2f);

        Instantiate(Drop, transform.position, Quaternion.identity);
    }

    private bool IsFacingRight()
	{
        return transform.localScale.x > Mathf.Epsilon;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
        transform.localScale = new Vector2(-Mathf.Sign(myRigidbody.velocity.x), transform.localScale.y);
	}
}
