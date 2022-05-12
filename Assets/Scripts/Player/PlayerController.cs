using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // player Stats
    public Stats playerStats { get; protected set; }
    [SerializeField]
    private float Health = 100;

    // Camera Movment
    [SerializeField]
    private Transform camXPivot;
    private float mouseSensitivity = 5f;
    private float cameraPitch = 0f;

    // for player
    private CharacterController characterController;

    // gravity
    [SerializeField]
    private float gravity = -15f;
    private float VelocityY = 0f;

    private bool GamePlayState = true;

    [SerializeField]
    private CombatChannelSO combatChannel;

    private void OnEnable()
    {
        combatChannel.ZombieAttackedEventRaised += PlayerTakeDamage;
        combatChannel.GameOverEventRaised += GameOver;
    }
    private void OnDisable()
    {
        combatChannel.ZombieAttackedEventRaised -= PlayerTakeDamage;
        combatChannel.GameOverEventRaised -= GameOver;
    }


    private void Awake()
    {
        playerStats = new Stats(Health,10,1,100);

        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    private void Update()
    {
        if(GamePlayState)
        {
            MouseLook();
            Movment();
        }
    }



    private void MouseLook()
    {
        Vector2 mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        cameraPitch -= mouseInput.y * mouseSensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90);
        // rotate camera pivot                      
        camXPivot.localEulerAngles = Vector3.right * cameraPitch;

        // rotate player
        transform.Rotate(Vector3.up * mouseInput.x * mouseSensitivity);
    }

    private void Movment()
    {

        Vector2 inputDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        inputDirection.Normalize();

        if(characterController.isGrounded)
            VelocityY = 0f;

        VelocityY += gravity * Time.deltaTime;

        Vector3 velocity = (transform.forward * inputDirection.y + transform.right * inputDirection.x) * playerStats.MoveSpeed + Vector3.up * VelocityY;
        characterController.Move(velocity * Time.deltaTime);


    }
    

    private void PlayerTakeDamage(float ammount)
    {
        playerStats.TakeDamage(ammount);
        if(playerStats.Health <= 0)
            combatChannel.GameOver();
    }

    private void GameOver()
    {
        GamePlayState = false;
    }
}
