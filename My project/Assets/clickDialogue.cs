using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClickDialogue : MonoBehaviour
{
    public GameObject TalkPanel;
    public GameObject ClickTag;
    public TextMeshProUGUI nametag;
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public string[] names;
    public float textSpeed;

    private int index;
    private int nin;
    private bool talking = false;
    void Start()
    {
        TalkPanel.SetActive(false);
    }


    void Update()
    {
        //마우스 클릭시
        if (Input.GetMouseButtonDown(0))
        {
            //마우스 클릭한 좌표값 가져오기
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //해당 좌표에 있는 오브젝트 찾기
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

            if (hit.collider != null && talking != true)
            {
                GameObject click_obj = hit.transform.gameObject;
                if (click_obj.tag == ClickTag.tag)
                {
                    TalkPanel.SetActive(true);
                    textComponent.text = string.Empty;
                    nametag.text = string.Empty;
                    StartDialogue();

                }

            }
            else if (textComponent.text == lines[index] && talking) {
                NextLine();
            }
            else {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }

        }
    }

    void StartDialogue()
    {
        index = 0;
        nin = 0;
        StartCoroutine(TypeLine());
        talking = true;
    }

    IEnumerator TypeLine()
    {
        foreach (char n in names[nin].ToCharArray())
        {
            nametag.text += n;
            yield return new WaitForSeconds(textSpeed);
        }
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            nin++;
            textComponent.text = string.Empty;
            nametag.text = string.Empty;

            StartCoroutine(TypeLine());
        }
        else
        {
            TalkPanel.SetActive(false);
            talking = false;
        }
    }
}
