using System.Collections;
using UnityEngine;

public class WarriorBoss : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public float attackRange = 10f;
    public float moveSpeed = 3f;

    public float attackCooldown = 3f;

    public bool isWalking;
    public bool isAttacking = false;
    private bool isAttackingCoroutine = false;

    public string[] animationNames = { "Attack1", "Attack2", "Attack3", "Attack4" };
    

    private Transform player; // 플레이어의 위치를 참조하기 위한 변수

    Animator animator;

    private void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform; // 플레이어 태그를 가진 오브젝트 찾기
        animator = GetComponent<Animator>();

    }

    private void Update()
    {
        //플레이어 위치에 따라 좌우변경
        if (transform.position.x > player.position.x)
        {
            transform.localScale = new Vector3(10f, 10f, 10f);
        } 
        else
        {
            transform.localScale = new Vector3(-10f, 10f, 10f);
        }

        if (!isAttacking && Vector2.Distance(transform.position, player.position) > attackRange)
        {
            // 플레이어 쪽으로 이동하기
            Vector2 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);

            isWalking = true;
            animator.SetBool("IsWalking", isWalking);
            
        }
        else if (!isAttackingCoroutine && Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            StartCoroutine(AttackSequence());
        }

        else if (Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            isWalking = false;
            animator.SetBool("IsWalking", isWalking);
        }
    }

    void Attack()
    {
        isWalking = false;
        isAttacking = true;
        animator.SetBool("IsWalking", isWalking);

        int randomIndex = Random.Range(0, animationNames.Length);
        string randomAnimation = animationNames[randomIndex];
        animator.SetTrigger(randomAnimation);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // 보스가 죽었을 때의 동작
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // 보스가 죽었을 때의 동작
        // 예를 들어, 애니메이션을 재생하거나 보상 지급 등을 수행합니다.
        Destroy(gameObject); // 보스 오브젝트 삭제
    }



    //애니메이션 함수

    public void AttackAnimationFinished()
    {
        isAttacking = false;
    }




    // 보스가 일정 시간마다 공격할 수 있도록 하는 코루틴
    IEnumerator AttackSequence()
    {
        isAttackingCoroutine = true;

        Attack();

        yield return new WaitForSeconds(attackCooldown);

        isAttackingCoroutine = false;
    }
}
