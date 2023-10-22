using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float xAngleLeft = 30f;
    [SerializeField] private float xAngleRight = 30f;
    
    [SerializeField] private GameObject leftHandle;
    [SerializeField] private GameObject rightHandle;
    [SerializeField] private GameObject leftCenterOfMass;
    [SerializeField] private GameObject leftPointToApplyGravity;
    [SerializeField] private GameObject rightCenterOfMass;
    [SerializeField] private GameObject rightPointToApplyGravity;

    [SerializeField] private float gravityForce;
    private Rigidbody leftHandleRb;

    private Quaternion leftBottomRotation;
    private Quaternion leftTopRotation;

    private Rigidbody rightHandleRb;

    private Quaternion rightBottomRotation;
    private Quaternion rightTopRotation;

   
    private Vector3 gravityForceVector;

    [SerializeField] private float handleForce = 1f;

    [SerializeField] private GameManager gameManager;
    private void Start()
    {
        gravityForceVector=Vector3.down * gravityForce;
        leftBottomRotation.eulerAngles= new Vector3(xAngleLeft-1, 0,0);
        leftTopRotation.eulerAngles = new Vector3(360- xAngleLeft+1, 0, 0);
        leftHandleRb = leftHandle.GetComponent<Rigidbody>();

       leftHandleRb.centerOfMass = leftHandleRb.transform.InverseTransformPoint(leftCenterOfMass.transform.position);

        rightBottomRotation.eulerAngles = new Vector3(xAngleRight - 1, 180, 0);
        rightTopRotation.eulerAngles = new Vector3(360 - xAngleRight + 1, 180, 0);
        rightHandleRb = rightHandle.GetComponent<Rigidbody>();

        rightHandleRb.centerOfMass = rightHandleRb.transform.InverseTransformPoint(rightCenterOfMass.transform.position);

        
    }
    private void Update()
    {
        if (!gameManager.isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                // leftcurTimer = handleTimer;
                //isActiveLeft = true;
                // StartCoroutine(RotateHandle(leftHandle, leftcurTimer, isActiveLeft, leftBottomRotation, leftTopRotation));
                leftHandleRb.AddTorque(Vector3.up * handleForce, ForceMode.Impulse);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                // leftcurTimer = handleTimer;
                //isActiveLeft = true;
                // StartCoroutine(RotateHandle(leftHandle, leftcurTimer, isActiveLeft, leftBottomRotation, leftTopRotation));
                rightHandleRb.AddTorque(Vector3.up * handleForce, ForceMode.Impulse);
            }
            CheckRestrictions();
            AddGravity();
        }
        else
        {
            leftHandleRb.velocity = Vector3.zero;
            leftHandleRb.angularVelocity = Vector3.zero;
            rightHandleRb.velocity = Vector3.zero;
            rightHandleRb.angularVelocity = Vector3.zero;
        }
    }
    private void CheckRestrictions()
    {

        if( leftHandle.transform.rotation.eulerAngles.x>0 && leftHandle.transform.rotation.eulerAngles.x <=180 && leftHandle.transform.rotation.eulerAngles.x> xAngleLeft)
        {
           // Debug.Log("freexleft");
            leftHandleRb.angularVelocity = Vector3.zero;
            leftHandleRb.velocity = Vector3.zero;
            leftHandle.transform.rotation = leftBottomRotation;
        }
        else if (leftHandle.transform.rotation.eulerAngles.x >180 && leftHandle.transform.rotation.eulerAngles.x < 360 - xAngleLeft)
        {
            leftHandleRb.angularVelocity = Vector3.zero;
            leftHandleRb.velocity = Vector3.zero;
            leftHandle.transform.rotation = leftTopRotation;
           // Debug.Log("freexbottom"+ leftHandle.transform.rotation.eulerAngles.x);
        }

        if (rightHandle.transform.rotation.eulerAngles.x > 0 && rightHandle.transform.rotation.eulerAngles.x <= 180 && rightHandle.transform.rotation.eulerAngles.x > xAngleRight)
        {
            // Debug.Log("freexright");
            rightHandleRb.angularVelocity = Vector3.zero;
            rightHandleRb.velocity = Vector3.zero;
            rightHandle.transform.rotation = rightBottomRotation;
        }
        else if (rightHandle.transform.rotation.eulerAngles.x > 180 && rightHandle.transform.rotation.eulerAngles.x < 360 - xAngleRight)
        {
            rightHandleRb.angularVelocity = Vector3.zero;
            rightHandleRb.velocity = Vector3.zero;
            rightHandle.transform.rotation = rightTopRotation;
            // Debug.Log("freexbottom"+ rightHandle.transform.rotation.eulerAngles.x);
        }
    }
    private void AddGravity()
    {
        if(leftHandle.transform.rotation != leftBottomRotation)
        leftHandleRb.AddForceAtPosition(gravityForceVector, leftPointToApplyGravity.transform.position);

        if (rightHandle.transform.rotation != rightBottomRotation)
            rightHandleRb.AddForceAtPosition(gravityForceVector, rightPointToApplyGravity.transform.position);
    }
   /* IEnumerator RotateHandle(GameObject handle, float timer, bool isActive, Quaternion startRotation, Quaternion endRotation)
    {
        while(isActive)
        {
            timer -=Time.deltaTime;
            if(timer>0)
            {
                handle.transform.rotation = Quaternion.Slerp(startRotation, endRotation, (handleTimer- timer)/ handleTimer);
            }
            else
            {
                handle.transform.rotation = endRotation;
                yield break;
            }
            yield return null;
        }
    }*/

}
