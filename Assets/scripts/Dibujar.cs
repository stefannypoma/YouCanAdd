using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Dibujar : MonoBehaviour {

	public Shader shader;
	public Color color;

    private List<LineRenderer> lineRenderers;
    private Coordinator coordinator;

    void Start()
    {
        this.coordinator = Coordinator.GetInstance();
        this.coordinator.SetOnNext(DeleteLineRenderers);
    }

	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			StartCoroutine (dibujar ());
		}
	}

    private void DeleteLineRenderers()
    {
        foreach (LineRenderer line in this.lineRenderers)
        {
            Destroy(line);
        }
    }

	IEnumerator dibujar (){

		LineRenderer r = new GameObject ().AddComponent<LineRenderer> ();
        
		r.transform.SetParent (transform);

        r.material = new Material(shader)
        {
            color = color
        };

        r.SetWidth (0.1f, 0.1f);

		List<Vector3> posiciones = new List<Vector3> ();
        
        while (Input.GetMouseButton (0)) {
			posiciones.Add (Camera.main.ScreenToWorldPoint (Input.mousePosition) + Vector3.forward * 5);

			r.SetVertexCount (posiciones.Count);

			r.SetPositions (posiciones.ToArray());

			yield return new WaitForSeconds (0);
		}        
    }
}
