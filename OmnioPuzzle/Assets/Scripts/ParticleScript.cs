using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour {
    // Start is called before the first frame update
    void Update() {
        if (transform.position.y >= 0.0276f) {
            transform.position = new Vector3(transform.position.x, transform.position.y - (Time.deltaTime / 20), transform.position.z);
        }
    }

}