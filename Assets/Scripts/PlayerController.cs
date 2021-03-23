using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;                         
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f; 
	[SerializeField] private bool m_AirControl = false;                        
	[SerializeField] private LayerMask m_WhatIsGround;                          
	[SerializeField] private Transform m_GroundCheck;
	public static PlayerController instance;

	const float k_GroundedRadius = .2f; 
	public bool m_Grounded;            
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  
	private Vector3 m_Velocity = Vector3.zero;
	private Animator animator;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	void Start()
	{
		animator = GetComponent<Animator>();
	}

	private void Awake()
	{
		instance = this;

		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;

		m_Grounded = false;
		
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;

				animator.SetBool("Jump", !m_Grounded);

				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}

	public void Move(float move, bool jump)
	{
		if (m_Grounded || m_AirControl)
		{

			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);

			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			if (move > 0 && !m_FacingRight)
			{
				Flip();
			}
			else if (move < 0 && m_FacingRight)
			{
				Flip();
			}
		}
		if (m_Grounded && jump)
		{
			m_Grounded = false;
			
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
		}
	}

	private void Flip()
	{
		m_FacingRight = !m_FacingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.CompareTag("Collectable"))
		{
			Destroy(collision.gameObject);
		}
	}
}
