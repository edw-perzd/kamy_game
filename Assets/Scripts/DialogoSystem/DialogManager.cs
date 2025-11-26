using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;

    [Header("Linked Components")]
    public TextMeshProUGUI nameBox;
    public TextMeshProUGUI textBox;
    public Image speakerImage;
    public GameObject dialogueGameObject;

    [Header("Text configuration")]
    public float typingSpeed = 0.05f;

    [Header("Dialogue Status")]
    public bool isTyping = false;
    public bool dialogueFinished = true;

    [Header("Dialogue")]
    public DialogueLine[] dialogueLines;

    #region PRIVATE VARIABLES
    private int currentIndex = 0;

    private Coroutine typingCoroutine;
    private bool justStarted = false;
    #endregion

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1"))
        {
            if (justStarted)
            {
                justStarted = false;
                return;
            }

            if (isTyping)
            {
                StopCoroutine(typingCoroutine);
                ShowFullLine(dialogueLines[currentIndex]);
                isTyping = false;
            }
            else
            {
                currentIndex++;

                if (currentIndex < dialogueLines.Length)
                {
                    typingCoroutine = StartCoroutine(TypeLine(dialogueLines[currentIndex]));
                }
                else
                {
                    textBox.text = "";
                    nameBox.text = "";
                    speakerImage.sprite = null;
                    dialogueFinished = true;
                    dialogueGameObject.SetActive(false);
                }
            }
        }
    }

    public void StartDialogue(DialogueLine[] newLines)
    {
        dialogueGameObject.SetActive(true);
        dialogueFinished = false;
        dialogueLines = newLines;
        currentIndex = 0;
        justStarted = true;
        typingCoroutine = StartCoroutine(TypeLine(dialogueLines[currentIndex]));
    }

    IEnumerator TypeLine(DialogueLine line)
    {
        isTyping = true;

        textBox.text = "";
        nameBox.text = line.speakerName;
        speakerImage.sprite = line.speakerPortrait;

        foreach (char c in line.dialogueText)
        {
            textBox.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    private void ShowFullLine(DialogueLine line)
    {
        textBox.text = line.dialogueText;
        nameBox.text = line.speakerName;
        speakerImage.sprite = line.speakerPortrait;
    }
}
