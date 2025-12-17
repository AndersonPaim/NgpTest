using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelFailedUI : ScreenBase
{
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _restartButton;
    
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
        _homeButton.onClick.AddListener(HandleHomeButtonClicked);
        _restartButton.onClick.AddListener(HandleRestartButtonClicked);
    }
        
    private void DestroyEvents()
    {
        _homeButton.onClick.RemoveAllListeners();
        _restartButton.onClick.RemoveAllListeners();
    }

    private void HandleHomeButtonClicked()
    {
        SceneManager.LoadScene(0);
    }

    private void HandleRestartButtonClicked()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}