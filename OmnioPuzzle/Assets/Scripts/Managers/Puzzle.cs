using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType {
    Empty,
    Road,
    Chocolate,
    Obstacle
}
public class Puzzle : MonoBehaviour {
    public BlockType blockType;
    public GameObject[] blockObjects;
    
    public void SetBlocks(int blockNumb) {
        switch (blockNumb) {
            case 0:
                blockType = BlockType.Empty;
                blockObjects[0].SetActive(true);
                break;
            case 1:
                blockType = BlockType.Road;
                blockObjects[1].SetActive(true);
                break;
            case 2:
                blockType = BlockType.Chocolate;
                blockObjects[2].SetActive(true);
                break;
            case 3:
                blockType = BlockType.Obstacle;
                blockObjects[3].SetActive(true);
                break;
        }
    }
}