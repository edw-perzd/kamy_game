using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    public Image rellenoBarraVida;
    private Player player;
    private float vidaMaxima;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        vidaMaxima = player.vidaMax;
    }

    // Update is called once per frame
    void Update()
    {
        rellenoBarraVida.fillAmount = (float)player.health / vidaMaxima;
    }
}
