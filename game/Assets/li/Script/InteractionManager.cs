using UnityEngine;
using UnityEngine.UI;

public class InteractionManager : MonoBehaviour
{
    public Text interactText;
    private NPCDialogue currentNPC;

    void Update()
    {
        if (currentNPC != null && Input.GetKeyDown(KeyCode.E))
        {
            // 触发对话
            FindObjectOfType<DialogueSystem>().StartDialogue(currentNPC.dialogueData);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            currentNPC = other.GetComponent<NPCDialogue>();
            interactText.text = "按 E 对话";
            interactText.enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            currentNPC = null;
            interactText.enabled = false;
        }
    }
}
