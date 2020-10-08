using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CmFreelookOnlyWhenRightMouseDown : MonoBehaviour
{
    void Start(){
        CinemachineCore.GetInputAxis = GetAxisCustom;
    }
    
    public float GetAxisCustom(string axisName){
        if(axisName == "Mouse X"){
            if (Input.GetMouseButton(1))
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                return Input.GetAxis("Mouse X");
            } else{
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
                return 0;
            }
        }
        else if (axisName == "Mouse Y"){
            if (Input.GetMouseButton(1)){
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                return Input.GetAxis("Mouse Y");
            } else{
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
                return 0;
            }
        }
        return Input.GetAxis(axisName);
    }
}
