
using UnityEngine;

[System.Serializable]

public class DialogueLine
{
    public string speakerName;
    [TextArea(1, 3)]
    public string dialogueText;
    public Sprite speakerPortrait;


}
