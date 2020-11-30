using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Animator camera_Animator;

    void Start(){
    	camera_Animator = this.transform.GetComponent<Animator>();
    }

    public void CameraShake(){
    	camera_Animator.SetTrigger("shake");
    }

    public void HardCameraShake(){
    	camera_Animator.SetTrigger("hard_shake");
    }
}
