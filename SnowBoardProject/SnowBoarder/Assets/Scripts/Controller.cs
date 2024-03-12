using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    Rigidbody2D rb2d;
    float torque = -2f;
    float torque1 = 2f;
    float boostSpeed = 30f;
    float regularSpeed = 12f;
    SurfaceEffector2D surfaceEffector2D;
    bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
       rb2d = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindObjectOfType<SurfaceEffector2D>(); //accessing the surface effector on the level Object
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            RotationMove();
            Boost();
        }

    }

    public void DisableControls()
    {
       canMove = false;
    }

    private void Boost()
    {
        if (Input.GetKey(KeyCode.W))
        {
            surfaceEffector2D.speed = boostSpeed;
            Debug.Log(surfaceEffector2D.speed);
        }
        else
        {
            surfaceEffector2D.speed = regularSpeed;
        }

    }

    void RotationMove()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rb2d.AddTorque(torque);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb2d.AddTorque(torque1);
        }

    }


}
