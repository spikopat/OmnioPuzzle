using UnityEngine;
using System.Collections;
using DG.Tweening;

public class GameManager : MonoBehaviour {

    #region FROM_INSPECTOR
    public bool isGameActive;
    public bool isGameFinished;

    private int chocolatedSurface;
    public int ChocolatedSurface {
        get {
            return chocolatedSurface;
        }
        set {
            chocolatedSurface = value;
            if (chocolatedSurface == 4) {
                isGameFinished = true;
                FinishAnim1();
            }
        }
    }
    #endregion

    //Çatalı yukarı kaldır ve 2.animasyonu tetikle.
    void FinishAnim1() {
        GameObject temp = GameSceneManagers.Spawn.spawnedFruitWithFork.gameObject;
        temp.transform.DOMoveY(0.38f, 0.5f).OnComplete(() => FinishAnim2(temp)).SetDelay(0.25f);
    }

    //Çatalı döndür ve 3.animasyonu tetikle.
    void FinishAnim2(GameObject temp) {
        temp = temp.transform.GetChild(0).gameObject;
        temp.transform.DOLocalRotate(new Vector3(90, 0, 0), 0.1f).SetLoops(3, LoopType.Incremental).SetEase(Ease.Linear).OnComplete(() => FinishAnim3());
    }

    //Çatalı dışarı at ve 4.animasyonu tetikle.
    void FinishAnim3() {
        GameObject temp = GameSceneManagers.Spawn.spawnedFruitWithFork.gameObject;
        temp.transform.DORotate(new Vector3(0, -90, 0), 1).OnComplete(() => FinishAnim4(temp)).SetDelay(0.5f);
        temp.transform.DOMove(new Vector3(-3, 0, 0), 1.5f).SetDelay(0.5f);
    }

    //Bitiş kamerası animasyonunu aktif et, çikolata kabının animasyonlarını tetikle ve çatallı meyveyi yok et.
    void FinishAnim4(GameObject temp) {
        Camera.main.GetComponent<Animator>().SetTrigger("CameraFinishAnim");
        GameObject.FindObjectOfType<ChocolatePaperScript>().FirstState();
        Destroy(temp);
    }

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