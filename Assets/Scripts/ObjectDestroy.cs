using UnityEngine;

public class ObjectDestroy : MonoBehaviour
{

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.CompareTag("Player"))
		{
			Destroy(gameObject);
		}
	}
}
