﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputMethod {
    PCInput,
    TouchInput
}
public class GameInputManager : MonoBehaviour {

    FruitCurrentPos fruitCurrentPos;

    public InputMethod inputType;

    bool CanMoveForward() {
        fruitCurrentPos = GameSceneManagers.Spawn.GetFruitCurrentPos();
        //Önü yoksa return
        if (fruitCurrentPos.x == 0)
            return false;
        //Önü boşsa return
        if (GameSceneManagers.Spawn.grid[fruitCurrentPos.x - 1, fruitCurrentPos.y].blockType == BlockType.Empty)
            return false;
        //Önü engelse return
        if (GameSceneManagers.Spawn.grid[fruitCurrentPos.x - 1, fruitCurrentPos.y].blockType == BlockType.Obstacle)
            return false;
        return true;
    }

    void MoveForward() {
        if (!CanMoveForward())
            return;

        //Arkada çikolata varsa
        bool chocolated = false;
        //Arkası boşsa return
        if (GameSceneManagers.Spawn.grid[fruitCurrentPos.x - 1, fruitCurrentPos.y].blockType == BlockType.Chocolate)
            chocolated = true;

        //Hareket işlemleri
        Vector3 currentPos = GameSceneManagers.Spawn.spawnedFruitWithFork.transform.position;
        GameSceneManagers.Spawn.spawnedFruitWithFork.transform.position = new Vector3(currentPos.x, currentPos.y, currentPos.z + GameSceneManagers.Spawn.fruitWithForkSize + GameSceneManagers.Spawn.offsetValue);
        GameSceneManagers.Spawn.spawnedFruitWithFork.MoveForward(chocolated);

        //Pozisyonun değiştiğini kaydet.
        GameSceneManagers.Spawn.grid[fruitCurrentPos.x, fruitCurrentPos.y].isPuzzleOnGround = false;
        GameSceneManagers.Spawn.grid[fruitCurrentPos.x - 1, fruitCurrentPos.y].isPuzzleOnGround = true;
    }

    bool CanMoveBack() {
        fruitCurrentPos = GameSceneManagers.Spawn.GetFruitCurrentPos();

        //Arkası yoksa return
        if (fruitCurrentPos.x == GameSceneManagers.Spawn.grid.GetLength(0) - 1)
            return false;

        //Arkası boşsa return
        if (GameSceneManagers.Spawn.grid[fruitCurrentPos.x + 1, fruitCurrentPos.y].blockType == BlockType.Empty)
            return false;

        //Arkası engelse return
        if (GameSceneManagers.Spawn.grid[fruitCurrentPos.x + 1, fruitCurrentPos.y].blockType == BlockType.Obstacle)
            return false;
        return true;
    }

    void MoveBack() {
        if (!CanMoveBack())
            return;

        //Arkada çikolata varsa
        bool chocolated = false;
        //Arkası boşsa return
        if (GameSceneManagers.Spawn.grid[fruitCurrentPos.x + 1, fruitCurrentPos.y].blockType == BlockType.Chocolate)
            chocolated = true;

        //Hareket işlemleri
        Vector3 currentPos = GameSceneManagers.Spawn.spawnedFruitWithFork.transform.position;
        GameSceneManagers.Spawn.spawnedFruitWithFork.transform.position = new Vector3(currentPos.x, currentPos.y, currentPos.z - (GameSceneManagers.Spawn.fruitWithForkSize + GameSceneManagers.Spawn.offsetValue));
        GameSceneManagers.Spawn.spawnedFruitWithFork.MoveBack(chocolated);

        //Pozisyonun değiştiğini kaydet.
        GameSceneManagers.Spawn.grid[fruitCurrentPos.x, fruitCurrentPos.y].isPuzzleOnGround = false;
        GameSceneManagers.Spawn.grid[fruitCurrentPos.x + 1, fruitCurrentPos.y].isPuzzleOnGround = true;
    }

    bool CanMoveLeft() {
        fruitCurrentPos = GameSceneManagers.Spawn.GetFruitCurrentPos();

        //Sol tarafı yoksa return
        if (fruitCurrentPos.y == 0)
            return false;
        //Sol tarafı yoksa return
        if (GameSceneManagers.Spawn.grid[fruitCurrentPos.x, fruitCurrentPos.y - 1].blockType == BlockType.Empty)
            return false;
        //Sol tarafı engelse return
        if (GameSceneManagers.Spawn.grid[fruitCurrentPos.x, fruitCurrentPos.y - 1].blockType == BlockType.Obstacle)
            return false;

        //1 kare soldaki kare, o karenin altındakileri kontrol et. Engel varsa return
        if (fruitCurrentPos.x + 1 != GameSceneManagers.Spawn.grid.GetLength(0)) {
            if (GameSceneManagers.Spawn.grid[fruitCurrentPos.x + 1, fruitCurrentPos.y - 1].blockType == BlockType.Obstacle)
                return false;
        }

        return true;
    }

    void MoveLeft() {
        if (!CanMoveLeft())
            return;

        //Solda çikolata varsa
        bool chocolated = false;
        if (GameSceneManagers.Spawn.grid[fruitCurrentPos.x, fruitCurrentPos.y - 1].blockType == BlockType.Chocolate) {
            chocolated = true;
        }

        //Çevirme işlemleri.
        GameSceneManagers.Spawn.spawnedFruitWithFork.transform.RotateAround(GameSceneManagers.Spawn.spawnedFruitWithFork.currentTurnLeftPoint.position, Vector3.forward, 90);
        GameSceneManagers.Spawn.spawnedFruitWithFork.TurnLeft(chocolated);

        //Pozisyonun değiştiğini kaydet.
        GameSceneManagers.Spawn.grid[fruitCurrentPos.x, fruitCurrentPos.y].isPuzzleOnGround = false;
        GameSceneManagers.Spawn.grid[fruitCurrentPos.x, fruitCurrentPos.y - 1].isPuzzleOnGround = true;
    }

    bool CanMoveRight() {
        fruitCurrentPos = GameSceneManagers.Spawn.GetFruitCurrentPos();

        //Sağ tarafı yoksa return
        if (fruitCurrentPos.y == GameSceneManagers.Spawn.grid.GetLength(1) - 1)
            return false;
        //Sağ tarafı boşsa return
        if (GameSceneManagers.Spawn.grid[fruitCurrentPos.x, fruitCurrentPos.y + 1].blockType == BlockType.Empty)
            return false;
        //Sağ tarafı engelse return
        if (GameSceneManagers.Spawn.grid[fruitCurrentPos.x, fruitCurrentPos.y + 1].blockType == BlockType.Obstacle)
            return false;

        //1 kare sağdaki kare, o karenin altındakileri kontrol et. Engel varsa return
        if (fruitCurrentPos.x + 1 != GameSceneManagers.Spawn.grid.GetLength(0)) {
            if (GameSceneManagers.Spawn.grid[fruitCurrentPos.x + 1, fruitCurrentPos.y + 1].blockType == BlockType.Obstacle)
                return false;
        }
        return true;
    }

    void MoveRight() {
        if (!CanMoveRight())
            return;

        //Sağda çikolata varsa
        bool chocolated = false;
        if (GameSceneManagers.Spawn.grid[fruitCurrentPos.x, fruitCurrentPos.y + 1].blockType == BlockType.Chocolate)
            chocolated = true;

        //Çevirme işlemleri.
        GameSceneManagers.Spawn.spawnedFruitWithFork.transform.RotateAround(GameSceneManagers.Spawn.spawnedFruitWithFork.currentTurnRightPoint.position, Vector3.back, 90);
        GameSceneManagers.Spawn.spawnedFruitWithFork.TurnRight(chocolated);
        
        //Pozisyonun değiştiğini kaydet.
        GameSceneManagers.Spawn.grid[fruitCurrentPos.x, fruitCurrentPos.y].isPuzzleOnGround = false;
        GameSceneManagers.Spawn.grid[fruitCurrentPos.x, fruitCurrentPos.y + 1].isPuzzleOnGround = true;
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