using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderTop : Interactive
{
    public static LadderTop Instance;

    [SerializeField] private GameObject player;
    public Transform topPosition;
    public Transform topPositionExit;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public override void OnInteraction()
    {
        if(Ladder.Instance.onLadder)
        {
            Ladder.Instance.onLadder = false;
            SpringArm.Instance.cameraStatus = CameraStatus.FirstPerson;
            player.transform.position = topPositionExit.position;
            player.GetComponent<Rigidbody>().useGravity = true;
            PlayerController.Instance.canRotate = true;
            PlayerController.Instance.canMove = true;
        }
        else
        {
            Ladder.Instance.onLadder = true;
            SpringArm.Instance.cameraStatus = CameraStatus.ThirdPerson;
            player.transform.position = topPosition.position;
            player.GetComponent<Rigidbody>().useGravity = false;
            PlayerController.Instance.canRotate = false;
            PlayerController.Instance.canMove = false;
        }

    }
}
