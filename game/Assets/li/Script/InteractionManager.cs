using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public GameObject interactText;
    private NPCDialogue currentNPC;

    void Update()
    {
        // 按 E 触发对话
        if (currentNPC != null && Input.GetKeyDown(KeyCode.E))
        {
            DialogueSystem diaSys = FindObjectOfType<DialogueSystem>();
            if (diaSys != null && currentNPC.dialogueData != null)
            {
                diaSys.StartDialogue(currentNPC.dialogueData);
                interactText.SetActive(false);
            }
        }
    }

    // 3D 触发进入
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            currentNPC = other.GetComponent<NPCDialogue>();
            if (currentNPC != null)
            {
                interactText.SetActive(true);
            }
        }
    }

    // 3D 触发离开
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            currentNPC = null;
            interactText.SetActive(false);
        }
    }
}