using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Canvas : MonoBehaviour
{
	public Text score;
    public Text ready;
    public Text go;

    public GameObject hearth0;
    public GameObject hearth1;
    public GameObject hearth2;
    public GameObject crossfade;


	public Sprite hearth_filled;
	public Sprite hearth_empty;

    public bool levelStarted = false;

    public AudioClip deathSound;

	private int scorePoints = 0;

	private float delay = 0f;
    private float startTimeLimit = 0.6f;
    private float startTimer = 0f;
    private float goTimeLimit = 0.7f;
    private float goTimer = 0f;

    private bool showGo = false;

	private GameObject player;


    void Start()
    {
    	player = GameObject.Find("Player");
        player.transform.GetComponent<Player>(). canMove = false;

        score.text = "00";

        score.enabled = false;
        ready.enabled = false;
        go.enabled = false;

        hearth0.transform.GetComponent<Image>(). enabled = false;
        hearth1.transform.GetComponent<Image>(). enabled = false;
        hearth2.transform.GetComponent<Image>(). enabled = false;
    }

   
    void Update()
    {
        if(!levelStarted)
            StartLevel();
        if(levelStarted && player.transform.GetComponent<Player>(). canMove)
            UpdateScore();
        if(showGo)
            GoAnimation();
    }

    void GoAnimation(){
        if(goTimer<=goTimeLimit){
            go.enabled = true;
            goTimer += Time.deltaTime;
        }
        else{
            showGo = false;
            go.enabled = false;
        }
    }

    void StartLevel(){
        if(startTimer<=startTimeLimit)
            startTimer += Time.deltaTime;
        else
            StartCoroutine(StartAnimation());
    }

    void LoadNextLevel(){
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene(). buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex){
        crossfade.transform.GetComponent<Animator>().SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(levelIndex);
    }

    void UpdateScore(){
    	delay += 0.5f;
    	if(delay%50==0){
	    	scorePoints++;
	    	score.text = scorePoints.ToString();
    	}
    }

    public void Hit(){
        switch(player.transform.GetComponent<Player>().lives){
            case 2:
                hearth0.transform.GetComponent<Animator>(). SetTrigger("hit");
            break;

            case 1:
                hearth1.transform.GetComponent<Animator>(). SetTrigger("hit");
            break;

            default:
                hearth2.transform.GetComponent<Animator>(). SetTrigger("hit");
                StartCoroutine(ProccesBeforeDeath());
            break;
        }
    }

    IEnumerator ProccesBeforeDeath(){
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i = 0; i<enemies.Length; i++)
            enemies[i].transform.GetComponent<Enemy>().canGo=false;

        GameObject.Find("ScoreTransition").transform.GetComponent<ScoreTransition>(). score = this.scorePoints;
        DontDestroyOnLoad(GameObject.Find("ScoreTransition"));

        player.transform.GetComponent<Player>(). canMove = false;

        GameObject.Find("Game").transform.GetComponent<AudioSource>(). Stop();

        yield return new WaitForSeconds(1f);

        StartCoroutine(DeathAnimation());
    }

    IEnumerator DeathAnimation(){
        player.transform.GetComponent<Animator>(). SetTrigger("die");

        this.transform.GetComponent<AudioSource>(). enabled = true;
        this.transform.GetComponent<AudioSource>(). PlayOneShot(deathSound);

        yield return new WaitForSeconds(2f);

        LoadNextLevel();
    }

    IEnumerator StartAnimation(){
            ready.enabled = true;
            
            yield return new WaitForSeconds(2f);
            
            ready.enabled = false;
            
            score.enabled = true;
            hearth0.transform.GetComponent<Image>(). enabled = true;
            hearth1.transform.GetComponent<Image>(). enabled = true;
            hearth2.transform.GetComponent<Image>(). enabled = true;
            
            levelStarted = true;
            player.transform.GetComponent<Player>(). canMove = true;
            showGo = true;
    }
}
