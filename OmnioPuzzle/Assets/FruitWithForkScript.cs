using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitWithForkScript : MonoBehaviour {

    public Transform currentTurnLeftPoint;
    public Transform currentTurnRightPoint;
    TestMeshCtr testMeshCtr;

    [Space(10)]
    public Transform[] turnPoints;

    [Space(10)]
    public GameObject[] chocolateSurfaces;

    int i = 0;
    private void Start() {
        currentTurnLeftPoint = turnPoints[i];
        currentTurnRightPoint = turnPoints[i + 1];
        testMeshCtr = transform.GetChild(0).GetComponent<TestMeshCtr>();
    }

    public void TurnRight(bool chocolated) {
        testMeshCtr.AddForcce(new Vector3(0, 0.0375f, 0.0375f), 100);
        i = (i + 1) % turnPoints.Length;
        currentTurnLeftPoint = turnPoints[i % turnPoints.Length];
        currentTurnRightPoint = turnPoints[(i + 1) % turnPoints.Length];

        //Çikolatalanan yüzeyi aktif et.
        if (chocolated) { 
            chocolateSurfaces[i].SetActive(true);
            //chocolateSurfaces[i].transform.GetChild(0).GetComponent<ParticleSystem>().Play(true);
        }
    }

    public void TurnLeft(bool chocolated) {
        testMeshCtr.AddForcce(new Vector3(0, 0.0375f, -0.0375f), 100);
        i = ((i - 1) + turnPoints.Length) % turnPoints.Length;
        currentTurnLeftPoint = turnPoints[i % turnPoints.Length];
        currentTurnRightPoint = turnPoints[((i + 1) + turnPoints.Length) % turnPoints.Length];

        //Çikolatalanan yüzeyi aktif et.
        if (chocolated) {
            chocolateSurfaces[i].SetActive(true);
            //chocolateSurfaces[i].transform.GetChild(0).GetComponent<ParticleSystem>().Play(true);
        }
    }

    public void MoveForward(bool chocolated) {
        testMeshCtr.AddForcce(new Vector3(-0.0375f, 0.0375f, -0.0375f), 100);
        //Çikolatalanan yüzeyi aktif et.
        if (chocolated) {
            chocolateSurfaces[i].SetActive(true);
            //chocolateSurfaces[i].transform.GetChild(0).GetComponent<ParticleSystem>().Play(true);
        }
    }

    public void MoveBack(bool chocolated) {
        testMeshCtr.AddForcce(new Vector3(0.0375f, 0.0375f, 0), 100);
        //Çikolatalanan yüzeyi aktif et.
        if (chocolated) {
            chocolateSurfaces[i].SetActive(true);
            //chocolateSurfaces[i].transform.GetChild(0).GetComponent<ParticleSystem>().Play(true);
        }
    }
}