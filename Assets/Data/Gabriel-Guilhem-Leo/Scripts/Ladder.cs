using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : Interactive
{
    public static Ladder Instance;

    [SerializeField] private GameObject player;
    public Transform bottomPosition;
    public Transform topPosition;
    [HideInInspector] public bool onLadder = false;

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
        player.transform.position = bottomPosition.position;

        if(onLadder)
        {
            onLadder = false;
            SpringArm.Instance.cameraStatus = CameraStatus.FirstPerson;
            player.GetComponent<Rigidbody>().useGravity = true;
            PlayerController.Instance.canRotate = true;
            PlayerController.Instance.canMove = true;
        }
        else
        {
            onLadder = true;
            SpringArm.Instance.cameraStatus = CameraStatus.ThirdPerson;
            player.GetComponent<Rigidbody>().useGravity = false;
            PlayerController.Instance.canRotate = false;
            PlayerController.Instance.canMove = false;
        }

    }
}
