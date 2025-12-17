using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : ScreenBase
{
    [SerializeField] private Button _playButton;
    
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
        _playButton.onClick.AddListener(HandlePlayButtonClicked);
    }
        
    private void DestroyEvents()
    {
        _playButton.onClick.RemoveAllListeners();
    }
    
    private void HandlePlayButtonClicked()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }
}