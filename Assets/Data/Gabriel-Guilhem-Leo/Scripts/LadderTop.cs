using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderTop : Interactive
{
    [SerializeField] private GameObject player;
    public Transform topPosition;
    public Transform topPositionExit;
    public Transform botPosition;
    public Ladder ladder;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public override void OnInteraction()
    {
        PlayerController.Instance.topPosition = topPosition.position;
        PlayerController.Instance.botPosition = botPosition.position;
        if (ladder.onLadder)
        {
            ladder.onLadder = false;
            SpringArm.Instance.cameraStatus = CameraStatus.FirstPerson;
            player.transform.position = topPositionExit.position;
            player.GetComponent<Rigidbody>().useGravity = true;
            PlayerController.Instance.canRotate = true;
            PlayerController.Instance.canMove = true;
        }
        else
        {
            ladder.onLadder = true;
            SpringArm.Instance.cameraStatus = CameraStatus.ThirdPerson;
            player.transform.position = topPosition.position;
            player.GetComponent<Rigidbody>().useGravity = false;
            PlayerController.Instance.canRotate = false;
            PlayerController.Instance.canMove = false;
        }

    }
}
