using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float velocityCamera = 0.025f;
    public Vector3 desplazamiento;

    public void LateUpdate()
    {
        Vector3 posicioDeseada = player.position + desplazamiento;
        Vector3 posicionSuavizada = Vector3.Lerp(transform.position, posicioDeseada, velocityCamera);

        transform.position = posicionSuavizada;
    }
}
