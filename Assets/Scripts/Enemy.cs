using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
	public bool canGo = false;

    public GameObject squad;
    public GameObject effect;
    public GameObject camera;
    public GameObject hit_sound;

	private GameObject player;

	private float speed = 8f;
	

    void Start()
    {
        player = GameObject.Find("Player");
    }
 
    void Update()
    {
    	if(canGo)
        	EnemyMovement();
        CheckCollision();
    }

    public void UpdateEnemyPosition(float Y){
    	Vector3 newPosition = getCurrentPosition();
    	newPosition.y = Y;
    	this.transform.localPosition = newPosition;
    }

    Vector3 getCurrentPosition(){
        return this.transform.localPosition;
    }

    void EnemyMovement(){
    	Vector3 newPosition = getCurrentPosition();
    	if(newPosition.x<=-10f){
    		newPosition.x = 14f;
    		squad.transform.GetComponent<Squad>().form=true;
    	}
    	else
    		newPosition.x -= speed * Time.deltaTime;
    	this.transform.localPosition = newPosition;
    }

    bool IsPlayerVulnerable(){
        return !player.transform.GetComponent<Player>().isRecovering && player.transform.GetComponent<Player>(). canMove;
    }

    void PlayerRecoverEffect(){
        player.transform.GetComponent<Player>().isRecovering = true;
        player.transform.GetComponent<Animator>().SetTrigger("recover");
    }

    void EnemyHitEffect(){
        Instantiate(hit_sound, transform.position, Quaternion.identity);
        GameObject.Find("Canvas").transform.GetComponent<Canvas>().Hit();
        camera.transform.GetComponent<Camera>().HardCameraShake();

        Instantiate(effect, transform.position, Quaternion.identity);
        this.transform.GetComponent<SpriteRenderer>().enabled = false; 
    }

    void CheckCollision(){
    	if(IsPlayerVulnerable()){
		    if(this.transform.GetComponent<Collider2D>().IsTouching(player.transform.GetComponent<Collider2D>())){
		    	player.transform.GetComponent<Player>().lives -= 1;

                if(player.transform.GetComponent<Player>(). lives > 0){
		    	     PlayerRecoverEffect();
                }

		    	EnemyHitEffect();     
		    }
		}
    }
}
