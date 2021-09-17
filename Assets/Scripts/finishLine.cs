using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class finishLine : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter()
    {
        int sceneCount = SceneManager.sceneCount;
        int index = SceneManager.GetActiveScene().buildIndex;
        if(index < sceneCount){
            SceneManager.LoadScene(index + 1);
        }
        else{
            SceneManager.LoadScene(0);
        }
    }
}
