using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] ParticleSystem _particleSystem;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            _particleSystem.Play();
            GetComponent<AudioSource>().Play();//playesx the audio attached to the finish line object
            Invoke("LoadScene", 1f); // "0"  is the reference of the very first scene of the project
        }
    }

    // method was created in order to use the above on "Invoke" where Invoke resets the scene once some trigger is done
    void LoadScene()
    {
        SceneManager.LoadScene(0);
    }
}
