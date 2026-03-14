using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI nameText;    //   ≈‰TextMeshPro
    public Image headImage;
    public TextMeshProUGUI sentenceText;
    

    public float typeSpeed = 0.05f;
    private PlayerController player;
    private DialogueData currentData;
    private int index;
    private bool isTyping;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (dialoguePanel == null || !dialoguePanel.activeSelf)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                StopAllCoroutines();
                sentenceText.text = currentData.sentences[index];
                isTyping = false;
            }
            else
            {
                NextSentence();
            }
        }
    }

    public void StartDialogue(DialogueData data)
    {
        currentData = data;
        index = 0;

        dialoguePanel.SetActive(true);
        nameText.text = currentData.npcName;
        headImage.sprite = currentData.npcHead;

        player.SetCanMove(false);
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        isTyping = true;
        sentenceText.text = "";
        foreach (char c in currentData.sentences[index])
        {
            sentenceText.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }
        isTyping = false;
    }

    void NextSentence()
    {
        index++;
        if (index < currentData.sentences.Length)
        {
            StartCoroutine(TypeText());
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        player.SetCanMove(true);
    }
}