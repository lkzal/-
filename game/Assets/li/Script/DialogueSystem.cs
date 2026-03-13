using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    // 面板引用
    public GameObject dialoguePanel;
    public Text nameText;
    public Image headImage;
    public Text sentenceText;

    // 打字效果
    public float typeSpeed = 0.05f;

    // 玩家控制器（自动关联）
    private PlayerController player;

    // 内部数据
    private DialogueData currentData;
    private int sentenceIndex;
    private bool isTyping;

    void Start()
    {
        // 自动找到玩家
        player = FindObjectOfType<PlayerController>();
        // 默认关闭对话面板
        dialoguePanel.SetActive(false);
    }

    void Update()
    {
        // 正在打字 → 按空格跳过
        if (isTyping && Input.GetKeyDown(KeyCode.Space))
        {
            StopAllCoroutines();
            sentenceText.text = currentData.sentences[sentenceIndex];
            isTyping = false;
        }
        // 对话中 → 按空格下一句
        else if (dialoguePanel.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            NextSentence();
        }
    }

    // 外部调用：开始对话
    public void StartDialogue(DialogueData data)
    {
        currentData = data;
        sentenceIndex = 0;

        // 打开面板
        dialoguePanel.SetActive(true);
        // 设置名字、头像
        nameText.text = currentData.npcName;
        headImage.sprite = currentData.npcHead;

        // 锁住玩家
        player.SetCanMove(false);

        // 开始显示第一句
        StartCoroutine(TypeSentence());
    }

    // 逐字显示
    IEnumerator TypeSentence()
    {
        isTyping = true;
        sentenceText.text = "";

        foreach (char c in currentData.sentences[sentenceIndex])
        {
            sentenceText.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }

        isTyping = false;
    }

    // 下一句
    public void NextSentence()
    {
        if (isTyping) return;

        sentenceIndex++;

        if (sentenceIndex < currentData.sentences.Length)
        {
            StartCoroutine(TypeSentence());
        }
        else
        {
            EndDialogue();
        }
    }

    // 结束对话
    public void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        // 解锁玩家
        player.SetCanMove(true);
    }
}