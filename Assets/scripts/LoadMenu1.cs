using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMenu1 : MonoBehaviour {

	public float delay;

	public void OnClick()
	{
		StartCoroutine(LoadLevelAfterDelay(delay));
	}

	IEnumerator LoadLevelAfterDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
		SceneManager.LoadScene("PantallaPrincipal");
	}
}
