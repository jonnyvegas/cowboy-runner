using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {

	private Animator anim;

	// Use this for initialization
	void Awake () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void OnCollisionEnter2D (Collision2D target) {
		if (target.gameObject.tag == "Obstacle") {
			anim.Play ("Idle");
		}
	}

	void OnCollisionExit2D(Collision2D target){
		if (target.gameObject.tag == "Obstacle") {
			anim.Play ("Running");
		}
	}
}
