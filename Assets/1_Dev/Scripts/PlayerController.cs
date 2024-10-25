using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public Vector3 PlayerPosition;
    public int Health = 100;
    public float MoveSpeed = 5f; 
    public float JumpForce = 5f;
    private bool isGrounded = false; 
    private bool isMove;
    private Rigidbody2D rigidBody; 

#region LifeCycle
    private void Awake() {
        Instance = this;

        GameStateEvents.OnGameStart += GameStart;
        GameStateEvents.OnGamePlay += GamePlay;
        GameStateEvents.OnGamePause += GamePause;
        GameStateEvents.OnGameEnd += GamePause;
        GameStateEvents.OnCehckpoint += GamePause;
        GameStateEvents.OnDie += Die;
    }
    
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>(); // Rigidbody2D bileşenini bul.
    }

    void Update()
    {
        Move(); // Hareket işlemini sürekli çalıştır.
        Jump(); // Zıplama işlemini sürekli kontrol et.
    }

    private void OnDisable()  {
        GameStateEvents.OnGameStart -= GameStart;
        GameStateEvents.OnGamePlay -= GamePlay;
        GameStateEvents.OnGamePause -= GamePause;
        GameStateEvents.OnGameEnd -= GamePause;
        GameStateEvents.OnCehckpoint -= GamePause;
        GameStateEvents.OnDie -= Die;
    }

     // Karakterin yere temas edip etmediğini kontrol etmek için.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // Zeminle temas varsa.
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false; // Zeminle temas kalkarsa.
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Item collectible = other.GetComponent<Item>();

        if (collectible != null)
        {
            collectible.Collect(transform);
        }

        Checkpoint checkpoint = other.GetComponent<Checkpoint>();
        if (checkpoint != null)
        {
            checkpoint.Check();
        }
    }
#endregion

    public int GetHealth(){
        return Health;
    }

    private void Move()
    {
        if (!isMove) return;

        float moveX = Input.GetAxis("Horizontal");
        rigidBody.velocity = new Vector2(moveX * MoveSpeed, rigidBody.velocity.y); 
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rigidBody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse); 
        }
    }

    public void ItemCollect(Item collectible) {
        InventoryController.Instance.CurrentInventoryAdd(collectible);
    }

    private void GameStart(){
        transform.position = PlayerPosition;
        GamePause();
    }

    private void GamePause() {
       isGrounded = false;
       isMove = false;
       rigidBody.isKinematic = true;
    }

    private void GamePlay() {
        isGrounded = true;
        isMove = true;
        rigidBody.isKinematic = false;
    }

    private void Die() {
        Health = SaveSystem.Instance.LoadHealth();
        GamePause();
    }
}
