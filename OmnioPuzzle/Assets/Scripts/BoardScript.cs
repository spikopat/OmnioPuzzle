using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardScript : MonoBehaviour {

    MeshRenderer cubeMeshRenderer;
    public Transform cubeTransform;

    public Vector3 cubeMeshSize;

    public Vector3 topLeftPoint;

    private void Awake() {
        cubeMeshRenderer = cubeTransform.GetComponent<MeshRenderer>();
        cubeMeshSize = cubeMeshRenderer.bounds.size;
        topLeftPoint = new Vector3(cubeTransform.position.x - (cubeMeshSize.x / 2), cubeTransform.position.y, cubeTransform.position.z + (cubeMeshSize.z / 2));
        Debug.Log(topLeftPoint);
    }
}