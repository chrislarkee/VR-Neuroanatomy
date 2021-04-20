using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainMovement : MonoBehaviour
{
    private float speed = 100f;
    public Vector3 limits;
    public GameObject lookTarget;

    private Vector3 newPosition;

    // Start is called before the first frame update
    void Start()
    {
        newPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        {
            newPosition = transform.position;
            // makes objects move left/right with letter keys

            if (Input.GetKey(KeyCode.D))
            {
                newPosition += (Vector3.left * speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.A))
            {
                newPosition += (Vector3.left * speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.S))
            {
                newPosition += (Vector3.left * speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.W))
            {
                newPosition += (Vector3.left * speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.J))
            {
                newPosition += (Vector3.forward * speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.K))
            {
                newPosition += (Vector3.back * speed * Time.deltaTime);
            }

            if (newPosition == transform.position) return;

            if (Mathf.Abs(newPosition.x) <= limits.x &&
                Mathf.Abs(newPosition.y) <= limits.y &&
                Mathf.Abs(newPosition.z) <= limits.z)
            {
                transform.position = newPosition;
            }

            transform.LookAt(lookTarget.transform.position);
        }
    }
}
