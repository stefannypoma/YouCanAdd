using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class Timer : MonoBehaviour {
	//declaras el timer
	private float time = 0;
	private string timerString;
	private DatabaseReference sceneReference;
	// use this       
	public Text timer;	
	public Text numtext;
	public Button yourButton;
	public Button errorButton1;
	public Button errorButton2;

	// Use this for initialization
	void Start () {
		//inicializasss
		timer.GetComponent<Text> ();
		//numtext.GetComponent<Text> ();
		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(onButtonSuccessClick);

		errorButton1.onClick.AddListener (onButtonErrorClick);
		errorButton2.onClick.AddListener (onButtonErrorClick);

		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://youcanadd-ffbe4.firebaseio.com");
		sceneReference = FirebaseDatabase.DefaultInstance.RootReference.Child("games").Push();
		sceneReference.Child("fecha").SetValueAsync (System.DateTime.Now.ToString());
		sceneReference.Child("numero").SetValueAsync (numtext.text);

		Debug.Log (System.DateTime.Now.ToString());
	}
	
	// Update is called once per frame
	void Update () {
		//contar el tiempo 
		time += Time.deltaTime;
	
//		formateas
		int minutes = (int)(time / 60);
		int seconds = (int)(time % 60);
		int miliseconds = (int)(time * 1000) % 1000;
		timerString = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, miliseconds);

		// asignas
		timer.text = timerString;

	}



	private void saveResult(bool error) {
		string t = timerString;
		DatabaseReference reference = sceneReference.Child ("intento").Push ();
		reference.Child("tiempo").SetValueAsync (t);
		if (error) reference.Child("error").SetValueAsync (true);
	}

	void onButtonErrorClick() {
		saveResult (true); 
	}

	void onButtonSuccessClick() {
		saveResult (false);
	}

//	void TaskOnClick(){
//		//int sysHour = System.DateTime.Now.Hour;
//		//int min = System.DateTime.Now.Minute;
//		//StreamWriter sw = new StreamWriter ("/Users/stefanny/Desktop/LogData.txt", true); 
//		//sw.WriteLine(timerString);
//		//sw.Flush ();
//		//sw.Close ();
//
//		intentoReference.Child("fecha").SetValueAsync (System.DateTime.Now.ToString());
//		intentoReference.Child("tiempo").SetValueAsync (timerString);
//		Debug.Log (timerString);
//	}
}