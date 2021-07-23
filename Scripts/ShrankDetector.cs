using System;
using UnityEngine;

public class ShrankDetector : MonoBehaviour
{
    public float yScale = 0.9775727f;
    public float yTransform = 0.25f;

    Vector3 transo;
    Vector3 scale;

    Vector3 transoOra;
    Vector3 scaleOra;

    private void Start()
    {
        transo = gameObject.transform.position;
        scale = gameObject.transform.localScale;

        transoOra = transo;
        scaleOra = scale;

    }
    public void ShrankAllCards()
    {
      
        //transo = new Vector3(gameObject.transform.position.x, yTransform, gameObject.transform.position.z);
        //this.transform.position = transo;


        //scale.y = yScale;
        //this.transform.localScale = scale;

    }
    public void Deshrank() 
    {
        transoOra = new Vector3(gameObject.transform.position.x, transoOra.y, gameObject.transform.position.z);
        this.transform.position = transoOra;


        scaleOra.y = yScale;
        this.transform.localScale = scaleOra;


    }

}
