using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    [SerializeField] BallController ballController;
    [SerializeField] CameraController camController;
    private void Update()
    {
        var inputActive = Input.GetMouseButton(button: 0) 
            && ballController.IsMove() == false 
            && ballController.ShootingMode == false;
            
        camController.SetInputActive(inputActive);
    }
}
