using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public static DialogueManager instance;

    #region SingleTon
    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;     
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion SingleTon

    public Text text;
    public SpriteRenderer renderSprite;
    public SpriteRenderer renderDialogueBox;
    private player_movement player;
    private List<string> listSentences;
    private List<Sprite> listSprites;
    private List<Sprite> listDialogueBoxes;
    public Animator animSprite;
    public Animator animDialogueBox;
    private AudioManager Audio;
    private bool isHold=false;
    private int count = 0;
    private bool iscomplete = false;
    public bool nextDialogue = false;
    private bool dialogueOn = false;
    private bool changeDone = true;

    // Use this for initialization
    void Start()
    {
        count = 0;
        text.text = "";
        listSentences = new List<string>();
        listSprites = new List<Sprite>();
        listDialogueBoxes = new List<Sprite>();
        player = FindObjectOfType<player_movement>();
        Audio = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        checkDialogue();
    }
    void checkDialogue()
    {
        if (player.istalking && iscomplete)
        {
            
            if (Input.GetKeyDown(KeyCode.X))
            {
                count++;
                text.text = "";
                if (count == listSentences.Count)
                {
                    holdDialogue();
                }
                else
                {
                    StartCoroutine(startDialogue());
                    iscomplete = false;
                }
            }
        }
        if (player.istalking && !iscomplete && Input.GetKeyDown(KeyCode.X)&&dialogueOn)
        {
            text.text = "";
            text.text = listSentences[count];
            iscomplete = true;
            dialogueOn = false;
            StopAllCoroutines();
        }
    }

    public void ShowDialogue(Dialogue dialogue)
    {
        player.istalking = true;
        nextDialogue = false;
        for (int i = 0; i < dialogue.sentences.Length; i++)
        {
            listSentences.Add(dialogue.sentences[i]);
            listSprites.Add(dialogue.portraits[i]);
            listDialogueBoxes.Add(dialogue.dialogueBoxs[i]);
        }
        if (!isHold)
        {
            animSprite.SetBool("appear", true);
            animDialogueBox.SetBool("appear", true);
        }
        StartCoroutine(startDialogue());
    }
    IEnumerator startDialogue()
    {
        if (count > 0)
        {
            if (listSprites[count] != listSprites[count - 1])
            {
                changeDone = false;
                animSprite.SetBool("appear", false);
                yield return new WaitForSeconds(0.05f);
                renderSprite.sprite = listSprites[count];
                animSprite.SetBool("appear", true);
                yield return new WaitForSeconds(0.05f);
                changeDone = true;
            }
            else
            {
                yield return new WaitForSeconds(0.3f);
            }
        }
        else
        {
            renderDialogueBox.sprite = listDialogueBoxes[count];
            renderSprite.sprite = listSprites[count];
        }
        
        for (int i = 0; i < listSentences[count].Length; i++)
        {  
                yield return new WaitUntil(() => changeDone); 
                text.text += listSentences[count][i];
                yield return new WaitForSeconds(0.04f);
                dialogueOn = true;
            if (i % 3 == 1) { Audio.play("dialogue"); }
        }
        dialogueOn = false;
        iscomplete = true;
    }
    public void ExitDialogue()
    {
        StopAllCoroutines();
        count = 0;
        text.text = "";
        listSentences.Clear();
        listSprites.Clear();
        listDialogueBoxes.Clear();
        animSprite.SetBool("appear", false);
        animDialogueBox.SetBool("appear", false);
        player.istalking = false;
        iscomplete = false;
        dialogueOn = false;
        isHold = false;
        nextDialogue = true;
    }
    void holdDialogue()
    {
        StopAllCoroutines();
        count = 0;
        text.text = "";
        listSentences.Clear();
        listSprites.Clear();
        listDialogueBoxes.Clear();
        iscomplete = false;
        nextDialogue = true;
    }
}
