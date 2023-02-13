using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    [SerializeField] private SpringArm springArm;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Fps"))
        {
            springArm.cameraStatus = CameraStatus.FirstPerson;
        }
        
     }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Fps"))
        {
            springArm.cameraStatus = CameraStatus.ThirdPerson;
        }
    }
}
