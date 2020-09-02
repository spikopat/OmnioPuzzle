using System.Collections.Generic;
using UnityEngine;

public enum InputMethod {
    PCInput,
    TouchInput
}
public class GameInputManager : MonoBehaviour {

    public InputMethod inputType;

    void MoveForward() {
        Debug.Log("Forward");
    }

    void MoveBack() {
        Debug.Log("Back");
    }

    void MoveLeft() {
        Debug.Log("Left");
    }

    void MoveRight() {
        Debug.Log("Right");
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