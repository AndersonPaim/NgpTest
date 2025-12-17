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
    
    private int _enemiesDead = 0;
    
    private void Start()
    {
        SetupEvents();
    }

    private void OnEnable()
    {
        _enemiesDead = 0;
        Time.timeScale = 1; 
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

    private void HandleEnemyDeath()
    {
        _enemiesDead++;

        if (_enemiesDead == _enemies.Count)
        {
            Time.timeScale = 0;
            _levelCompleteScreen.Open();
        }
    }

    private void HandlePlayerDeath()
    {
        Time.timeScale = 0;
        _levelFailedScreen.Open();
    }
}
