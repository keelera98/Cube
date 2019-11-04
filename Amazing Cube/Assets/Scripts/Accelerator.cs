using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerator : MonoBehaviour
{
    public bool freeze;
    private Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 accelInput = Input.acceleration;

        accelInput = Quaternion.Euler(90, 0, 0) * accelInput;

        float speed = gameObject.GetComponent<PlayerController>().speed;

        if (!freeze)
        {
            Vector3 force = new Vector3(accelInput.x, 0, accelInput.y);
            //flat-on desk
            //transform.Translate(accelInput.x, 0, accelInput.z);
            //held in hand
            //transform.Translate(accelInput.x, 0, accelInput.y);
            rigidbody.AddForce(force * speed);
        }
        //Debug.DrawRay(transform.position + Vector3.up, accelInput, Color.cyan);
        //Debug.Log(accelInput);

    }
}