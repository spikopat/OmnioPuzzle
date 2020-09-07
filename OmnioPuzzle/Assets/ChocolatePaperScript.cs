using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChocolatePaperScript : MonoBehaviour {

    //Kendi etrafında döndürür.
    public void FirstState() {
        transform.DORotate(new Vector3(0, 270, 0), 3, RotateMode.Fast).SetLoops(1, LoopType.Incremental).SetEase(Ease.InSine).OnComplete(()=> SecondState());
    }

    //Yukarı kaldırarak döndürür. Yukarı kaldırma bittiğinde sallar.
    void SecondState() {
        transform.DOMoveY(0.28f, 0.5f).OnComplete(() => transform.DOShakeRotation(0.5f, 45, 10, 90, false));
        transform.DORotate(new Vector3(0, 180, 0), 0.2f, RotateMode.FastBeyond360).SetLoops(5, LoopType.Incremental).SetEase(Ease.Linear).OnComplete(() => ThirdState());
    }

    //Aşağı indirerek döndürür.
    void ThirdState() {
        transform.DORotate(new Vector3(0, 0, 0), 1, RotateMode.FastBeyond360).SetLoops(1, LoopType.Incremental).SetEase(Ease.OutQuad);
        transform.DOMoveY(0.25f, 1);
    }

}