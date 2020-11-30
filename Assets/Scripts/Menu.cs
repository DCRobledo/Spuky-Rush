using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Menu : MonoBehaviour
{
	public GameObject background;
	public GameObject tutorial;
	public GameObject credits;
    public GameObject UI_sound;
    public GameObject crossfade;

	public Sprite startSelected;
	public Sprite tutorialSelected;
	public Sprite optionsSelected;
	public Sprite exitSelected;
	public Sprite creditsSelected;

	private int cursorPointer = 0;

	private string sceneSelected = "Level";

	private float inputTimeLimit = 0.1f;
    private float inputTimer = 0f;

    private bool tutorialOpened = false;
    private bool creditsOpened = false;

    void Start(){
        Screen.SetResolution(1600, 900, false);
    }

    void Update()
    {
    	UpdateSprite();
        UpdateMenu();
        UpdateSceneSelected();
    }

    void UpdateMenu(){
    	if(inputTimer>=inputTimeLimit){

    		UpdateCursor();

	    	if(Input.GetKey(KeyCode.Return) && !tutorialOpened && !creditsOpened)
                CheckEnter();

	    	if(Input.GetKey(KeyCode.Escape) && (tutorialOpened || creditsOpened))
                CheckEscape();

	    	inputTimer = 0f;
    	}
    	else 
            inputTimer += Time.deltaTime;
    }

    void CheckEnter(){
        Instantiate(UI_sound, transform.position, Quaternion.identity);
        if(sceneSelected.Equals("Exit"))
            Application.Quit();

        if(sceneSelected.Equals("Tutorial"))
            SwitchEmergentWindow(tutorial, tutorialOpened);

        if(sceneSelected.Equals("Credits"))
            SwitchEmergentWindow(credits, creditsOpened);

        if(sceneSelected.Equals("Level"))
            LoadNextLevel();  
    }

    void CheckEscape(){
        Instantiate(UI_sound, transform.position, Quaternion.identity);
        if(tutorialOpened)
            SwitchEmergentWindow(tutorial, tutorialOpened);
        if(creditsOpened)
            SwitchEmergentWindow(credits, creditsOpened); 
    }

    void LoadNextLevel(){
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene(). buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex){
        crossfade.transform.GetComponent<Animator>().SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(levelIndex);
    }

    void SwitchEmergentWindow(GameObject option, bool controler){
    	controler = !controler;
    	option.transform.GetComponent<Image>().enabled = !option.transform.GetComponent<Image>().enabled;
    }

    void UpdateCursor(){
    	if(cursorPointer > 0 && Input.GetKey(KeyCode.UpArrow)){
            Instantiate(UI_sound, transform.position, Quaternion.identity);
	    	cursorPointer--;
        }
	    if(cursorPointer < 4 && Input.GetKey(KeyCode.DownArrow)){
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
    			sceneSelected = "Tutorial";
    		break;

    		case 2:
    			sceneSelected = "Options";
    		break;

    		case 3:
    			sceneSelected = "Exit";
    		break;

    		default:
    			sceneSelected = "Credits";
    		break;
    	}
    }

    void UpdateSprite(){
    	if(sceneSelected.Equals("Level"))
    		background.transform.GetComponent<Image>().sprite = startSelected;

    	else if(sceneSelected.Equals("Tutorial"))
    		background.transform.GetComponent<Image>().sprite = tutorialSelected;

    	else if(sceneSelected.Equals("Options"))
    		background.transform.GetComponent<Image>().sprite = optionsSelected;

    	else if(sceneSelected.Equals("Exit"))
    		background.transform.GetComponent<Image>().sprite = exitSelected;
    		
    	else if(sceneSelected.Equals("Credits"))
    		background.transform.GetComponent<Image>().sprite = creditsSelected;
    }

}
