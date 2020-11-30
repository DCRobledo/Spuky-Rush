using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
	public GameObject background;
    public GameObject UI_sound;
    public GameObject crossfade;
    public GameObject score;

	public Sprite retrySelected;
	public Sprite menuSelected;
	public Sprite exitSelected;

	private int cursorPointer = 0;

	private string sceneSelected = "Level";

	private float inputTimeLimit = 0.1f;
    private float inputTimer = 0f;

    void Start(){
        GameObject scoreText = GameObject.Find("ScoreTransition");
        score.transform.GetComponent<Text>(). text = scoreText.transform.GetComponent<ScoreTransition>(). score.ToString();
    }

    void Update()
    {
    	UpdateSprite();
        UpdateGameOver();
        UpdateSceneSelected();
    }

    void UpdateGameOver(){
    	if(inputTimer>=inputTimeLimit){

    		UpdateCursor();
    		if(Input.GetKey(KeyCode.Return)){
                CheckEnter();
    		}

	    	inputTimer = 0f;
    	}
    	else 
            inputTimer += Time.deltaTime;
    }

    void CheckEnter(){
        Instantiate(UI_sound, transform.position, Quaternion.identity);
        if(sceneSelected.Equals("Exit"))
            LoadNextLevel(-1);
        if(sceneSelected.Equals("Level"))
            LoadNextLevel(1);
        else
            LoadNextLevel(0);
    }

    void UpdateCursor(){
    	if(cursorPointer > 0 && Input.GetKey(KeyCode.UpArrow)){
            Instantiate(UI_sound, transform.position, Quaternion.identity);
	    	cursorPointer--;
        }
	    if(cursorPointer < 2 && Input.GetKey(KeyCode.DownArrow)){
            Instantiate(UI_sound, transform.position, Quaternion.identity);
	    	cursorPointer++;
        }
    }

    void UpdateSceneSelected(){
    	switch(cursorPointer){
    		case 0:
    			sceneSelected = "Level";
    		break;

    		case 1:
    			sceneSelected = "Menu";
    		break;

    		default:
    			sceneSelected = "Exit";
    		break;
    	}
    }

    void LoadNextLevel(int levelIndex){
        StartCoroutine(LoadLevel(levelIndex));
    }

    IEnumerator LoadLevel(int levelIndex){
        crossfade.transform.GetComponent<Animator>().SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        if(levelIndex==-1)
            Application.Quit();

        SceneManager.LoadScene(levelIndex);
    }

    void UpdateSprite(){
    	if(sceneSelected.Equals("Level"))
    		background.transform.GetComponent<Image>().sprite = retrySelected;

    	else if(sceneSelected.Equals("Menu"))
    		background.transform.GetComponent<Image>().sprite = menuSelected;

    	else if(sceneSelected.Equals("Exit"))
    		background.transform.GetComponent<Image>().sprite = exitSelected;
    }		
}