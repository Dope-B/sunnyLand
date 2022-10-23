using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class choiceManager : MonoBehaviour {

    public static choiceManager instance;
    private string question;
    private List<string> answerList;
    public GameObject go;
    public Text question_text;
    public Text[] answer_text;
    public GameObject[] answer_panel;
    public bool ischoice;
    private bool key_input;
    private int count;
    public int result;
    private WaitForSeconds wait = new WaitForSeconds(0.01f);
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

    // Use this for initialization
    void Start () {
        answerList = new List<string>();
        for(int i = 0; i < 4; i++)
        {
            answer_text[i].text = "";
            answer_panel[i].SetActive(false);
        }
        question_text.text = "";
        go.SetActive(false);
    }	
    public void showChoice(choice choice)
    {
        go.SetActive(true);
        ischoice = true;
        result = 0;
        question = choice.question;
        for(int i = 0; i < choice.answers.Length; i++)
        {
            answerList.Add(choice.answers[i]);
            count = i;
            answer_panel[i].SetActive(true);
        }
        selected();
        StartCoroutine(choiceC());
    }  
    IEnumerator ExitC()
    {
        for (int i = 0; i <= count; i++)
        {
            answer_text[i].text = "";
        }
        ischoice = false;
        question_text.text = "";
        answerList.Clear();
        yield return new WaitForSeconds(0.03f);
        go.SetActive(false);
    }
    IEnumerator choiceC()
    {
        yield return new WaitForSeconds(0.02f);
        StartCoroutine(typeQC());
        StartCoroutine(typeA0C());
        switch (count)
        {
            case 1:
                StartCoroutine(typeA1C());
                break;
            case 2:
                StartCoroutine(typeA1C());
                StartCoroutine(typeA2C());
                break;
            case 3:
                StartCoroutine(typeA1C());
                StartCoroutine(typeA2C());
                StartCoroutine(typeA3C());
                break;
        }
        yield return new WaitForSeconds(0.3f);
        key_input = true;
    }
    IEnumerator typeQC()
    {
        for(int i = 0; i < question.Length; i++)
        {
            question_text.text += question[i];
            yield return wait;
        }
    }
    IEnumerator typeA0C()
    {
        yield return new WaitForSeconds(0.02f);
        for (int i = 0; i < answerList[0].Length; i++)
        {
            answer_text[0].text += answerList[0][i];
            yield return wait;
        }
    }
    IEnumerator typeA1C()
    {
        yield return new WaitForSeconds(0.04f);
        for (int i = 0; i < answerList[1].Length; i++)
        {
            answer_text[1].text += answerList[1][i];
            yield return wait;
        }
    }
    IEnumerator typeA2C()
    {
        yield return new WaitForSeconds(0.06f);
        for (int i = 0; i < answerList[2].Length; i++)
        {
            answer_text[2].text += answerList[2][i];
            yield return wait;
        }
    }
    IEnumerator typeA3C()
    {
        yield return new WaitForSeconds(0.08f);
        for (int i = 0; i < answerList[3].Length; i++)
        {
            answer_text[3].text += answerList[3][i];
            yield return wait;
        }

    }
    // Update is called once per frame
    void Update () {
        if (key_input)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (result >0)
                {
                    result--;
                    selected();
                }
                else { result = count; selected(); }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (result <count)
                {
                    result++;
                    selected();
                }
                else { result = 0; selected(); }
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                key_input = false;
                StartCoroutine(ExitC());
            }
            
        }
	}
    public void selected()
    {
        Color color = answer_panel[0].GetComponent<Image>().color;
        color.a = 0.75f;
        for(int i = 0; i <= count; i++)
        {
            answer_panel[i].GetComponent<Image>().color = color;
        }
        color.a = 1f;
        answer_panel[result].GetComponent<Image>().color = color;
    }
    public int getResult()
    {
        return result;
    }

}
