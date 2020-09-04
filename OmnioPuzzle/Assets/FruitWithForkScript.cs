using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitWithForkScript : MonoBehaviour {

    public Transform currentTurnLeftPoint;
    public Transform currentTurnRightPoint;

    [Space(10)]
    public Transform[] turnPoints;

    [Space(10)]
    public GameObject[] chocolateSurfaces;

    int i = 0;
    private void Start() {
        currentTurnLeftPoint = turnPoints[i];
        currentTurnRightPoint = turnPoints[i + 1];
    }

    public void TurnRight(bool chocolated) {
        i = (i + 1) % turnPoints.Length;
        currentTurnLeftPoint = turnPoints[i % turnPoints.Length];
        currentTurnRightPoint = turnPoints[(i + 1) % turnPoints.Length];

        //Çikolatalanan yüzeyi aktif et.
        if (chocolated)
            chocolateSurfaces[i].SetActive(true);
    }

    public void TurnLeft(bool chocolated) {
        i = ((i - 1) + turnPoints.Length) % turnPoints.Length;
        currentTurnLeftPoint = turnPoints[i % turnPoints.Length];
        currentTurnRightPoint = turnPoints[((i + 1) + turnPoints.Length) % turnPoints.Length];

        //Çikolatalanan yüzeyi aktif et.
        if (chocolated)
            chocolateSurfaces[i].SetActive(true);
    }

    public void MoveForward(bool chocolated) {
        //Çikolatalanan yüzeyi aktif et.
        if (chocolated)
            chocolateSurfaces[i].SetActive(true);
    }

    public void MoveBack(bool chocolated) {
        //Çikolatalanan yüzeyi aktif et.
        if (chocolated)
            chocolateSurfaces[i].SetActive(true);
    }
}