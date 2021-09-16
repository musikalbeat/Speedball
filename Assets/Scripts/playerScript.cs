using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerScript : MonoBehaviour
{
    public GameObject rightPosition, leftPosition, deadPrefab, pauseMenu, getReadyMenu;
    bool changePosition, startGame, isPause;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        isPause = false;
        pauseMenu.SetActive(false);
        getReadyMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(startGame){
            // Push player forward
            GetComponent<Rigidbody>().AddForce(Vector3.forward * speed * Time.deltaTime);
        }

        // Switch player to right
        if(changePosition == true && startGame == true){
            transform.position = Vector3.Lerp(transform.position, new Vector3(rightPosition.transform.position.x, transform.position.y, transform.position.z), 10f * Time.deltaTime);
        }
        // Switch player to left
        if(changePosition == false && startGame == true){
            transform.position = Vector3.Lerp(transform.position, new Vector3(leftPosition.transform.position.x, transform.position.y, transform.position.z), 10f * Time.deltaTime);
        }
        // Mouseclick to switch sides
        if(Input.GetMouseButtonDown(0)){

            startGame = true;

            if(changePosition == false){
                changePosition = true;
            } else if(changePosition == true){
                changePosition = false;
            }
        }
        
    }
    
    void OnTriggerEnter(Collider other){
        // Death on Wall Collision
        if(other.tag == "wall"){
            transform.gameObject.SetActive(false);
            for(int i = 0; i < 17; i++){
                Instantiate(deadPrefab, transform.position, Quaternion.identity);
            }
        }
        // Continue to Next Level
        if(other.tag == "finish"){
            Debug.Log("Finish");
        }
    }
}
