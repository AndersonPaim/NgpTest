using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCompletedUI : ScreenBase
{
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Button _homeLevelButton;
    
    private void Start()
    {
        SetupEvents();
        Initialize();
    }

    private void OnDestroy()
    {
        DestroyEvents();
    }

    private void SetupEvents()
    {
        _nextLevelButton.onClick.AddListener(HandleNextLevelButtonClicked);
        _homeLevelButton.onClick.AddListener(HandleHomeButtonClicked);
    }
        
    private void DestroyEvents()
    {
        _nextLevelButton.onClick.RemoveAllListeners();
        _homeLevelButton.onClick.RemoveAllListeners();
    }

    private void Initialize()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex + 1 >= SceneManager.sceneCountInBuildSettings)
        {
            _nextLevelButton.interactable = false;
        }
    }

    private void HandleNextLevelButtonClicked()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }

    private void HandleHomeButtonClicked()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
