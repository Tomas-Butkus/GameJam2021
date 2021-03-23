using UnityEngine;
using UnityEngine.UI; 

public class ScoringSystem : MonoBehaviour
{
	public GameObject ScoreText;
	public static int Score = 0;

	void Update()
	{
		ScoreText.GetComponent<Text>().text = ": " + Score;
	}
}
