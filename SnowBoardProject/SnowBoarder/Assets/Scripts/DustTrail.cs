using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DustTrail : MonoBehaviour
{
    [SerializeField]ParticleSystem dustTrail; //serializing this fiel in order to add the particle objects later on

    private void OnCollisionEnter2D(Collision2D collision) // collision is the variable of the type collision2d tat will receive the object colliding based on the object's TAG
    {
        if (collision.gameObject.tag == "Ground") 
        {
            dustTrail.Play();      
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            dustTrail.Stop();
        }
    }
}
