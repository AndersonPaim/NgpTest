using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private List<Enemy> _enemies = new();
    [SerializeField] private ScreenBase _levelCompleteScreen;
    [SerializeField] private ScreenBase _levelFailedScreen;
    [SerializeField] private int _endGameDelay = 1;
    
    private int _enemiesDead = 0;
    
    private void Start()
    {
        Time.timeScale = 1;
        SetupEvents();
    }

    private void OnDestroy()
    {
        DestroyEvents();
    }

    private void SetupEvents()
    {
        foreach (Enemy enemy in _enemies)
        {
            enemy.OnDeath += HandleEnemyDeath;
        }
        
        _player.OnDeath += HandlePlayerDeath;
    }
        
    private void DestroyEvents()
    {
        foreach (Enemy enemy in _enemies)
        {
            enemy.OnDeath -= HandleEnemyDeath;
        }
        
        _player.OnDeath -= HandlePlayerDeath;
    }

    private async void HandleEnemyDeath()
    {
        _enemiesDead++;

        if (_enemiesDead == _enemies.Count)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_endGameDelay));
            Time.timeScale = 0;
            _levelCompleteScreen.Open();
        }
    }

    private async void HandlePlayerDeath()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_endGameDelay));
        Time.timeScale = 0;
        _levelFailedScreen.Open();
    }
}
