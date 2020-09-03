using System.Collections.Generic;
using UnityEngine;

public enum InputMethod {
    PCInput,
    TouchInput
}
public class GameInputManager : MonoBehaviour {

    public InputMethod inputType;

    void MoveForward() {
        Vector3 currentPos = GameSceneManagers.Spawn.spawnedFruitWithFork.transform.position;
        GameSceneManagers.Spawn.spawnedFruitWithFork.transform.position = new Vector3(currentPos.x, currentPos.y, currentPos.z + GameSceneManagers.Spawn.fruitWithForkSize + GameSceneManagers.Spawn.offsetValue);
    }

    void MoveBack() {
        Vector3 currentPos = GameSceneManagers.Spawn.spawnedFruitWithFork.transform.position;
        GameSceneManagers.Spawn.spawnedFruitWithFork.transform.position = new Vector3(currentPos.x, currentPos.y, currentPos.z - (GameSceneManagers.Spawn.fruitWithForkSize + GameSceneManagers.Spawn.offsetValue));
    }

    void MoveLeft() {
        GameSceneManagers.Spawn.spawnedFruitWithFork.transform.RotateAround(GameSceneManagers.Spawn.spawnedFruitWithFork.currentTurnLeftPoint.position, Vector3.forward, 90);
        GameSceneManagers.Spawn.spawnedFruitWithFork.TurnLeft();
    }

    void MoveRight() {
        GameSceneManagers.Spawn.spawnedFruitWithFork.transform.RotateAround(GameSceneManagers.Spawn.spawnedFruitWithFork.currentTurnRightPoint.position, Vector3.back, 90);
        GameSceneManagers.Spawn.spawnedFruitWithFork.TurnRight();
    }

    private void PCInput() {
        if (Input.GetKeyDown(KeyCode.W)) {
            MoveForward();
        }
        else if (Input.GetKeyDown(KeyCode.S)) {
            MoveBack();
        }
        else if (Input.GetKeyDown(KeyCode.A)) {
            MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.D)) {
            MoveRight();
        }
    }

    private void TouchInput() {
       
    }

    private void Update() {
        #if UNITY_EDITOR
            PCInput();
        #elif UNITY_ANDROID
            TouchInput();
        #endif
    }

}