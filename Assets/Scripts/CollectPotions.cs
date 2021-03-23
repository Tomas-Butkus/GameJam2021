using UnityEngine;

public class CollectPotions : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		ScoringSystem.Score += 1;
		TimeCountDown.SecondsLeft += 5;
		Destroy(gameObject);
	}
}
