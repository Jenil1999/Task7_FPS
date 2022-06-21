using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController;
    public float Speed = 20f;
    public float Gravity = -9.81f;

    public Animator PlayerAnimator;

    public static PlayerMovement Instance;
        
    public float JumpHeight = 3f;
    public Transform Leg;
    public float GroundDistance = 0.2f;
    public LayerMask GroundMask;

    public float Health = 50f;

    public TextMeshProUGUI PlayerHealth;


    Vector3 Velocity;
    bool IsGrounded;

    //========================================================================================================

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        
        PlayerAnimator = GetComponent<Animator>();
        
    }

    void Update()
    {

        IsGrounded = Physics.CheckSphere(Leg.position, GroundDistance, GroundMask);


        if (IsGrounded && Velocity.y < 0)
        {
            Velocity.y = -2f;
        }
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        Vector3 Move = transform.right * h + transform.forward * v;

        characterController.Move(Move * Speed * Time.deltaTime);
       // Debug.Log("Move" + Move + "multiplyer" + Move*Speed*Time.deltaTime + "h" + h + "v" + v);
        

        
        if(Input.GetButtonDown("Jump") && IsGrounded)
        {
            //LayerMask.GetMask("Ground");
            Velocity.y = Mathf.Sqrt(JumpHeight * -2f * Gravity);
            IsGrounded = true;
        }

        Velocity.y += Gravity * Time.deltaTime;

        PlayerAnimator.SetFloat("Speed", v);

        characterController.Move(Velocity * Time.deltaTime);

        PlayerHealth.text = Health.ToString();
      
    }

    public void TakeDamage(float amount)
    {
        Health -= amount;

        if (Health <= 0)
        {
            Die();
        }

    }

    void Die()
    {
        SceneManager.LoadScene(2);
    }

}
   


