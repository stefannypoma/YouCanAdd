using System;
using System.Collections.Generic;
using UnityEngine;

public class Dibujar : MonoBehaviour {
    public Transform basedot;
    public KeyCode mouseleft;

    private List<GameObject> dots;
    private Vector2 previousVector;

    void Start()
    {
        dots = new List<GameObject>();
    }

    void Update()
    {
        if (Input.GetKey(mouseleft))
        {
            Vector2 currentVector = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            float distance = Vector2.Distance(previousVector, currentVector);
            if (distance > 0.5)
            {
                List<Vector2> vectors = new List<Vector2>() { currentVector };
                if (distance > 1 && previousVector != Vector2.zero)
                {
                    float dx = currentVector.x - previousVector.x;
                    float dy = currentVector.y - previousVector.y;

                    float biggest = Math.Abs(dy);
                    if (Math.Abs(dx) > biggest) biggest = Math.Abs(dx);

                    float interval = biggest / 2f;

                    float incX = dx / interval;
                    float incY = dy / interval;

                    float indicator = 0;
                    float x = currentVector.x;
                    float y = currentVector.y;
                    while (Math.Abs(indicator) < Math.Abs(dx))
                    {
                        indicator += incX;
                        x += incX;
                        y += incY;
                        Vector2 vector = new Vector2(x, y);
                        vectors.Add(vector);
                    }
                }               

                foreach(Vector2 vector in vectors)
                {
                    Vector2 objPosition = Camera.main.ScreenToWorldPoint(vector);
                    GameObject t = Instantiate(basedot, objPosition, basedot.rotation).gameObject;
                    this.dots.Add(t);
                }                
            }
            this.previousVector = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        } else
        {
            previousVector = Vector2.zero;
        }
    }

    public void Borrar()
    {
        foreach(GameObject t  in this.dots)
        {
            Destroy(t);
        }
    }
}
