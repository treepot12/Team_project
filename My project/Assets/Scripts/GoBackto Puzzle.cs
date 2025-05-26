using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CorrectSceneManager : MonoBehaviour
{
    public Button backButton;

    void Start()
    {
        backButton.onClick.AddListener(GoBackToPuzzle);
    }

    void GoBackToPuzzle()
    {
        //SceneManager.LoadScene("버튼 구현"); 
    }
}
