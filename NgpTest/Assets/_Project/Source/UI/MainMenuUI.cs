using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : ScreenBase
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _quitButton;
    
    private void Start()
    {
        SetupEvents();
    }

    private void OnDestroy()
    {
        DestroyEvents();
    }

    private void SetupEvents()
    {
        Time.timeScale = 1;
        _playButton.onClick.AddListener(HandlePlayButtonClicked);
        _quitButton.onClick.AddListener(HandleQuitButtonClicked);
    }
        
    private void DestroyEvents()
    {
        _playButton.onClick.RemoveAllListeners();
        _quitButton.onClick.RemoveAllListeners();
    }
    
    private void HandlePlayButtonClicked()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }

    private void HandleQuitButtonClicked()
    {
        Application.Quit();
    }
}