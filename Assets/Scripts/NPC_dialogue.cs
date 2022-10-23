using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_dialogue : MonoBehaviour {

    [SerializeField]
    public Dialogue[] dialogue;
    private choiceManager CM;
    public choice[] choice;
    private DialogueManager DM;
    private player_movement player;
    private int jct;
    private bool flg;
	// Use this for initialization
	void Start () {
        DM = FindObjectOfType<DialogueManager>();
        CM = FindObjectOfType<choiceManager>();
        player = FindObjectOfType<player_movement>();
    }
	
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Player"&&Input.GetKeyDown(KeyCode.X)&&!player.istalking)
        {
            StartCoroutine(StartDialogue());
        }
    }
    IEnumerator StartDialogue()
    {
        DM.ShowDialogue(dialogue[0]);
        yield return new WaitUntil(() => DM.nextDialogue);
        if (choice.Length!=0)
        {
            CM.showChoice(choice[0]);
            yield return new WaitUntil(() => !CM.ischoice);
            switch (CM.getResult())
            {
                case 0:
                    Debug.Log("1 start");
                    DM.ShowDialogue(dialogue[1]);
                    yield return new WaitUntil(() => DM.nextDialogue);
                    Debug.Log("1 end");
                    break;
                case 1:
                    DM.ShowDialogue(dialogue[2]);
                    yield return new WaitUntil(() => DM.nextDialogue);
                    break;
                case 2:
                    DM.ShowDialogue(dialogue[3]);
                    yield return new WaitUntil(() => DM.nextDialogue);
                    break;
                default:
                    break;
            }
        }
        DM.ExitDialogue();

    }
}
