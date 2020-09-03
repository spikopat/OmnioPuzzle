﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelEnum {
    Level1,
    Level2
}
public class GameSceneSpawnManager : MonoBehaviour {

    public LevelEnum levelEnum;

    int[,] level1 = { 
        { 2, 1, 1, 2 }, 
        { 1, 1, 2, 0 }, 
        { 1, 2, 1, 1 }
    };
    int[,] level2 = {
        { 1, 2, 1, 1, 2},
        { 1, 1, 0, 2, 3},
        { 2, 1, 2, 1, 1}
    };

    #region FROM_INSPECTOR
    public GameObject spawnPoint;
    public GameObject boardWood;
    public GameObject fruitWithFork;

    [Header("Board")]
    public BoardScript boardScript;

    public float offsetValue;
    #endregion

    Puzzle[,] grid;
    int z, x;
    float boardWoodSize;
    float fruitWithForkSize;

    //Seçilmiş olan levelin değerlerini tutar.
    int[,] selectedLevel;
    public Transform lastSpawnedObject;
    public FruitWithForkScript spawnedFruitWithFork;

    void SpawnLevel() {
        GameObject spawnedObject;
        for (int i = 0; i < z; i++) {
            for (int j = 0; j < x; j++) {
                spawnedObject = Instantiate(spawnPoint, new Vector3(
                        boardScript.topLeftPoint.x + ((boardWoodSize * (j+1)) + (offsetValue * j)),
                        boardScript.topLeftPoint.y, 
                        boardScript.topLeftPoint.z - ((boardWoodSize * (i+1)) + (offsetValue * i))),
                        Quaternion.identity
                    );
                spawnedObject.name = "[" + i + "][" + j + "]";
                spawnedObject.transform.SetParent(boardScript.cubeTransform);
                Puzzle spawnedPuzzle = spawnedObject.GetComponent<Puzzle>();
                spawnedPuzzle.SetBlocks(selectedLevel[i, j]);
                //grid[i,j] = spawnedPuzzle;
                lastSpawnedObject = spawnedObject.transform;
            }
        }
        GameObject spawnedForkWithObj = Instantiate(fruitWithFork, new Vector3(lastSpawnedObject.position.x, lastSpawnedObject.position.y + (boardWoodSize / 4), lastSpawnedObject.position.z), Quaternion.Euler(0, 90, 0));
        spawnedFruitWithFork = spawnedForkWithObj.GetComponent<FruitWithForkScript>();
        lastSpawnedObject.GetComponent<Puzzle>().isPuzzleOnGround = true;
    }

    #region UNITY_FUNCTIONS
    private void Start() {
        if (levelEnum == LevelEnum.Level1) {
            z = level1.GetLength(0);
            x = level1.GetLength(1);
            selectedLevel = level1;
        }
        else if (levelEnum == LevelEnum.Level2) {
            selectedLevel = level2;
            z = level2.GetLength(0);
            x = level2.GetLength(1);
        }
        grid = new Puzzle[z, x];
        GetBounds();
        SpawnLevel();
    }
    #endregion

    void GetBounds() {
        boardWoodSize = boardWood.transform.GetChild(0).GetComponent<MeshRenderer>().bounds.size.x;
        fruitWithForkSize = fruitWithFork.transform.GetChild(0).GetComponent<MeshRenderer>().bounds.size.y;
    }
}