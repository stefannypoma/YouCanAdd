using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Dibujar : MonoBehaviour {

	public Shader shader;
	public Color color;

	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			StartCoroutine (dibujar ());
		}
	}

	IEnumerator dibujar (){

		LineRenderer r = new GameObject ().AddComponent<LineRenderer> ();

		r.transform.SetParent (transform);

        r.material = new Material(shader)
        {
            color = color
        };

        r.SetWidth (0.5f, 0.5f);

		List<Vector3> posiciones = new List<Vector3> ();

		while (Input.GetMouseButton (0)) {
			posiciones.Add (Camera.main.ScreenToWorldPoint (Input.mousePosition) + Vector3.forward * 5);

			r.SetVertexCount (posiciones.Count);

			r.SetPositions (posiciones.ToArray());

			yield return new WaitForSeconds (0);
		}
	}
}
