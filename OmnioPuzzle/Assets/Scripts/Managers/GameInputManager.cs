using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInputManager : MonoBehaviour {

    FruitCurrentPos fruitCurrentPos;

    //Hareket denemeleri coroutinelerinde kullanılıyor.
    bool tryToMove;

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
        //Ön taraf hareket etmeye müsait değilse
        if (!CanMoveForward()) {
            return;
        }
        if (tryToMove) {
            return;
        }

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
        //Arka taraf hareket etmeye müsait değilse
        if (!CanMoveBack()) {
            return;
        }
        if (tryToMove) {
            return;
        }

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
        //Sol taraf hareket etmeye müsait değilse
        if (!CanMoveLeft()) {
            //O esnada hareket etmeye çalışmıyorsa
            if (!tryToMove) {
                StartCoroutine(TryTurnLeft());
            }
            return;
        }
        if (tryToMove) {
            return;
        }

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

    IEnumerator TryTurnLeft() {
        tryToMove = true;
        GameSceneManagers.Spawn.spawnedFruitWithFork.transform.RotateAround(GameSceneManagers.Spawn.spawnedFruitWithFork.currentTurnLeftPoint.position, Vector3.forward, 20);
        yield return new WaitForSeconds(0.5f);
        GameSceneManagers.Spawn.spawnedFruitWithFork.transform.RotateAround(GameSceneManagers.Spawn.spawnedFruitWithFork.currentTurnLeftPoint.position, Vector3.forward, -20);
        tryToMove = false;
        yield return null;
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
        if (!CanMoveRight()) {
            //Bu noktada küçük bir animasyon tetikle.
            if (!tryToMove) {
                StartCoroutine(TryTurnRight());
            }
            return;
        }
        if (tryToMove) {
            return;
        }
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

    IEnumerator TryTurnRight() {
        tryToMove = true;
        GameSceneManagers.Spawn.spawnedFruitWithFork.transform.RotateAround(GameSceneManagers.Spawn.spawnedFruitWithFork.currentTurnLeftPoint.position, Vector3.back, 20);
        yield return new WaitForSeconds(0.5f);
        GameSceneManagers.Spawn.spawnedFruitWithFork.transform.RotateAround(GameSceneManagers.Spawn.spawnedFruitWithFork.currentTurnLeftPoint.position, Vector3.back, -20);
        tryToMove = false;
        yield return null;
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

    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered

    private void TouchInput() {
        if (Input.touchCount == 1) // user is touching the screen with a single touch
         {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position;  //last touch position. Ommitted if you use list

                //Check if drag distance is greater than 20% of the screen height
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance) {//It's a drag
                                                                                                     //check if the drag is vertical or horizontal
                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y)) {   //If the horizontal movement is greater than the vertical movement...
                        if ((lp.x > fp.x))  //If the movement was to the right)
                        {   //Right swipe
                            MoveRight();
                        }
                        else {   //Left swipe
                            MoveLeft();
                        }
                    }
                    else {   //the vertical movement is greater than the horizontal movement
                        if (lp.y > fp.y)  //If the movement was up
                        {   //Up swipe
                            MoveForward();
                        }
                        else {   //Down swipe
                            MoveBack();
                        }
                    }
                }
                else {   //It's a tap as the drag distance is less than 20% of the screen height
                    Debug.Log("Tap");
                }
            }
        }
    }

    private void Update() {
        if (!GameSceneManagers.Game.isGameFinished) {
#if UNITY_EDITOR
            PCInput();
#elif UNITY_ANDROID
                TouchInput();
#endif
        }
    }

}