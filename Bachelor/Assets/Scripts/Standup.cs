using UnityEngine;
using System.Collections;

public class Standup : MonoBehaviour {
	private bool seated = true;
	private float forward = 0.1f;
	private float up = 0.3f;
	private float timeSinceStandup = 0;

	private Animator animator;
	private GameObject model;


	// Use this for initialization
	void Start () {
		GameObject player = GameObject.FindGameObjectWithTag("player");
		model = player;
		animator = player.transform.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		listenKey ();
		standUp ();
	}

	private void standUp(){
		float time = 0.03f;
		if (!seated && (Time.time-timeSinceStandup >= time)) {
			if (forward > 0) {
				transform.Rotate(Vector3.forward,0.05f);
				this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z + 0.01f);
				model.transform.position = new Vector3 (model.transform.position.x, model.transform.position.y, model.transform.position.z + 0.01f);
				forward-=0.005f;
			}
			else if (up > 0) {
				transform.Rotate(Vector3.forward,-0.1f);
				this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y + 0.01f, this.transform.position.z);
				up-=0.01f;
			}
			timeSinceStandup += time;
		}
	}

	private void listenKey(){
		if (Input.GetKeyDown (KeyCode.Space) && seated == true) {
			changeAnimation();
			startStandUp();
		}
	}

	private void changeAnimation(){
		animator.SetTrigger ("StandUp");
	}

	private void startStandUp(){
		seated = false;
		timeSinceStandup = Time.time;
		GameObject.FindGameObjectWithTag("standUp").transform.GetComponent<CanvasGroup>().alpha = 0f;
		GameObject.FindGameObjectWithTag("standUpShadow").transform.GetComponent<CanvasGroup>().alpha = 0f;
	}


	public void setSeatedTrue(){
		seated = true;
	}

	public bool seatedState(){
		return seated;
	}
}
