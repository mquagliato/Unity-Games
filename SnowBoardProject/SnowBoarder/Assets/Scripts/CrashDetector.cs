using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] ParticleSystem snowB;
    bool haveHit = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground" && haveHit == true)
        {
            FindAnyObjectByType<Controller>().DisableControls(); //this line calls the script "Controller" and runs the PUBLIC method disableControls().
            GetComponent<AudioSource>().Play();
            haveHit = false;
            Invoke("LoadScene", 1f); // "0"  is the reference of the very first scene of the project
        }
    }


    // method was created in order to use the above on "Invoke" where Invoke resets the scene once some trigger is done
    void LoadScene()
    {
        SceneManager.LoadScene(0);
    }
}
