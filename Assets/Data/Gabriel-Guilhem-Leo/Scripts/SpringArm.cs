using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpringArm : MonoBehaviour
{
    #region Rotation Settings
    [Space]
    [Header("Rotation Settings \n-------------------------")]
    [Space]
    [SerializeField] private bool useControlRotation = true;
    [SerializeField] private float mouseSensitivity = 500f;
    [SerializeField] private bool invertYAxis;
    [SerializeField][Range(-90.0f, 0)] private float minAngle = -90f;
    [SerializeField][Range(0, 90.0f)] private float maxAngle = 90f;

    private float pitch;
    private float yaw;
    private int axeInt;

    
    #endregion

    #region Follow Settings

    [Space]
    [Header("Follow Settings \n-------------------------")]
    [Space]
    [SerializeField] private Transform target;
    [SerializeField] private float movementSmoothTime = 0.5f;
    [SerializeField] private Vector3 targetOffset = new Vector3(0, 1.8f, 0);
    [SerializeField] private float targetArmLength = 13f;
    [SerializeField] private Vector3 cameraOffset = new Vector3(0.5f, 0, -0.3f);

    private Vector3 moveVelocity;
    private Vector3 endPoint;
    private Vector3 cameraPosition;

    #endregion

    #region Collisions

    [Space]
    [Header("Collision Settings \n-------------------------")]
    [Space]
    [SerializeField] private bool doCollisionTest = true;
    [Range(2, 20)][SerializeField] private int collisiontestResolution = 4;
    [SerializeField] private float collisionProbeSize = 0.3f;
    [SerializeField] private float collisionSmoothTime = 0.05f;
    [SerializeField] private LayerMask collisionLayerMask = ~0;

    private RaycastHit[] hits;
    private Vector3[] raycastPositions;


    #endregion

    // Start is called before the first frame update
    void Start()
    {
        raycastPositions = new Vector3[collisiontestResolution];
        hits = new RaycastHit[collisiontestResolution];
    }

    private void OnValidate()
    {
        raycastPositions= new Vector3[collisiontestResolution];
        hits = new RaycastHit[collisiontestResolution];
    }

    // Update is called once per frame
    void Update()
    {
        if(!target)
            return;

        if(doCollisionTest)
        {
            CheckCollisions();
        }

        SetCameraTransform();

        if (useControlRotation && Application.isPlaying)
        {
            Rotate();
        }

        Vector3 targetPosition = target.position + targetOffset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref moveVelocity, movementSmoothTime);
    }

    //Ca c'est de la documentation qu'on peut rajouter
    /// <summary>
    /// Handle Rotation
    /// </summary>
    private void Rotate()
    {
        //Notation ternaire = faire ce if juste au dessu en une ligne pour avoir un retour de valeur, gauche si c'est true et droite si c'est false
        axeInt = invertYAxis? -1 : 1;
        pitch -= Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime * axeInt;
        yaw += Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
        
        //Bloque la caméra verticalement pour pas qu'elle passe derrière le player
        pitch = Mathf.Clamp(pitch, minAngle, maxAngle);

        transform.localRotation =Quaternion.Euler(pitch, yaw, 0f);
    }

    private void SetCameraTransform()
    {
        Transform trans = transform;

        Vector3 targetArmOffset = cameraOffset - new Vector3(0, 0, targetArmLength);
        endPoint = trans.position + (trans.rotation * targetArmOffset);

        if (doCollisionTest)
        {
            float minDistance = targetArmLength;
            foreach (RaycastHit hit in hits)
            {
                if (!hit.collider)
                {
                    continue;
                }
                float distance = Vector3.Distance(hit.point, trans.position);
                if (minDistance > distance)
                {
                    minDistance = distance;
                }
            }

            Vector3 dir = (endPoint - trans.position).normalized;
            Vector3 armOffset = dir * (targetArmLength - minDistance);
            cameraPosition = endPoint - armOffset;
        }
        else
        {
            cameraPosition = endPoint;
        }

        Vector3 cameraVelocity = Vector3.zero;
        foreach (Transform child in trans)
        {
            child.position = Vector3.SmoothDamp(child.position, cameraPosition, ref cameraVelocity, collisionSmoothTime);
        }
    }

    ///<summary>
    ///Checks for collisions and fill the raycastPositions and hits array
    /// </summary>
    private void CheckCollisions()
    {
        Transform trans = transform;

        for(int i = 0, angle = 0; i<collisiontestResolution; i++, angle += 360 / collisiontestResolution)
        {
            Vector3 rayvastLocalEndPoint = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0) * collisionProbeSize;
            raycastPositions[i] = endPoint + (trans.rotation * rayvastLocalEndPoint);
            Physics.Linecast(trans.position, raycastPositions[i], out hits[i], collisionLayerMask);
        }
    }
}
