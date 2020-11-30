using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Squad : MonoBehaviour
{
	public GameObject[] squad = new GameObject[2];
    public GameObject player;

	public bool form = false;

	public float squadDelay;

	private float squadDelayLimit;
	private float squadDelayTimer = 0f;

    private bool levelStarted = false;
 

    void Start()
    {
        squadDelayLimit = squadDelay;
        player = GameObject.Find("Player");
    }

    void Update()
    {
        CanStart();
        if(levelStarted){
            if(GetPlayerLives() > 0)
            	CanGo();
            	if(form)
                	FormSquad();
        }
    }

    int GetPlayerLives(){
        return player.transform.GetComponent<Player>().lives;
    }

    void CanStart(){
        levelStarted = GameObject.Find("Canvas").transform.GetComponent<Canvas>(). levelStarted;
    }

    void FormSquad(){
    	int holePosition = Random.Range(0,3);
    	switch(holePosition){
    		case 0:
                PrepareSquad(-3f,-5f);
    		break;

    		case 1:
                PrepareSquad(-1f,-5f);
    		break;

    		default:
                PrepareSquad(-1f,-3f);
    		break;
    	}

    	squad[0].transform.GetComponent<SpriteRenderer>().enabled = true;
    	squad[1].transform.GetComponent<SpriteRenderer>().enabled = true;
    	
    	form = false;
    }

    void PrepareSquad(float FirstEnemyPosition, float SecondEnemyPosition){
        squad[0].transform.GetComponent<Enemy>().UpdateEnemyPosition(FirstEnemyPosition);
        squad[1].transform.GetComponent<Enemy>().UpdateEnemyPosition(SecondEnemyPosition);
    }

    void CanGo(){
    	if(squadDelayTimer<squadDelay)
    		squadDelayTimer += Time.deltaTime;
    	else{
    		squad[0].transform.GetComponent<Enemy>().canGo = true;
    		squad[1].transform.GetComponent<Enemy>().canGo = true;
    	}
    }

}
