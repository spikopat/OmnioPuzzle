using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardWoodScript : MonoBehaviour {
    public GameObject spawnableChocolate;

    //Çikolata yüzeyine bulanmış bir yüzey, tahtaya dokunduğunda dokunduğu noktaya animasyonlu çikolata spawnla.
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "FruitChocolate") {
            GameObject spawnedGameObject = Instantiate(spawnableChocolate, new Vector3(0, 0, 0), Quaternion.identity, transform);
        }
    }
}