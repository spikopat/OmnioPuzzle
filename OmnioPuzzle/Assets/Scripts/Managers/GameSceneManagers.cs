using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GameManager))]
[RequireComponent(typeof(GameSceneUIManager))]
[RequireComponent(typeof(GameInputManager))]
[RequireComponent(typeof(GameSceneScoreManager))]
[RequireComponent(typeof(GameSceneSpawnManager))]
public class GameSceneManagers : MonoBehaviour {

    private static GameManager _gameManager;
    public static GameManager Game {
        get { return _gameManager; }
    }

    private static GameSceneUIManager _gameSceneUIManager;
    public static GameSceneUIManager UI {
        get { return _gameSceneUIManager; }
    }

    private static GameInputManager _gameInputManager;
    public static GameInputManager Input {
        get { return _gameInputManager; }
    }

    private static GameSceneScoreManager _gameSceneScoreManager;
    public static GameSceneScoreManager Score {
        get { return _gameSceneScoreManager; }
    }

    private static GameSceneSpawnManager _gameSceneSpawnManager;
    public static GameSceneSpawnManager Spawn {
        get { return _gameSceneSpawnManager; }
    }

    void Awake() {
        _gameManager = GetComponent<GameManager>();
        _gameSceneUIManager = GetComponent<GameSceneUIManager>();
        _gameInputManager = GetComponent<GameInputManager>();
        _gameSceneScoreManager = GetComponent<GameSceneScoreManager>();
        _gameSceneSpawnManager = GetComponent<GameSceneSpawnManager>();
    }

}