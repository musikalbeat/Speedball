using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class playerScript : MonoBehaviour
{
    public GameObject rightPosition, leftPosition, deadPrefab, pauseMenu, getReadyMenu, levelText, countDownText;
    bool changePosition, startGame, isPause;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        isPause = false;
        pauseMenu.SetActive(false);
        getReadyMenu.SetActive(true);
        levelText.GetComponent<TextMeshProUGUI>().text = "Level "+SceneManager.GetActiveScene().buildIndex.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(startGame && !isPause){
            // Push player forward
            GetComponent<Rigidbody>().AddForce(Vector3.forward * speed * Time.deltaTime);
            // Switch player to right
            if(changePosition == true && startGame == true){
                transform.position = Vector3.Lerp(transform.position, new Vector3(rightPosition.transform.position.x, transform.position.y, transform.position.z), 10f * Time.deltaTime);
            }
            // Switch player to left
            if(changePosition == false && startGame == true){
                transform.position = Vector3.Lerp(transform.position, new Vector3(leftPosition.transform.position.x, transform.position.y, transform.position.z), 10f * Time.deltaTime);
            }
        }

        // Mouseclick to switch sides
        if(Input.GetMouseButtonDown(0)){ 
            if(startGame && !isPause){
                if(changePosition == false){
                    changePosition = true;
                } else if(changePosition == true){
                    changePosition = false;
                }
            }
            else if(!startGame || !isPause){
                StartCoroutine(countDown(3));
            }
        }

        // If Pause keybind pressed
        if(Input.GetKeyDown(KeyCode.Escape) && startGame){
            if(!isPause){
                isPause = true;
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
            }
            else{
                isPause = false;
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
            }

        }
    }
    
    void OnTriggerEnter(Collider other){
        // Death on Wall Collision
        if(other.tag == "wall"){
            for(int i = 0; i < 17; i++){
                Instantiate(deadPrefab, transform.position, Quaternion.identity);
            }
            StartCoroutine(wait(3));
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        // Continue to Next Level
        if(other.tag == "finish"){
            Debug.Log("Finish");
        }
    }

    IEnumerator countDown(int seconds){
        int count = seconds;

        while(count > 0){
            countDownText.GetComponent<TextMeshProUGUI>().text = count.ToString();
            yield return new WaitForSeconds(1);
            count--;
        }
        countDownText.GetComponent<TextMeshProUGUI>().text = "Start!";
        yield return new WaitForSeconds(1);
        getReadyMenu.SetActive(false);
        startGame = true;
    }

    IEnumerator wait(int seconds){
        int count = seconds;

        while(count > 0){
            yield return new WaitForSeconds(1);
            count--;
        }
    }

    public void MainMenu(){
        SceneManager.LoadScene(0);
    }

    public void ReturnToGame(){
        isPause = false;
    }

    public void ExitGame(){
        Application.Quit();
    }
}
