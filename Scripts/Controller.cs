using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public FixedJoystick Joystick;
    public FixedButton Button;
    public FixedTouchField TouchField;
    public Player Player;

    //Camera Controll
    public Vector3 CameraPivot;
    public float CameraDistance;

    public float RotationSpeed = 20;

    protected float InputRotationX;
    protected float InputRotationY;

    void Start()
    {
        
    }


    void Update()
    {
        InputRotationX = InputRotationX + FixedTouchField.TouchDist.x * RotationSpeed * Time.deltaTime % 360f;
        InputRotationY = Mathf.Clamp(InputRotationY - FixedTouchField.TouchDist.y * RotationSpeed * Time.deltaTime, -88f, 88f);

        //left and forward
        var characterForward = Quaternion.AngleAxis(InputRotationX, Vector3.up * Time.deltaTime) * Vector3.forward ;
        var characterLeft = Quaternion.AngleAxis(InputRotationX + 90, Vector3.up * Time.deltaTime) * Vector3.forward;

        //look and run direction
        var runDirection = characterForward * (Input.GetAxisRaw("Vertical") + Joystick.Vertical) +  characterLeft * (Input.GetAxisRaw("Horizontal") + Joystick.Horizontal);
        var LookDirection = Quaternion.AngleAxis(InputRotationY, characterLeft) * characterForward;

        //set player values
        Player.Input.RunX = runDirection.x;
        Player.Input.RunZ = runDirection.z;
        Player.Input.LookX = LookDirection.x;
        Player.Input.LookZ = LookDirection.z;


        var CharacterPivot = Quaternion.AngleAxis(InputRotationX, Vector3.up) * CameraPivot;
        StartCoroutine(setCamera(LookDirection, CharacterPivot));
    }

    private IEnumerator setCamera(Vector3 LookDirection, Vector3 CharacterPivot)
    {

        yield return new WaitForFixedUpdate();

        //set camera values
        Camera.main.transform.position = (transform.position + CharacterPivot) - LookDirection * CameraDistance;
        Camera.main.transform.rotation = Quaternion.LookRotation(LookDirection, Vector3.up);
    }
}

