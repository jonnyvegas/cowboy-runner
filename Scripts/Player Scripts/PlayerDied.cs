using UnityEngine;
using System.Collections;

public class PlayerDied : MonoBehaviour {

	public delegate void EndGame();
	public static event EndGame endGame;

	void PlayerDiedEndGame()
	{
		if (endGame != null)
			endGame ();

		Destroy (gameObject);
	}

	//Can check trigger because collector is trigger.
	void OnTriggerEnter2D(Collider2D target)
	{
		if (target.tag == "Collector") {
			PlayerDiedEndGame ();
		}
	}


	//Player and Zombie do not have trigger, must use on collision instead.
	void OnCollisionEnter2D(Collision2D target)
	{
		if (target.gameObject.tag == "Zombie") {
			PlayerDiedEndGame ();
		}
	}
}
