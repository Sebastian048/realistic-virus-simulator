using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public float speed;

    public bool testing;

    Vector3 mPrevPos = Vector3.zero;
    Vector3 mPosDelta = Vector3.zero;

    public float minYRot;
    public float maxYRot;


    public Vector2 rotate;
    public Vector2 move;
    public Vector3 cameraRotation;
    
    public Vector3 cameraLocation;
    public Vector3 cameraLocationNew;

    public Vector2 mousePos;

    public Camera cam;
    public Transform camTransform;
    public bool sliderTimeChanging;

    Vector3 touchStart;
    public float zoomOutMin = 1;
    public float zoomOutMax = 8;
    public Vector3 newCamPos;

    public bool twoFingerTouch;

    public Vector3 cameraForward;
    public Vector3 cameraRight;

    public Transform fakeMovement;
    public Vector2 fakeMove;
    public Vector3 fakeCameraLocation;

    public float lastMoveX;
    public float lastMoveY;
    public bool mouseUp;

    public Vector3 point;
    public bool rotating;

    public Quaternion saveRotation;

    public Transform clickPositoin;
    public Camera fakeCamera;

    public bool alreadyChecked;

    public Vector2 differenceInRotate;

    public bool buttonIsBeingPressed;
    public bool hasRotated;

    public Vector3 right;
    public Vector3 forward;
    public Vector3 movement;

    public FixedJoystick fixedJoystick;

    void Start () {
        Application.targetFrameRate = 60;

        fakeCamera = Camera.main;

        saveRotation = transform.localRotation;
        point = transform.position;
    }
    void Update () {
        cameraForward = transform.forward;
        cameraRight = transform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;

        if (sliderTimeChanging == false && buttonIsBeingPressed == false) {

            if(Input.touchCount == 2){
                rotating = true;

            twoFingerTouch = true;
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            float increment = difference * 0.05f;

            newCamPos = new Vector3 (0, 0, cam.transform.localPosition.z + increment);

            newCamPos.z = Mathf.Clamp(newCamPos.z, -150, -5);
            

            cam.transform.localPosition = newCamPos;
            }

            if (Input.touchCount == 0) {
                twoFingerTouch = false;
                
                if (alreadyChecked == false) {
                    if (rotating == false) {
                        transform.position = point;
                        if (hasRotated == false) {

                        }
                    } else {
                        transform.localRotation = saveRotation;
                    }
                    alreadyChecked = true;
                    rotating = false;
                    mouseUp = true;
                }
            }

            



            if (Input.touchCount == 1 && twoFingerTouch == false) {
                mousePos = Input.mousePosition;

                if ((mousePos.x > Screen.width / 2) || (mousePos.x > 2000 / 2 && testing == true && (fixedJoystick.Vertical == 0 && fixedJoystick.Horizontal == 0))) {
                if (alreadyChecked == true) {
                    differenceInRotate = Vector2.zero;
                }
                alreadyChecked = false;   

                differenceInRotate.x += Input.touches[0].deltaPosition.x / 5;
                differenceInRotate.y += Input.touches[0].deltaPosition.y / 5;

                rotate.x -= Input.touches[0].deltaPosition.x / 5;
                rotate.y -= Input.touches[0].deltaPosition.y / 5;

                if (differenceInRotate.x > 5 || differenceInRotate.x < -5 || differenceInRotate.y > 5 || differenceInRotate.y < -5) {
                    rotating = true;
                    saveRotation = transform.localRotation;
                    hasRotated = true;
                }
                
                cameraRotation = new Vector3 (-rotate.y, rotate.x, 0);

                cameraRotation.x = Mathf.Clamp(cameraRotation.x, 5, 90);

                if (cameraRotation.x == 5) {
                    rotate.y = -5;
                }
                if (cameraRotation.x == 90) {
                    rotate.y = -90;
                }
                 
                transform.localRotation = Quaternion.Euler(cameraRotation);

                }
                
                //move.x += Input.touches[0].deltaPosition.x / 50;
                //move.y += Input.touches[0].deltaPosition.y / 50;
                float xVal = fixedJoystick.Horizontal;
                float yVal = fixedJoystick.Vertical;

                forward = new Vector3 (camTransform.forward.x, 0, camTransform.forward.z);
                right = new Vector3 (camTransform.right.x, 0, camTransform.right.z);

                movement = xVal * right + yVal * forward;

                this.transform.Translate(movement * speed, Space.World);




                if (mouseUp == true) {
                    mouseUp = false;

                    point = -Vector3.one;
                    Plane plane = new Plane(Vector3.up, 0f);
                    Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                    float distanceToPlane;

                    if (plane.Raycast(ray, out distanceToPlane)) {
                        point = ray.GetPoint(distanceToPlane);
                        point.x = Mathf.Clamp(point.x, -75, 75);
                        point.z = Mathf.Clamp(point.z, -75, 75);
                        Debug.Log(point);
                    }
                }
            }
        }
    }

    public void HoldingButton() {
        buttonIsBeingPressed = true;
    }
    public void LetGoOfButton () {
        StartCoroutine(StopButtonPress());
    }
    IEnumerator StopButtonPress() {
        yield return new WaitForSecondsRealtime(0.25f);

        buttonIsBeingPressed = false;
    }


    public void ChangingTimeSliderValue (bool changing) {
        sliderTimeChanging = changing;
    }
}
