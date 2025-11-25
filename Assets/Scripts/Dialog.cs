using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    [SerializeField] private GameObject dialogMark;
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogLines;
    private bool isPlayerInRange;
    private bool didDialogStart;
    private int lineIndex;

    private float typingTime = 0.05f;

    void Update()
    {
        if(isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!didDialogStart)
            {
                StartDialog();
            }
            else if(dialogueText.text == dialogLines[lineIndex])
            {
                NextDialogLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = dialogLines[lineIndex];
            }
        }
    }

    private void StartDialog()
    {
        didDialogStart = true;
        dialogPanel.SetActive(true);
        dialogMark.SetActive(false);
        lineIndex = 0;
        Time.timeScale = 0f;
        StartCoroutine(ShowLine());
    }

    private void NextDialogLine()
    {
        lineIndex++;
        if (lineIndex < dialogLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            didDialogStart = false;
            dialogPanel.SetActive(false);
            dialogMark.SetActive(true);
            Time.timeScale = 1f;
        }
    }
    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;

        foreach(char c in dialogLines[lineIndex])
        {
            dialogueText.text += c;
            yield return new WaitForSecondsRealtime(typingTime);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            dialogMark.SetActive(true);
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            dialogMark.SetActive(false);
            isPlayerInRange = false;
        }
    }
}
