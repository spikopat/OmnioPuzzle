using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitWithForkScript : MonoBehaviour {

    public Transform currentTurnLeftPoint;
    public Transform currentTurnRightPoint;

    [Space(10)]
    public Transform[] turnPoints;

    public int i = 0;
    private void Start() {
        currentTurnLeftPoint = turnPoints[i];
        currentTurnRightPoint = turnPoints[i + 1];
    }

    public void TurnRight() {
        i = (i + 1) % turnPoints.Length;
        currentTurnLeftPoint = turnPoints[i % turnPoints.Length];
        currentTurnRightPoint = turnPoints[(i + 1) % turnPoints.Length];
    }

    public void TurnLeft() {
        i = ((i - 1) + turnPoints.Length) % turnPoints.Length;
        currentTurnLeftPoint = turnPoints[i % turnPoints.Length];
        currentTurnRightPoint = turnPoints[((i + 1) + turnPoints.Length) % turnPoints.Length];
    }
}