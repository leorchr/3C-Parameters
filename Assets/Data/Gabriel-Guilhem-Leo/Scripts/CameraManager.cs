using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    [SerializeField] private SpringArm springArm;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fps"))
        {
            springArm.cameraStatus = CameraStatus.FirstPerson;
        }
        else if (other.CompareTag("TpsClose"))
        {
            springArm.cameraStatus = CameraStatus.ThirdPersonClose;
        }
        else if (other.CompareTag("TpsFar"))
        {
            springArm.cameraStatus = CameraStatus.ThirdPersonFar;
        }
        else if (other.CompareTag("Tps"))
        {
            springArm.cameraStatus = CameraStatus.ThirdPerson;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Fps") || other.CompareTag("TpsClose") || other.CompareTag("TpsFar"))
        {
            springArm.cameraStatus = CameraStatus.ThirdPerson;
        }
    }
}
