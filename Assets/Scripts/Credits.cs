using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Credits : MonoBehaviour
{
    public float scrollSpeed = 40f;
    public float endY = 900f;               // Ajusta este valor según tu UI
    public TMP_Text pressAnyKeyText;             // Referencia al texto
    private RectTransform rectTransform;
    private bool finished = false;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        pressAnyKeyText.gameObject.SetActive(false);   // Ocultar texto al inicio
    }

    void Update()
    {
        if (!finished)
        {
            // Mover créditos
            rectTransform.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);

            // Revisar si llegaron al final
            if (rectTransform.anchoredPosition.y >= endY)
            {
                finished = true;
                pressAnyKeyText.gameObject.SetActive(true); // Mostrar texto
            }
        }
        else
        {
            // Esperar cualquier tecla
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }
}
