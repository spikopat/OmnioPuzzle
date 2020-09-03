using System.Collections.Generic;
using UnityEngine;

public enum InputMethod {
    PCInput,
    TouchInput
}
public class GameInputManager : MonoBehaviour {

    FruitCurrentPos fruitCurrentPos;

    public InputMethod inputType;

    void MoveForward() {
        fruitCurrentPos = GameSceneManagers.Spawn.GetFruitCurrentPos();

        //Önü yoksa return
        if (fruitCurrentPos.x == 0)
            return;
        
        //Önü boşsa return
        if (GameSceneManagers.Spawn.grid[fruitCurrentPos.x - 1, fruitCurrentPos.y].blockType == BlockType.Empty)
            return;
        
        //Önü engelse return
        if (GameSceneManagers.Spawn.grid[fruitCurrentPos.x - 1, fruitCurrentPos.y].blockType == BlockType.Obstacle)
            return;
            
        //Hareket işlemleri
        Vector3 currentPos = GameSceneManagers.Spawn.spawnedFruitWithFork.transform.position;
        GameSceneManagers.Spawn.spawnedFruitWithFork.transform.position = new Vector3(currentPos.x, currentPos.y, currentPos.z + GameSceneManagers.Spawn.fruitWithForkSize + GameSceneManagers.Spawn.offsetValue);

        //Pozisyonun değiştiğini kaydet.
        GameSceneManagers.Spawn.grid[fruitCurrentPos.x, fruitCurrentPos.y].isPuzzleOnGround = false;
        GameSceneManagers.Spawn.grid[fruitCurrentPos.x - 1, fruitCurrentPos.y].isPuzzleOnGround = true;
    }

    void MoveBack() {
        fruitCurrentPos = GameSceneManagers.Spawn.GetFruitCurrentPos();
       
        //Arkası yoksa return
        if (fruitCurrentPos.x == GameSceneManagers.Spawn.grid.GetLength(0) - 1)
            return;

        //Arkası boşsa return
        if (GameSceneManagers.Spawn.grid[fruitCurrentPos.x + 1, fruitCurrentPos.y].blockType == BlockType.Empty)
            return;
       
        //Arkası engelse return
        if (GameSceneManagers.Spawn.grid[fruitCurrentPos.x + 1, fruitCurrentPos.y].blockType == BlockType.Obstacle)
            return;

        //Hareket işlemleri
        Vector3 currentPos = GameSceneManagers.Spawn.spawnedFruitWithFork.transform.position;
        GameSceneManagers.Spawn.spawnedFruitWithFork.transform.position = new Vector3(currentPos.x, currentPos.y, currentPos.z - (GameSceneManagers.Spawn.fruitWithForkSize + GameSceneManagers.Spawn.offsetValue));
        
        //Pozisyonun değiştiğini kaydet.
        GameSceneManagers.Spawn.grid[fruitCurrentPos.x, fruitCurrentPos.y].isPuzzleOnGround = false;
        GameSceneManagers.Spawn.grid[fruitCurrentPos.x + 1, fruitCurrentPos.y].isPuzzleOnGround = true;
    }

    void MoveLeft() {
        fruitCurrentPos = GameSceneManagers.Spawn.GetFruitCurrentPos();

        //Sol tarafı yoksa return
        if (fruitCurrentPos.y == 0)
            return;
        //Sol tarafı yoksa return
        if (GameSceneManagers.Spawn.grid[fruitCurrentPos.x, fruitCurrentPos.y - 1].blockType == BlockType.Empty)
            return;
        //Sol tarafı engelse return
        if (GameSceneManagers.Spawn.grid[fruitCurrentPos.x, fruitCurrentPos.y - 1].blockType == BlockType.Obstacle)
            return;

        //1 kare soldaki kare, o karenin altındakileri kontrol et. Engel varsa return
        if (fruitCurrentPos.x + 1 != GameSceneManagers.Spawn.grid.GetLength(0)) {
            if (GameSceneManagers.Spawn.grid[fruitCurrentPos.x + 1, fruitCurrentPos.y - 1].blockType == BlockType.Obstacle)
                return;
        }

        //Çevirme işlemleri.
        GameSceneManagers.Spawn.spawnedFruitWithFork.transform.RotateAround(GameSceneManagers.Spawn.spawnedFruitWithFork.currentTurnLeftPoint.position, Vector3.forward, 90);
        GameSceneManagers.Spawn.spawnedFruitWithFork.TurnLeft();
        
        //Pozisyonun değiştiğini kaydet.
        GameSceneManagers.Spawn.grid[fruitCurrentPos.x, fruitCurrentPos.y].isPuzzleOnGround = false;
        GameSceneManagers.Spawn.grid[fruitCurrentPos.x, fruitCurrentPos.y - 1].isPuzzleOnGround = true;
    }

    void MoveRight() {
        fruitCurrentPos = GameSceneManagers.Spawn.GetFruitCurrentPos();

        //Sağ tarafı yoksa return
        if (fruitCurrentPos.y == GameSceneManagers.Spawn.grid.GetLength(1) - 1)
            return;
        //Sağ tarafı boşsa return
        if (GameSceneManagers.Spawn.grid[fruitCurrentPos.x, fruitCurrentPos.y + 1].blockType == BlockType.Empty)
            return;
        //Sağ tarafı engelse return
        if (GameSceneManagers.Spawn.grid[fruitCurrentPos.x, fruitCurrentPos.y + 1].blockType == BlockType.Obstacle)
            return;

        //1 kare sağdaki kare, o karenin altındakileri kontrol et. Engel varsa return
        if (fruitCurrentPos.x+1 != GameSceneManagers.Spawn.grid.GetLength(0)) {
            if (GameSceneManagers.Spawn.grid[fruitCurrentPos.x + 1, fruitCurrentPos.y + 1].blockType == BlockType.Obstacle)
                return;
        }
        
        for (int i = 0; i < GameSceneManagers.Spawn.grid.GetLength(0); i++) {
            
        }
        //Çevirme işlemleri.
        GameSceneManagers.Spawn.spawnedFruitWithFork.transform.RotateAround(GameSceneManagers.Spawn.spawnedFruitWithFork.currentTurnRightPoint.position, Vector3.back, 90);
        GameSceneManagers.Spawn.spawnedFruitWithFork.TurnRight();
        
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