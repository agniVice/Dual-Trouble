﻿using System.Collections;
using UnityEngine;

public class PlayerScore : MonoBehaviour, ISubscriber, IInitializable
{
    public static PlayerScore Instance;

    public float Score { get; private set; }

    private bool _isInitialized;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }
    private void OnEnable()
    {
        if (!_isInitialized)
            return;

        SubscribeAll();
    }
    private void OnDisable()
    {
        UnsubscribeAll();
    }
    public void SubscribeAll()
    {
        GameState.Instance.GameFinished += Save;
    }

    public void UnsubscribeAll()
    {
        GameState.Instance.GameFinished += Save;
    }

    public void Initialize()
    {
        _isInitialized = true;
    }
    public void AddScore()
    {
        //Score++;
        GameState.Instance.ScoreAdded?.Invoke();
        Save();
    }
    private void Save()
    {
        Score = GameTimer.Instance.Score;
        if (Score > PlayerPrefs.GetFloat("HighScore", 0))
            PlayerPrefs.SetFloat("HighScore", Score);
    }
}