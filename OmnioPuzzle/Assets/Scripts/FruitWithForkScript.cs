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
        testMeshCtr.AddForcce(new Vector3(0, 0.0375f, 0.0375f), 20);
        i = (i + 1) % turnPoints.Length;
        currentTurnLeftPoint = turnPoints[i % turnPoints.Length];
        currentTurnRightPoint = turnPoints[(i + 1) % turnPoints.Length];

        //Çikolatalanan yüzeyi aktif et.
        if (chocolated) {
            if (!chocolateSurfaces[i].activeSelf) {
                chocolateSurfaces[i].SetActive(true);
                GameSceneManagers.Game.chocolatedSurface++;
            }
            //chocolateSurfaces[i].transform.GetChild(0).GetComponent<ParticleSystem>().Play(true);
        }
    }

    public void TurnLeft(bool chocolated) {
        testMeshCtr.AddForcce(new Vector3(0, 0.0375f, -0.0375f), 20);
        i = ((i - 1) + turnPoints.Length) % turnPoints.Length;
        currentTurnLeftPoint = turnPoints[i % turnPoints.Length];
        currentTurnRightPoint = turnPoints[((i + 1) + turnPoints.Length) % turnPoints.Length];

        //Çikolatalanan yüzeyi aktif et.
        if (chocolated) {
            if (!chocolateSurfaces[i].activeSelf) {
                chocolateSurfaces[i].SetActive(true);
                GameSceneManagers.Game.chocolatedSurface++;
            }
            //chocolateSurfaces[i].transform.GetChild(0).GetComponent<ParticleSystem>().Play(true);
        }
    }

    public void MoveForward(bool chocolated) {
        testMeshCtr.AddForcce(new Vector3(-0.0375f, 0.0375f, -0.0375f), 20);
        //Çikolatalanan yüzeyi aktif et.
        if (chocolated) {
            if (!chocolateSurfaces[i].activeSelf) {
                chocolateSurfaces[i].SetActive(true);
                GameSceneManagers.Game.chocolatedSurface++;
            }
            //chocolateSurfaces[i].transform.GetChild(0).GetComponent<ParticleSystem>().Play(true);
        }
    }

    public void MoveBack(bool chocolated) {
        testMeshCtr.AddForcce(new Vector3(0.0375f, 0.0375f, 0), 20);
        //Çikolatalanan yüzeyi aktif et.
        if (chocolated) {
            if (!chocolateSurfaces[i].activeSelf) {
                chocolateSurfaces[i].SetActive(true);
                GameSceneManagers.Game.chocolatedSurface++;
            }
            //chocolateSurfaces[i].transform.GetChild(0).GetComponent<ParticleSystem>().Play(true);
        }
    }

    float counter;
    private void Update() {
        if (GameSceneManagers.Game.chocolatedSurface == 4) {
            GameSceneManagers.Game.isGameFinished = true;
            Debug.Log("Oyunu kazanın!");
            counter += Time.deltaTime;
            if (counter < 0.5f) {
                GameObject temp = transform.GetChild(0).gameObject;
                temp.transform.position = new Vector3(temp.transform.position.x, temp.transform.position.y + Time.deltaTime / 5, temp.transform.position.z);
            }
            else if (counter < 1) {
                GameObject temp = transform.GetChild(0).gameObject;
                temp.transform.Rotate(Time.deltaTime * 1000, 0, 0);
            }
            else if (counter < 1.5f) {
                transform.Translate(Time.deltaTime, 0, Time.deltaTime);
            }
            else {
                //Cameranın bitiş animasyonunu tetikle.
                Camera.main.GetComponent<Animator>().SetTrigger("CameraFinishAnim");
                Destroy(gameObject);
            }
        }
    }
}