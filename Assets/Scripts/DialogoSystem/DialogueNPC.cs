using UnityEngine;

public class DialogueNPC : MonoBehaviour
{
    [SerializeField] private GameObject dialogMark;
    public DialogueLine[] dialogueLines;

    private bool playerInRange = false;
    public bool dialogueStarted = false;

    private void Update()
    {
        if (playerInRange && dialogueStarted == false &&
            Input.GetKeyDown(KeyCode.E))
        {
            DialogManager.instance.StartDialogue(dialogueLines);
            dialogueStarted = true;
        }

        if (dialogueStarted && DialogManager.instance.dialogueFinished == true)
        {
            dialogueStarted = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
