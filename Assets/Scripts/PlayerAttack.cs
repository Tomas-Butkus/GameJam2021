using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;

    // Audio
    public AudioSource audioSource;
    public AudioClip clip;

    public LayerMask enemyLayers;

    public int attackDamage;

    public float attackRate = 2f;
    public float attackRange = 0.5f;

    private float nextAttackTime = 0f;

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }
    void Attack()
    {
        if(PlayerController.instance.m_Grounded == true)
		{
            animator.SetTrigger("Attack");

            audioSource.PlayOneShot(clip, 0.25f);

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            foreach (Collider2D Enemies in hitEnemies)
            {
                Enemies.GetComponent<Enemy>().TakeDamage(attackDamage);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
