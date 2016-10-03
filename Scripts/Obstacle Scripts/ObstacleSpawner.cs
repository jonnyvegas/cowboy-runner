using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour {

	[SerializeField]
	private GameObject[] obstacles;
	private List<GameObject> obstaclesForSpawning = new List<GameObject> ();

	// Use this for initialization
	void Start () {
		StartCoroutine(SpawnRandomObstacle ());
	}
	
	void InitializeObstacles()
	{
		int index = 0;
		//Add each element in obstacles 3 times.
		for (int i = 0; i < obstacles.Length * 3; i++) {
			GameObject obj = Instantiate (obstacles [index], new Vector3(transform.position.x,
				transform.position.y, 1), Quaternion.identity) as GameObject;
			obstaclesForSpawning.Add (obj);
			obstaclesForSpawning [i].SetActive (false);
			index++;
			//reset index so we don't go out of bounds
			if (index == obstacles.Length) {
				index = 0;
			}
		}
	}

	void Awake()
	{
		InitializeObstacles ();
	}

	void Shuffle(){
		//Shuffle the objects using random indexs that are in the game object list.
		for (int i = 0; i < obstaclesForSpawning.Count; i++) {
			GameObject temp = obstaclesForSpawning [i];
			int random = Random.Range (i, obstaclesForSpawning.Count);
			obstaclesForSpawning [i] = obstaclesForSpawning [random];
			obstaclesForSpawning [random] = temp;
		}
	}

	IEnumerator SpawnRandomObstacle()
	{
		yield return new WaitForSeconds (Random.Range (1.5f, 4.5f));
		int index = Random.Range (0, obstaclesForSpawning.Count);
		//Need to loop. Check if item isn't active and set it to be active.
		bool looping = true;
		while (looping) {
			if (!obstaclesForSpawning [index].activeInHierarchy) {
				obstaclesForSpawning [index].SetActive (true);
				obstaclesForSpawning [index].transform.position = new Vector3(transform.position.x, transform.position.y, 1);
				looping = false;
			} else {
				index = Random.Range (0, obstaclesForSpawning.Count);
			}
		}

		StartCoroutine (SpawnRandomObstacle());
	}
}
