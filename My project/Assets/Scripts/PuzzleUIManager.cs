using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;


public class PuzzleUIManager : MonoBehaviour
{
    public TMP_InputField answerInput;
    public GameObject hintPanel;
    public GameObject wrongPopup;
    public GameObject correctPopup;
    public Button hintButton;
    public Button closeHintButton;
    public Button laterButton;
    public Button confirmButton;
    public TMP_Text confirmButtonText;

    //private bool isAnswerInputActive = false;
    void Start()
    {
        hintButton.onClick.AddListener(OnHintClicked);
        closeHintButton.onClick.AddListener(OnCloseHintClicked);
        laterButton.onClick.AddListener(OnLaterClicked);
        confirmButton.onClick.AddListener(OnConfirmClicked);

        hintPanel.SetActive(false);
        if (wrongPopup == null)
        {
            Debug.LogError(" wrongPopup이 Inspector에서 연결되어 있지 않습니다!");
        }
        else
        {
            Debug.Log(" wrongPopup 연결 성공: " + wrongPopup.name);
            wrongPopup.SetActive(false); // 숨김
        }
        if (correctPopup == null)
        {
            Debug.LogError("correctPopup이 Inspector에서 연결되어 있지 않습니다!");
        }
        else
        {
            correctPopup.SetActive(false);
        }

        //wrongPopup.SetActive(false);
        answerInput.gameObject.SetActive(false);
        
    }


    void OnHintClicked(){ hintPanel.SetActive(true);}
    void OnCloseHintClicked(){hintPanel.SetActive(false);}
    void OnLaterClicked()
    { 
        //Debug.Log("나중에 풀기 클릭됨");
        SceneManager.LoadScene("skipscene");
    }
    
    bool isAnswerInputActive = false;
    public void OnConfirmClicked()
    {
        if (!isAnswerInputActive)
        {
            // 입력창이 아직 비활성화 상태 → 이제 보이게 하기
            answerInput.text = "";
            answerInput.gameObject.SetActive(true);
            isAnswerInputActive = true;
            confirmButton.GetComponentInChildren<TMP_Text>().text = "결정!!!";
            Debug.Log("정답을 입력하세요!");
            return;
        }
        //string userAnswer = answerInput.text.Trim();
        string userAnswer = answerInput.text.Trim().ToLower();
        Debug.Log("입력값: [" + userAnswer + "]");

        // 입력값이 비어있으면 무시 (오답 처리하지 않음)
        if (string.IsNullOrEmpty(userAnswer))
        {
            Debug.Log("아직 정답을 입력하지 않았습니다.");
            return;
        }

        if (userAnswer == "apple")
        {
            Debug.Log("정답입니다!!!");
            
            StartCoroutine(ShowCorrectPopupAndMoveScene());
        }
        else
        {
            Debug.Log("오답입니다!");
            StartCoroutine(ShowWrongPopupAndRetry());
        }
        //Debug.Log("결정 클릭됨");
    }

    IEnumerator ShowWrongPopupAndRetry()
    {
        wrongPopup.SetActive(true); // 오답 팝업 
        yield return new WaitForSeconds(2f); // 2초 기다림
        wrongPopup.SetActive(false); // 팝업 끄기

        // 같은 씬 다시 불러와서 문제 재시도함
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator ShowCorrectPopupAndMoveScene()
    {
        correctPopup.SetActive(true); // 팝업 보이기
        yield return new WaitForSeconds(2f); // 2초 대기
        correctPopup.SetActive(false); // 원한다면 끄기
        SceneManager.LoadScene("CorrectScene"); // 정답 씬 이동
    }

}
