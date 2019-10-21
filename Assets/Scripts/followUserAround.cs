using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followUserAround : MonoBehaviour
{

    public Transform target;
    private Vector3 newPos;

    // Update is called once per frame
    void Update() {
        newPos = Vector3.Lerp(transform.position, target.position, Time.deltaTime * 6f);
        transform.position = newPos;        
    }
}
