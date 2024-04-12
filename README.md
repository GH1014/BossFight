## BossFight

**프로젝트 개요**

BossFight는 플레이어가 보스와의 전투를 펼치는 액션 게임입니다.

방향키로 캐릭터 이동, Z키를 이용하여 슬라이딩, X키를 이용하여 공격, C키를 이용하여 점프할 수 있습니다.

게임의 목표는 플레이어가 보스와의 전투를 통해 승리하는 것입니다. 

보스는 일정 시간이 지날 때마다 4가지 다른 패턴을 이용하여 플레이어를 공격합니다. 

--------------------------------------------------------

**사용한 기술 정보**

C++, Unity

--------------------------------------------------------

**구동 스크린샷**

![image](https://github.com/GH1014/BossFight/assets/95550744/88dee8c6-5f51-4a46-9fe2-f641426e2262)
![image](https://github.com/GH1014/BossFight/assets/95550744/a493034e-5db5-4ba2-a9f6-2da52a3da71c)
![image](https://github.com/GH1014/BossFight/assets/95550744/739d8ceb-1edf-4379-8a4a-3ee67928eed9)
![image](https://github.com/GH1014/BossFight/assets/95550744/bc7f5501-a49d-426e-88ff-87f959b6de0e)


--------------------------------------------------------

**주요 코드**

플레이어 이동 관련 코드입니다.

```c++
//슬라이딩 액션
 if (Input.GetKeyDown(KeyCode.Z) && !isSliding && isGrounded && !isAttacking)
{
    isSliding = true;
    animator.SetTrigger("Sliding");
    
    maxSpeed = slideSpeed;
    rb.AddForce(new Vector2(ViewDirection , 0) * slideSpeed, ForceMode2D.Impulse);
}
    

//점프 액션
if (Input.GetKeyDown(KeyCode.C) && !isJumping && !isSliding && isGrounded && jumpCount == 0)
{
    isJumping = true;
    animator.SetBool("IsJumping",true);
    rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
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
    rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
}
```

보스 공격 관련 코드입니다.

```c++
//플레이어 방향으로 이동
if (!isAttacking && Vector2.Distance(transform.position, player.position) > attackRange)
{
    Vector2 direction = (player.position - transform.position).normalized;
    transform.Translate(direction * moveSpeed * Time.deltaTime);

    isWalking = true;
    animator.SetBool("IsWalking", isWalking);
    
}

//공격 사거리에 들어왔으면 코루틴 실행
else if (!isAttackingCoroutine && Vector2.Distance(transform.position, player.position) <= attackRange)
{
    StartCoroutine(AttackSequence());
}

//걷는 에니메이션 중지
else if (Vector2.Distance(transform.position, player.position) <= attackRange)
{
    isWalking = false;
    animator.SetBool("IsWalking", isWalking);
}

//보스가 일정 시간마다 공격할 수 있도록 하는 코루틴
IEnumerator AttackSequence()
{
    isAttackingCoroutine = true;

    Attack();

    yield return new WaitForSeconds(attackCooldown);

    isAttackingCoroutine = false;
}

//보스 공격 함수
void Attack()
{
    isWalking = false;
    isAttacking = true;
    animator.SetBool("IsWalking", isWalking);

    int randomIndex = Random.Range(0, animationNames.Length);
    string randomAnimation = animationNames[randomIndex];
    animator.SetTrigger(randomAnimation);
}

```

--------------------------------------------------------
