using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxSpeed = 5f;
    public float jumpForce = 10f; // 점프 힘
    public float slideSpeed = 20f; // 대쉬 속도


    private float tempMaxspeed;
    private float tempMoveInput;
    private float ViewDirection = 1f;
    private int jumpCount = 0;

    
    public Transform groundCheck;
    public LayerMask groundLayer;

    Animator animator;
    Rigidbody2D rb;
    private Collider2D[] colliders;
    SpriteRenderer spriteRenderer;

    public bool isGrounded;
    public bool isJumping = false;
    public bool isWalking;
    private bool isAttacking = false;
    private bool isSliding = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        colliders = GetComponentsInChildren<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        tempMaxspeed = maxSpeed;
    }

    void Update()
    {
        bool isGrounded = false;

        foreach (Collider2D collider in colliders)
        {
            isGrounded |= Physics2D.IsTouchingLayers(collider, groundLayer);

            if (isGrounded)
            {
                // 캐릭터가 땅과 충돌하는 경우의 작업 수행
                isJumping = false;             
                break;
            }
        }

        float moveInput = Input.GetAxis("Horizontal");

        //바라보는 방향 저장
        if (moveInput > 0)
        {
            tempMoveInput = 1f;
            ViewDirection= 1f;
            isWalking = true;
        }
        else if (moveInput < 0)
        {
            tempMoveInput = -1f;
            ViewDirection= -1f;
            isWalking = true;
        }
        else
        {
            tempMoveInput = 0;
            isWalking = false;
        }

        animator.SetBool("IsWalking", isWalking);

        //움직이는 힘 작용
        if(!isSliding)
            rb.AddForce(Vector2.right * tempMoveInput * 1000, ForceMode2D.Impulse);


        if (rb.velocity.x > maxSpeed)
        {
            rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
        }
        else if (rb.velocity.x < maxSpeed*(-1))
        {
            rb.velocity = new Vector2(maxSpeed*(-1), rb.velocity.y);      
        }



        //슬라이딩 액션

         if (Input.GetKeyDown(KeyCode.Z) && !isSliding && isGrounded && !isAttacking)
        {
            isSliding = true;
            animator.SetTrigger("Sliding");


            //수정 필요
            maxSpeed = slideSpeed;
            rb.AddForce(new Vector2(ViewDirection , 0) * slideSpeed, ForceMode2D.Impulse);
        }
    

        //공격 액션
        if (Input.GetKeyDown(KeyCode.X) && !isAttacking && !isSliding)
        {
            isAttacking = true;
            maxSpeed = 5f;
            animator.SetTrigger("Attack"); // Attack 트리거를 설정하여 애니메이션 실행
        }


        //점프 액션
        if (Input.GetKeyDown(KeyCode.C) && !isJumping && !isSliding && isGrounded && jumpCount == 0)
        {
            isJumping = true;
            animator.SetBool("IsJumping",true);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // 점프 힘을 가하여 점프
        }

        //더블 점프
        if (Input.GetKeyUp(KeyCode.C) && !isSliding && jumpCount == 0)
        {
            jumpCount++;
        }

        if (Input.GetKeyDown(KeyCode.C) && !isSliding && jumpCount == 1)
        {
            jumpCount++;

            rb.velocity = Vector3.zero;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // 점프 힘을 가하여 점프
        }
    }

    void FixedUpdate() 
    {
        // 이동 방향에 따라 캐릭터를 뒤집습니다.
        if (!isAttacking)
        {
            if (rb.velocity.x > 0.1f)
            {
                transform.localScale = new Vector3(8f, 8f, 8f);
            }
            else if (rb.velocity.x < -0.1f)
            {
                transform.localScale = new Vector3(-8f, 8f, 8f);
            }
        }

    }

    //콜라이더 진입 시
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isJumping = false;
            animator.SetBool("IsJumping",false);

            jumpCount = 0;
        }
    }

    //콜라이더 탈출 시
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    //트리거 콜라이더 진입 (보스 무기 콜라이더)
    void OnTriggerEnter2D(Collider2D Collider)
    {

        if (Collider.gameObject.CompareTag("Enemy"))
        {
            OnDamaged(Collider.transform.position);
        }
    }


    //어택 에니메이션 끝남
    public void AttackAnimationFinished()
    {
        isAttacking = false;
        maxSpeed = tempMaxspeed;
    }

    //슬라이딩 애니메이션 끝남
    public void SlideAnimationFinished()
    {
        isSliding = false;
        maxSpeed = tempMaxspeed;
    }

    //데미지 받는 함수
    void OnDamaged(Vector2 targetPos)
    {  
        gameObject.layer = 9;

        spriteRenderer.color = new Color(8, 8, 8, 0.4f);

        int dirc = transform.position.x-targetPos.x > 0 ? 1 : -1;
        rb.AddForce(new Vector2(dirc,1) * 7 , ForceMode2D.Impulse);

        Invoke("OffDamaged" , 1);
    }

    //상태 초기화
    void OffDamaged()
    {  
        gameObject.layer = 10;

        spriteRenderer.color = new Color(8, 8, 8, 8);
    }
}

