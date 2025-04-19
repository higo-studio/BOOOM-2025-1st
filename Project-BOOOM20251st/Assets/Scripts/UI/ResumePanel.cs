using UnityEngine;

public class ResumePanel : MonoBehaviour
{
    CanvasGroup panel;

    void OnEnable()
    {
        panel = transform.GetComponent<CanvasGroup>();
    }

    public void OpenPanel()
    {
        panel.alpha = 1;
        panel.interactable = true;
        panel.blocksRaycasts = true;
        Time.timeScale = 0; //pause game
    }

    public void ClosePanel()
    {
        panel.alpha = 0;
        panel.interactable = false;
        panel.blocksRaycasts = false;
        Time.timeScale = 1; //resume game
    }

    public void QuitGame()
    {
        Debug.Log("Quit game");
        Application.Quit();
    }

    public void BackToTitle()
    { 
        Debug.Log("Back to title");
    }
}
