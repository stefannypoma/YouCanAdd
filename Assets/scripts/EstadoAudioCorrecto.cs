using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoAudioCorrecto : MonoBehaviour {

	public static EstadoAudioCorrecto estadoAudioCorrecto;



	// Use this for initialization
	void Start () {
		
	}

	void Awake(){
		if (estadoAudioCorrecto == null) {
			estadoAudioCorrecto = this;
			DontDestroyOnLoad (gameObject);
		} else if (estadoAudioCorrecto != this) {
			Destroy (gameObject);
		}
	}
	/*private static EstadoAudioCorrecto instance = null;
	public static EstadoAudioCorrecto Instance
	{
		get { return instance; }
	}

	void Awake()
	{
		if (instance != null && instance != this) {
			Destroy (this.gameObject);
			return;
		} 
		else 
		{
			instance = this;
		}
		DontDestroyOnLoad (this.gameObject);
	}
*/

	// Update is called once per frame
	void Update () {
		
	}
}
