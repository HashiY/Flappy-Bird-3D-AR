﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class actionCenaPrincipal : MonoBehaviour {

	private bool cartaoDetectado;
	private bool comecou;
	private bool acabou;
	private bool podeReiniciar;

	private float scrollSpeed;
	private float velocidadeObjeto = -75f;
    private float posicaoZInicialObjetos = 2.5f;
	private GameObject objetoX; 
	private float velocidade;
	private int score;


	public Material materialPiso;
	public GameObject nodeRootCena;
    public GameObject cerca;
	public GameObject canos; 
	public GameObject arbusto;
	public GameObject nuvem;
	public GameObject pedras;
	public GameObject passarinho;
	public GameObject peninha;

    public Text meuScore;

    //public AudioSource somMusica; 
	public AudioClip somFinal;
	public AudioClip somHit;
	public AudioClip somVoa;
	public AudioClip somScore;
	public AudioClip somPick; 


	void OnGUI() 
	{
        if (GUI.Button(new Rect(1000, 10, 30, 30), "X"))
        {
            Application.LoadLevel("Menu");
        }
    } 


	void Start () {
        Physics.gravity = new Vector3(0, -25.0F, 0); // aumenta a  gravidade da cena

        if (Application.loadedLevelName == "aviao3DAR")
		{
            cartaoDetectado = true;
        } else
        {
            cartaoDetectado = false;
        }
		
		
		Time.timeScale = 1f; 
		scrollSpeed = 0.0f;

		InvokeRepeating("criaCerca", 1, 0.15f);
		InvokeRepeating("criaCano", 1, 5.13F);
		InvokeRepeating("criaArbustoNuvemPedra", 1, 0.75f);

        //para isso funcionar teriaque colocar um audio sorce na hierarquia e colocar isso como gameobject
		 //somMusica.loop = true;
		 //somMusica.volume = 2.0f;
		 //somMusica.Play();
	}
	
	// Update is called once per frame
	void Update () 
	{
		float offset = Time.time * scrollSpeed;
	    materialPiso.SetTextureOffset("_MainTex", new Vector2(offset, 0));


		if(Input.anyKeyDown)
		{ 
			if(!acabou)
			{
				if(comecou)
				{ 
					voaBird();
				}else
					
				{
					passarinho.transform.position = new Vector3(0f, 1.76f, -1.25f);
					passarinho.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);  
					passarinho.GetComponent<Rigidbody>().useGravity = true;
					comecou = true;
					scrollSpeed = -0.5f;
					voaBird();
                    meuScore.text = "";
                    meuScore.text = score.ToString();
				}

			}else{
				 if (podeReiniciar)
				 {
				 	apagaTodosObjetos();  
						
					}
					
				}
		}


	 }


	void criaCerca()
	 {
		if(comecou && cartaoDetectado){
			GameObject novoObjeto = (GameObject) Instantiate(cerca); 
			novoObjeto.transform.parent = nodeRootCena.transform;
			novoObjeto.transform.position = new Vector3(0,0.5f,posicaoZInicialObjetos);
			//novoObjeto.transform.rotation = Quaternion.Euler(-90,0,0);
			//novoObjeto.GetComponent<Rigidbody>().AddForce (new Vector3(0,0,velocidadeObjeto), ForceMode.Force);
            
		}
	}


	void criaCano()
	{
		if(comecou && cartaoDetectado){
			var offSetCano = Random.Range(-0.5f,0.0f);
			GameObject novoObjeto = (GameObject) Instantiate(canos); 
			novoObjeto.transform.parent = nodeRootCena.transform;
			novoObjeto.transform.position = new Vector3(0,offSetCano,posicaoZInicialObjetos);
			novoObjeto.transform.rotation = Quaternion.Euler(0,0,0);
			//novoObjeto.GetComponent<Rigidbody>().AddForce (new Vector3(0,0,velocidadeObjeto), ForceMode.Force);
		}
	}


	void criaArbustoNuvemPedra()
	 {  
		if(comecou && cartaoDetectado)
		{
			
			var sorteiaObjeto = Random.Range(1,4); 

			var offSetNuvem = Random.Range(1.1f,2.5f);

			var giroRandom = Random.Range(-180.0f,180.0f);

			var giroNuvem = 0.0f;
			var posicaoX = 0.0f;
			
			if ((Random.Range(1,3)%2)==0) { 
				posicaoX = -0.45f;  
				giroNuvem = 0.0f;
				
			}else{ 
				posicaoX = 0.45f; 
				giroNuvem = 180.0f;
			}
			
			
			
			switch (sorteiaObjeto) 
			{ 
			case 1: 
				objetoX = (GameObject) Instantiate(arbusto); 
				objetoX.transform.parent = nodeRootCena.transform;
				objetoX.transform.position = new Vector3(posicaoX,0.5f,posicaoZInicialObjetos);
				objetoX.transform.rotation = Quaternion.Euler(0,giroRandom,0);
				break;
				
			case 2:  
				objetoX = (GameObject) Instantiate(nuvem); 
				objetoX.transform.parent = nodeRootCena.transform;
				objetoX.transform.position = new Vector3(posicaoX,offSetNuvem,posicaoZInicialObjetos);
				objetoX.transform.rotation = Quaternion.Euler(0,giroNuvem,0);
				break;
				
			case 3:  
				
				objetoX = (GameObject) Instantiate(pedras); 
				objetoX.transform.parent = nodeRootCena.transform;
				objetoX.transform.position = new Vector3(posicaoX,0.5f,posicaoZInicialObjetos);
				objetoX.transform.rotation = Quaternion.Euler(0,giroRandom,0);
				break;
			default: break;
			}
			
			//objetoX.GetComponent<Rigidbody>().AddForce (new Vector3(0,0,velocidadeObjeto), ForceMode.Force); 

		}

 	} 





	private void voaBird()
	{ 
		passarinho.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
		passarinho.GetComponent<Rigidbody>().AddForce (new Vector3(0,200,0), ForceMode.Force);
		criaPeninhas();
		GetComponent<AudioSource>().PlayOneShot(somVoa, 0.5F);
	}  


	void LateUpdate () 
	{
		 velocidade = passarinho.GetComponent<Rigidbody>().velocity.y; 
		 passarinho.transform.rotation = Quaternion.Euler(velocidade*-5,0, 0);
	 }


	public void fimDeJogo()
	 {
		 if(!acabou)
		 {
			 acabou = true;
			 print("Game Over");
			 comecou = false;
			 scrollSpeed = 0.0f; 
			paraObjetos();
			Invoke("setEstadoReload", 2);
			GetComponent<AudioSource>().PlayOneShot(somHit, 0.35F);
		 }
	 }

	void paraObjetos()
		
	{
		 var objects = GameObject.FindGameObjectsWithTag("O");
		 foreach (var obj in objects) {
		 if(obj != null){
			 obj.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
				
				//    Destroy(obj);
		  }
	 }
		
		
		
	}



	void setEstadoReload()
		
	{ 
		podeReiniciar = true;
        meuScore.text ="Toque para reiniciar!!!";
		GetComponent<AudioSource>().PlayOneShot(somFinal, 0.25F);
	 }



	void apagaTodosObjetos()
	 {
	 	var gameObjects = GameObject.FindGameObjectsWithTag ("O");
	 	for(var i = 0 ; i < gameObjects.Length ; i ++)
		 {
		 	Destroy(gameObjects[i]);
		 }
		passarinho.transform.position = new Vector3(0.0f, 1.0f, -0.5f);
		passarinho.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);  
		passarinho.GetComponent<Rigidbody>().useGravity = true;
		comecou = true;
		acabou = false;
		podeReiniciar = false;
		scrollSpeed = -0.5f;
		voaBird();
        meuScore.text ="";
		score=0;
        meuScore.text = score.ToString();
	}





	public void updateScore()
		
	{ 
		score++;
        meuScore.text = score.ToString();
		GetComponent<AudioSource>().PlayOneShot(somScore, 0.55F);
	 }
	

	public void criaPeninhas()
	{
        GameObject minhasParticulas = Instantiate(peninha);
        minhasParticulas.transform.position = passarinho.transform.position; // vai estar na posi do jogador
        minhasParticulas.transform.rotation = passarinho.transform.rotation;

        passarinho.GetComponent<Rigidbody>().velocity = Vector3.zero; // zera
        passarinho.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 6.0f, 0.0f); // para cima
    }	
	
	
	public void cartaoAtivado(){ cartaoDetectado = true; } 
	
	public void cartaoDesativado(){ cartaoDetectado = false; } 
	

	


}
























