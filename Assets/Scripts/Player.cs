using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public int lives = 3;

    public bool isRecovering = false;
    public bool canMove = true;

    public GameObject effect;
    public GameObject camera;
    public GameObject sound;

    private float inputTimeLimit = 0.1f;
    private float inputTimer = 0f;
    private float recoverTimeLimit = 2f;
    private float recoverTimer = 0f;
    private float ascendTimeLimit = 0.1f;
    private float ascendTimer = 0f;

    private bool isAscending = true;


    void Update(){ 
        if(canMove)
            updatePlayerPosition();
        AscendAnimation();
        Recover();
    }

    void AscendAnimation(){
        if(ascendTimer<=ascendTimeLimit){
            if(isAscending)
                UpdatePosition(0, 0.01f);
            else
                UpdatePosition(0, -0.01f);
            ascendTimer += Time.deltaTime * 0.5f;
        }
        else{
            isAscending = !isAscending;
            ascendTimer = 0f;
        }
    }

    Vector3 GetCurrentPosition(){
        return this.transform.localPosition;
    }

    void UpdatePosition(float X, float Y){
        Vector3 newPosition = GetCurrentPosition();
        newPosition.x += X;
        newPosition.y += Y;
        this.transform.localPosition = newPosition;
    }


    void updatePlayerPosition(){
        Vector3 currentPosition = GetCurrentPosition();
        if(inputTimer>=inputTimeLimit){
            if(currentPosition.y<=-2f && Input.GetKey(KeyCode.UpArrow)){
                MovementEffect();
                UpdatePosition(0f, 2f);
            }
            else if(currentPosition.y>=-4 && Input.GetKey(KeyCode.DownArrow)){
                MovementEffect();
                UpdatePosition(0f, -2f);
            }
            inputTimer = 0f;
        }
        else 
            inputTimer += Time.deltaTime;
    }

    void MovementEffect(){
        Instantiate(effect, transform.position, Quaternion.identity);
        camera.transform.GetComponent<Camera>().CameraShake();
        Instantiate(sound, transform.position, Quaternion.identity);
    }

    void Recover(){
        if(isRecovering){
             recoverTimer += Time.deltaTime;
            if(recoverTimer>=recoverTimeLimit){
                recoverTimer = 0f;
                this.transform.GetComponent<Animator>().SetTrigger("recover");
                isRecovering = false;
            }
        }
    }  
}
