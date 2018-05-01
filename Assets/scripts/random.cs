using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class random : MonoBehaviour {
	public GameObject errorbutton2;
	public Vector2 errorbutton1;
	public Vector2 transparent;


	// Use this for initialization
	void Start () {


		GameObject errorbutton2 = GameObject.Find("errorbutton2");
		Vector2 pos = errorbutton2.transform.position;

		Debug.Log ("Pato" + pos);

		GameObject errorbutton1 = GameObject.Find("errorbutton1");
		Vector2 pos2 = errorbutton1.transform.position;

		Debug.Log ("Panda" + pos2);

		GameObject transparent = GameObject.Find("transparent");
		Vector2 pos3 = transparent.transform.position;

		Debug.Log ("Pelota" + pos3);


		pos = new Vector2(Random.Range(pos2.x,pos3.x),pos.y) ;
		errorbutton2.transform.position = pos;

	}



	
	// Update is called once per frame
	void Update () {

	}
}
