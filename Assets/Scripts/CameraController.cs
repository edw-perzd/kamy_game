using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float velocityCamera = 0.025f;
    public Vector3 desplazamiento;

    public void LateUpdate()
    {
        if(SceneManager.GetActiveScene().name != "CreditsScene")
        {
            Vector3 posicioDeseada = player.position + desplazamiento;
            Vector3 posicionSuavizada = Vector3.Lerp(transform.position, posicioDeseada, velocityCamera);

            transform.position = posicionSuavizada;
        }
    }
}
