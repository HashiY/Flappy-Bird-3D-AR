using UnityEngine;
using System.Collections;

public class Obstaculo : MonoBehaviour {

    private bool cresce = false;
    private bool diminui = false;
    private Vector3 vetorEscalaObj = new Vector3(0.035f, 0.035f, 0.035f);

	// Use this for initialization
	void Start () {
        this.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);//a escala do ogj quando começa
        cresce = true;
        this.transform.position = new Vector3(this.transform.position.x,this.transform.position.y,7); // eu - começa nessa posiçao
		GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, -5.0f); //  andar
	}
	
	// Update is called once per frame
	void Update () {
		if(this.transform.position.z < -6){ // quebra se chegar aqui
			Destroy(this.gameObject);
		}
        if((this.transform.localScale.x < 1.0f) && cresce) // se a escala for menor q isso 
        {
            transform.localScale += vetorEscalaObj; // vai aumentando
        }
        else
        {
            cresce = false;
        }
        if(this.transform.position.z <= -4f) // se chegar aqui
        {
            diminui = true;
        }
        if (diminui)
        {
            
            if(this.transform.localScale.x >= 0.01f) // vai diminuindo ate chegar a essa escala
            {
                transform.localScale -= vetorEscalaObj;
            }
            else
            {
                diminui = true;
            }
        }
	}
}
