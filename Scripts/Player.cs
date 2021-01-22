using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{

    [HideInInspector]
    public InputStr Input;
    public struct InputStr
    {
        public float LookX;
        public float LookZ;
        public float RunX;
        public float RunZ;
        public bool Jump;
    }

    public Transform Point;
    public GameObject projectile;
    public Vector3 jump;
    public float jumpForce = 2.0f;
    public bool isGrounded;

    public PickUpController Gun;
    public float Speed = 10f;
    public float JumpForce = 7f;
    public Button Button;
    protected Rigidbody Rigidbody;
    private Animator animator;
    protected Quaternion LookRotation;
    private Controller Controller;
    protected bool Grounded;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        Controller = GetComponent<Controller>();
        animator = GetComponent<Animator>();
        Gun = GetComponent<PickUpController>();

    }

    private void FixedUpdate()
    {
        var inputRun = Vector3.ClampMagnitude(new Vector3(Input.RunX, 0, Input.RunZ), 1);
        var inputLook = Vector3.ClampMagnitude(new Vector3(Input.LookX, 0, Input.LookZ), 1);

        Rigidbody.velocity = new Vector3(inputRun.x * Speed, Rigidbody.velocity.y, inputRun.z * Speed);


        //rotation to go target
        if (inputLook.magnitude > 0.01f)
            LookRotation = Quaternion.AngleAxis(Vector3.SignedAngle(Vector3.forward, inputLook, Vector3.up), Vector3.up);

        transform.rotation = LookRotation;
        
    }


    void Start()
    {
        jump = new Vector3(0.0f, 10.0f, 0.0f);
    }
    void OnCollisionStay()
    {
        isGrounded = true;
    }

    void Update()
    {
        if (animator == null) return;

        var x = Controller.Joystick.Horizontal;
        var y = Controller.Joystick.Vertical;
        Move(x, y);
    }
    public void Jump()
    {
        if (isGrounded)
        {

            Rigidbody.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            animator.SetTrigger("Jump");

        }

    }
        public void FireStart()
    {


        ///Attack code here
        Rigidbody rb =  Instantiate(projectile, Point.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 62f, ForceMode.Impulse);
        rb.AddForce(transform.up * 3f, ForceMode.Impulse);
        animator.SetBool("Fire", true);


    }
    public void FireStop()
    {
        animator.SetBool("Fire", false);
    }
    private void Move(float x, float y)
    {
        if(PickUpController.slotFull == true)
        {
            animator.SetBool("WeaponE", true);
            animator.SetFloat("VelX", x, 0.1f, Time.deltaTime);
            animator.SetFloat("VelY", y, 0.1f, Time.deltaTime);
        }
        else
        {
            animator.SetBool("WeaponE", false);
            animator.SetFloat("VelX", x, 0.1f, Time.deltaTime);
        animator.SetFloat("VelY", y, 0.1f, Time.deltaTime);
        }
    }

}
