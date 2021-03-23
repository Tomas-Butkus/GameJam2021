using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimeCountDown : MonoBehaviour
{
	public GameObject TextDisplay;
	public static int SecondsLeft = 30;
	public bool TakingAway = false;

    void Start()
	{
		TextDisplay.GetComponent<Text>().text = "00:" + SecondsLeft;
	}

	void Update()
	{
		if(!TakingAway && SecondsLeft > 0)
		{
			StartCoroutine(TimerTake());
		}
	}

	IEnumerator TimerTake()
	{
		TakingAway = true;

		yield return new WaitForSeconds(1);

		SecondsLeft -= 1;

		if(SecondsLeft < 10)
		{
			TextDisplay.GetComponent<Text>().text = "00:0" + SecondsLeft;
		}
		else
		{
			TextDisplay.GetComponent<Text>().text = "00:" + SecondsLeft;
		}

		TakingAway = false;
	}
}
