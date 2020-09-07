using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    #region FROM_INSPECTOR
    public bool isGameActive;
    public bool isGameFinished;
    public int chocolatedSurface;
    #endregion

    #region UNITY_FUNCTIONS
    private void Start() {
        isGameActive = true;
    }

    float counter;
    private void Update() {
        if (isGameActive) {
            Time.timeScale = 1;
        } else {
            Time.timeScale = 0;
        }
    }
    #endregion

}