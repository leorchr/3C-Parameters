using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    [SerializeField] private SpringArm springArm;
    private void OnTriggerEnter(Collider other)
    {
        if(springArm.cameraStatus == CameraStatus.FirstPerson ||
            springArm.cameraStatus == CameraStatus.ThirdPersonClose ||
            springArm.cameraStatus == CameraStatus.ThirdPersonFar
            || springArm.cameraStatus == CameraStatus.ThirdPersonBFC)
        {
            if(other.CompareTag("Fps") || other.CompareTag("TpsClose") || other.CompareTag("TpsFar") || other.CompareTag("TpsBFC"))
            {
                springArm.cameraStatus = CameraStatus.ThirdPerson;
            }
        }
        else if (other.CompareTag("Fps"))
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
        else if (other.CompareTag("TpsBFC"))
        {
            springArm.cameraStatus = CameraStatus.ThirdPersonBFC;
        }
    }
}
