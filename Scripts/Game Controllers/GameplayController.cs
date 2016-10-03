using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour {

	[SerializeField]
	private GameObject pausePanel;

	[SerializeField]
	private Button restartGameButton;

	[SerializeField] 
	private Text scoreText, pauseText;
	private int score;

	// Use this for initialization
	void Start () {
		//This will set the proper score
		scoreText.text = score + "M";
		//start counting immediately when we start
		StartCoroutine (CountScore ());
	}
	
	IEnumerator CountScore()
	{
		yield return new WaitForSeconds (0.6f);
		score++;
		scoreText.text = score + "M";
		StartCoroutine (CountScore ());
	}

	void OnEnable()
	{
		PlayerDied.endGame += PlayerDiedEndTheGame;
	}

	void OnDisable()
	{
		PlayerDied.endGame -= PlayerDiedEndTheGame;
	}

	void PlayerDiedEndTheGame() 
	{
		//We just started the game. Don't have the key
		if (!PlayerPrefs.HasKey ("Score")) {//Set the key's score to 0. key value pairs.
			PlayerPrefs.SetInt ("Score", 0);
		} else {
			//Get the score from the score key
			int highScore = PlayerPrefs.GetInt ("Score");
			//We have a new high score!
			if (highScore < score) {
				PlayerPrefs.SetInt ("Score", score);
			}
		}

		pauseText.text = "Game Over";
		pausePanel.SetActive (true);
		restartGameButton.onClick.RemoveAllListeners ();
		restartGameButton.onClick.AddListener (() => RestartGame());
		//Time stops
		Time.timeScale = 0f;
	}

	public void PauseButton()
	{//Time stops
		Time.timeScale = 0f;
		pausePanel.SetActive (true);
		restartGameButton.onClick.RemoveAllListeners ();
		restartGameButton.onClick.AddListener (() => ResumeGame());
	}

	public void GoToMenu()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene ("MainMenu");
	}

	public void RestartGame(){
		Time.timeScale = 1f;
		SceneManager.LoadScene ("Gameplay");
	}

	public void ResumeGame()
	{
		Time.timeScale = 1f;
		pausePanel.SetActive (false);
	}
}
