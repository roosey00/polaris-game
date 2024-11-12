using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

[Serializable]
public class Dialogue
{
    public string[] sentences;
    public GameObject image;
}

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public Dialogue[] dialogues;
    public GameObject arrowIndicator;
    public float typingSpeed = 0.1f;
    public float arrowRightPadding = 50f;
    public float arrowBottomPadding = 20f;
    
    private int currentDialogueIndex = 0;
    private int currentSentenceIndex = 0;
    private bool isTyping = false;
    private bool isComplete = false;
    private Coroutine typingCoroutine;
    private string fullsentence = "";
    
    string CurrentSentence
    {
        get { return dialogues[currentDialogueIndex].sentences[currentSentenceIndex]; }
    }

    void Start()
    {
        foreach (Dialogue dialogue in dialogues)
        {
            if (dialogue.image != null)
                dialogue.image.SetActive(false);
        }
        
        arrowIndicator.SetActive(false);

        if (dialogues.Length > 0)
        {
            StartDialogue();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Jump"))
        {
            if(isTyping)
            {
                StopCoroutine(typingCoroutine);
                typingCoroutine = null;
                dialogueText.text = fullsentence;
                isTyping = false;
                isComplete = true;
                PositionArrowIndicator();
            }
            else if (isComplete)
            {
                AdvanceSentence();
            }
        }
    }

    void StartDialogue()
    {
        dialogueText.text = "";
        fullsentence = "";
        currentSentenceIndex = 0;
        if (dialogues[currentDialogueIndex].image != null)
            dialogues[currentDialogueIndex].image.SetActive(true);
        AdvanceSentence();
    }

    void AdvanceSentence()
    {
        arrowIndicator.SetActive(false);

        if (currentSentenceIndex < dialogues[currentDialogueIndex].sentences.Length)
        {
            string sentence = dialogues[currentDialogueIndex].sentences[currentSentenceIndex];
            sentence = sentence.Replace("\\n", "\n");

            typingCoroutine = StartCoroutine(TypeSentence(sentence));
            currentSentenceIndex++;
        }
        else
        {  
            AdvanceDialogue();
        }
    }

    void AdvanceDialogue()
    {
        if (dialogues[currentDialogueIndex].image != null)
            dialogues[currentDialogueIndex].image.SetActive(false);
        
        currentDialogueIndex++;

        if (currentDialogueIndex < dialogues.Length)
        {
            dialogueText.text = "";
            fullsentence = "";
            currentSentenceIndex = 0;
            if (dialogues[currentDialogueIndex].image != null)
                dialogues[currentDialogueIndex].image.SetActive(true);
            AdvanceSentence();
        }
        else
        {
            SceneManager.LoadSceneAsync("TutorialScene");
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        isComplete = false;
        fullsentence += sentence;

        foreach (char letter in sentence)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
        isComplete = true;
        PositionArrowIndicator();
    }

    void PositionArrowIndicator()
    {
        arrowIndicator.SetActive(true);

        RectTransform arrowRect = arrowIndicator.GetComponent<RectTransform>();
        RectTransform canvasRect = arrowIndicator.transform.parent as RectTransform;

        // 우측 하단에 패딩을 설정하여 위치 조정
        arrowRect.anchoredPosition = new Vector2(
            canvasRect.rect.width / 2 - arrowRightPadding,
            -canvasRect.rect.height / 2 + arrowBottomPadding
        );
    }
}