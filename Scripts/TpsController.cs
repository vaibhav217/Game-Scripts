using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;
using UnityStandardAssets.Characters.ThirdPerson;

public class TpsController : MonoBehaviour
{
    public FixedJoystick LeftJoystick;
    public FixedButton Button;
    public FixedTouchField TouchField;
    protected ThirdPersonUserControl Control;


    //protected Actions Actions;
    //protected PlayerController;
    //protected RigidBody RigidBody;

    public float CameraAngle;
    public float CameraAngleSpeed = 0.2f;
    //protected float CameraPosY;
    //protected float CameraPosSpeed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        Control = GetComponent<ThirdPersonUserControl>();

    }

    // Update is called once per frame



    void Update()
    {
        Control.m_Jump = Button.Pressed;
        Control.Hinput = LeftJoystick.Horizontal;
        Control.Vinput = LeftJoystick.Vertical;

        CameraAngle += FixedTouchField.TouchDist.x * CameraAngleSpeed;

        Camera.main.transform.position = transform.position + Quaternion.AngleAxis(CameraAngle, Vector3.up) * new Vector3(0, 2, 3.5f);
        Camera.main.transform.rotation = Quaternion.LookRotation(transform.position + Vector3.up * 2f - Camera.main.transform.position, Vector3.up);
        
    }
}
