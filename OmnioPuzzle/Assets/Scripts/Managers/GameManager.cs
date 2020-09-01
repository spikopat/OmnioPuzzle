using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    #region FROM_INSPECTOR
    public bool isGameActive;
    #endregion

    #region UNITY_FUNCTIONS
    private void Start() {
        isGameActive = true;
    }

    private void Update() {
        if (isGameActive) {
            Time.timeScale = 1;
        } else {
            Time.timeScale = 0;
        }
    }
    #endregion

}