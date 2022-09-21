using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMenuIntro : MonoBehaviour
{
    public Texture2D texturaFundo;
    public Texture2D texturaLogo;
    public Texture2D texturaFelpudo;

    private float larguraLogo = 200;
    private float alturaLogo = 120;

    private float larguraFelpudo = 200;
    private float alturaFelpudo = 250;

    private float larguraBotao = 160;
    private float alturaBotao = 40;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnGUI()
    {
        //vai desenhar a textura 2d na tela, cria um flame retangular com o tamnaho da tela (largura e altura),usa essa textura, ocupa toda a tela
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texturaFundo, ScaleMode.StretchToFill);
        //x do logo na largura da tela - variavel - 10,10,
        GUI.DrawTexture(new Rect(Screen.width - larguraLogo - 10, 10, larguraLogo,alturaLogo),texturaLogo, ScaleMode.StretchToFill);
        //x=10 Y=/2
        GUI.DrawTexture(new Rect(10, Screen.height/2,larguraFelpudo,alturaFelpudo),texturaFelpudo,ScaleMode.StretchToFill);

        if(GUI.Button(new Rect(Screen.width/2 - larguraBotao/2, Screen.height/2 + alturaBotao + 30,larguraBotao,alturaBotao), "Jogar COM Camera"))
        {
            Application.LoadLevel("aviao3DAR");
        }
        if(GUI.Button(new Rect(Screen.width / 2 - larguraBotao / 2, Screen.height / 2 + alturaBotao - 30, larguraBotao, alturaBotao),"Jogar SEM Camera"))
        {
            Application.LoadLevel("aviao3D");
        }
        if (GUI.Button(new Rect(Screen.width / 2 - larguraBotao / 2, Screen.height / 2 + alturaBotao + 90, larguraBotao, alturaBotao), "Sair"))
        {
            Application.Quit();
        }

        
    }
}
