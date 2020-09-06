using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardWoodScript : MonoBehaviour {
    public GameObject spawnableChocolate;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "FruitChocolate") {
            GameObject spawnedGameObject = Instantiate(spawnableChocolate, new Vector3(0, 0, 0), Quaternion.identity, transform);
        }
    }
}