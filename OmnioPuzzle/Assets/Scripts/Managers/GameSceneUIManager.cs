using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameSceneUIManager : MonoBehaviour {

    public GameObject activeMenu;

    [Space(10)]
    public IngamePanel inGamePanel;
    

    //Mevcut menüyü kapatır, parametre olarak gönderilen menüyü açar.
    public void ChangeActiveUI(GameObject newPanel) {
        if (activeMenu != null) {
            activeMenu.SetActive(false);
        }
        activeMenu = newPanel;
        newPanel.SetActive(true);
    }

    #region FROM_INSPECTOR
    //Inspectordan parametre olarak verdiğimiz menüler.
    #endregion
    

    private void Start() {
        ChangeActiveUI(inGamePanel.gameObject);
    }

}