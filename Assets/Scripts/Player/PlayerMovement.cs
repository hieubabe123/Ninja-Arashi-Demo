using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("------------------Player Data------------------")]
    public PlayerScriptableObject playerData;
    public DashSkillScriptableObject currentDashData;
    public ThrowShurikenScriptableObject currentThrowShurikenData;
    public HealAndShieldScriptableObject currenthealAndShieldData;
    public CamouflageScriptableObject currentCamouflageData;


    [SerializeField] private PlayerInputAction playerInputAction;
    private Rigidbody2D rb;
    private CircleCollider2D circleCollider2D;
    private CapsuleCollider2D capsuleCollider2D;
    [SerializeField] private Transform shurikenSpawnPos;
    [SerializeField] private GameObject deadPlayerPrefab;
    [SerializeField] private GameObject camouFlagePrefab;
    [SerializeField] private SpriteRenderer camouflageSprite;
    [SerializeField] private List<GameObject> rootPlayerPrefabs = new List<GameObject>();



    [Header("---------------Stats---------------")]
    public float currentMoveSpeed;
    public float currentMoveSpeedWhenCamouflaging;
    public float currentJumpForce;
    public float currentCooldownShuriken;
    public float currentCooldownCamouflage;
    public float currentCooldownDashKill;
    private int currentDiguiseDuration;
    private int currentLifeCount;
    private float originalGravityScale;

    //Seperate private stat in PlayerStats script and child stat in another script
    public int CurrentLifeCount
    {
        get { return currentLifeCount; }
        set
        {
            if (currentLifeCount != value)
            {
                currentLifeCount = value;
                if (UIForAll.instance != null)
                {
                    UIForAll.instance.currentLifeCountDisplay.text = CurrentLifeCount.ToString();
                }
            }
        }
    }

    private float shieldDuration;


    [Header("---------------- Check State of Player --------------")]
    public bool isMoving;
    public bool isGround;
    public bool isWalling;
    private bool canJumping;
    public bool canAttack;
    public bool isDashing;
    public bool isCamouflage;
    public bool isKilled = false;
    public bool haveShield;
    public float jumpStatus;
    public float throwStatus;
    public float lastMoveDirX;


    [Header("---------------- Check How Player Dead --------------")]
    public bool isDeadByEnemies;
    public bool isDeadByPoison;
    public bool isDeadByElectric;
    public bool isDeadByFire;

    [Header("---------------- Effect --------------")]
    public ParticleSystem jumpGroundEffect;
    public ParticleSystem doubleJumpEffect;
    public ParticleSystem dashEffect;
    public ParticleSystem camouflageEffect;
    public ParticleSystem shieldEffect;
    public ParticleSystem healEffect;
    public ParticleSystem gemCollectEffect;
    public ParticleSystem coinCollectEffect;
    public GameObject playerEffectPos;

    private static int doubleJump = 2;
    private int currentJump;
    private float dashDistance = 15f;
    private float dashDuration = 0.2f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        playerInputAction = GetComponent<PlayerInputAction>();


        currentDashData = DataManager.instance.currentDashData;
        currentCamouflageData = DataManager.instance.currentCamouflageData;
        currentThrowShurikenData = DataManager.instance.currentThrowShurikenData;
        currenthealAndShieldData = DataManager.instance.currentHealAndShieldData;

        currentMoveSpeed = playerData.MoveSpeed;
        currentJumpForce = playerData.JumpForce;
        currentMoveSpeedWhenCamouflaging = playerData.MoveSpeedWhenCamouflaging;

        currentCooldownShuriken = DataManager.instance.currentThrowShurikenData.Cooldown;
        currentCooldownDashKill = DataManager.instance.currentDashData.Cooldown;
        currentCooldownCamouflage = DataManager.instance.currentCamouflageData.Cooldown;
        currentDiguiseDuration = DataManager.instance.currentCamouflageData.DiguiseDuration;
        shieldDuration = DataManager.instance.currentHealAndShieldData.ShieldDuration;
        CurrentLifeCount = DataManager.instance.currentHealAndShieldData.LifeCount;

        camouflageSprite.sprite = DataManager.instance.camouflageSprite;
    }

    private void Start()
    {
        isDeadByEnemies = false;
        isDeadByFire = false;
        isDeadByElectric = false;
        isDeadByPoison = false;

        haveShield = false;
        isCamouflage = false;
        lastMoveDirX = 1;
        originalGravityScale = rb.gravityScale;

        UIForAll.instance.currentLifeCountDisplay.text = CurrentLifeCount.ToString();
    }

    private void OnDisable()
    {
        isDeadByEnemies = false;
        isDeadByFire = false;
        isDeadByElectric = false;
        isDeadByPoison = false;
    }


    private void Update()
    {
        if (currentJump <= 0)
        {
            canJumping = false;
        }
        currentCooldownCamouflage -= Time.deltaTime;
        currentCooldownShuriken -= Time.deltaTime;
        currentCooldownDashKill -= Time.deltaTime;
        if (currentCooldownShuriken > 0)
        {
            canAttack = false;
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        Move();
        FlipSprite();
        jumpStatus = rb.velocity.y;
    }

    #region Move, Jump and Flip Sprite
    public Vector2 MoveDirection()
    {
        return new Vector2(playerInputAction.GetNormalizedDirection().x, 0);
    }

    public void Move()
    {
        if (isDashing)
        {
            return;
        }
        Vector2 moveDir = new Vector2(playerInputAction.GetNormalizedDirection().x, 0);
        float moveDistance;
        if (!isCamouflage)
        {
            moveDistance = currentMoveSpeed * Time.deltaTime;
        }
        else
        {
            moveDistance = currentMoveSpeedWhenCamouflaging * Time.deltaTime;
        }
        rb.velocity = new Vector2(moveDistance * moveDir.x, rb.velocity.y);
        isMoving = Mathf.Abs(rb.velocity.x) > 0;
        if (moveDir.x != 0)
        {
            lastMoveDirX = moveDir.x;
        }
    }

    private void FlipSprite()
    {
        Vector2 playerScale = new Vector2(Mathf.Sign(lastMoveDirX), 1f);
        this.transform.localScale = playerScale;
    }

    public void Jump()
    {
        if (!canJumping)
        {
            return;
        }
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(currentJumpForce * Vector3.up, ForceMode2D.Impulse);
        doubleJumpEffect.Emit(8);
        currentJump--;
        AudioManager.instance.PlayListSFX(AudioManager.instance.playerJumpSFX);
    }
    #endregion

    #region Attack
    public void Fire()
    {
        currentCooldownShuriken = DataManager.instance.currentThrowShurikenData.Cooldown;
        canAttack = true;

        GameObject shuriken = ObjectPooling.instance.GetObjectFromPool();
        if (shuriken != null)
        {
            shuriken.transform.position = shurikenSpawnPos.position;
            shuriken.SetActive(true);
            AudioManager.instance.PlayListSFX(AudioManager.instance.shurikenThrowSFX);
        }
    }

    public void DashKill()
    {
        if (isDashing && currentCooldownDashKill > 0)
        {
            return;
        }
        isDashing = true;

        originalGravityScale = rb.gravityScale;
        rb.gravityScale = 0;

        capsuleCollider2D.isTrigger = true;
        circleCollider2D.isTrigger = true;
        rb.velocity = Vector2.zero;
        float dashVelocity = dashDistance / dashDuration;
        rb.velocity = new Vector2(lastMoveDirX * dashVelocity, 0);
        this.gameObject.layer = LayerMask.NameToLayer("PlayerDashing");
        AudioManager.instance.PlaySFX(AudioManager.instance.playerDashingSFX);


        currentCooldownDashKill = DataManager.instance.currentDashData.Cooldown;
        StartCoroutine(DashCoroutine());
    }
    private IEnumerator DashCoroutine()
    {
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        rb.gravityScale = originalGravityScale;
        capsuleCollider2D.isTrigger = false;
        circleCollider2D.isTrigger = false;
        this.gameObject.layer = LayerMask.NameToLayer("Player");


        AudioManager.instance.PlayListSFX(AudioManager.instance.playerDashDoneSFX);

    }
    #endregion

    #region Camouflage
    public void Camouflage()
    {
        if (currentCooldownCamouflage > 0 && isCamouflage)
        {
            return;
        }
        camouFlagePrefab.SetActive(true);
        AudioManager.instance.PlaySFX(AudioManager.instance.playerCamouflageSFX);
        foreach (var gameObj in rootPlayerPrefabs)
        {
            gameObj.SetActive(false);
        }
        isCamouflage = true;
        Instantiate(camouflageEffect, this.transform.position, Quaternion.identity);
        this.gameObject.layer = LayerMask.NameToLayer("Wood");

        currentCooldownCamouflage = DataManager.instance.currentCamouflageData.Cooldown;

        StartCoroutine(CamouflageCoroutine());
    }

    private IEnumerator CamouflageCoroutine()
    {
        yield return new WaitForSeconds(currentDiguiseDuration);
        TurnOffCamouflage();
    }

    public void TurnOffCamouflage()
    {
        isCamouflage = false;
        camouFlagePrefab.SetActive(false);
        foreach (var gameObj in rootPlayerPrefabs)
        {
            gameObj.SetActive(true);
        }
        this.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    #endregion


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
            isWalling = false;
            canJumping = true;
            currentJump = doubleJump;
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            isWalling = true;
            canJumping = true;
            currentJump = doubleJump;
        }
        if (collision.gameObject.CompareTag("Trap"))
        {
            if (!haveShield)
            {
                Kill();
                AudioManager.instance.PlayListSFX(AudioManager.instance.playerDeadByEnemySFX);
            }
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && isDashing)
        {
            if (collision.gameObject.TryGetComponent(out EnemyStats enemy))
            {
                enemy.TakeDamage(100);
            }
        }
        if (collision.gameObject.CompareTag("Wood") && isDashing)
        {
            if (collision.gameObject.TryGetComponent(out ObjectDestroy obj))
            {
                obj.DestroyObject();
            }
        }
        if (collision.CompareTag("WinPos"))
        {
            GameManager.instance.GameWin();
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = false;
            jumpGroundEffect.Emit(1);
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            isWalling = false;
        }
    }

    public void Kill()
    {
        CurrentLifeCount = CurrentLifeCount - 1;
        deadPlayerPrefab.SetActive(true);
        deadPlayerPrefab.transform.position = this.gameObject.transform.position;

        if (currentLifeCount > 0)
        {
            CheckpointManager.instance.RespawnPlayer(this.gameObject, deadPlayerPrefab, 2f);

        }
        else
        {
            GameManager.instance.GameOver();
        }
        this.gameObject.SetActive(false);
    }

    #region Collect Items

    public void RestoreHealth(int amount)
    {
        if (healEffect != null)
        {
            Instantiate(healEffect, playerEffectPos.transform.position, Quaternion.identity);
        }
        CurrentLifeCount += amount;
    }

    public void TakeShield()
    {
        if (shieldEffect != null)
        {
            shieldEffect.Play();

        }
        haveShield = true;
        StartCoroutine(WaitToUnshield());

    }

    private IEnumerator WaitToUnshield()
    {
        yield return new WaitForSeconds(shieldDuration);
        haveShield = false;
        shieldEffect.Stop();
    }

    public void GetMoney(int amount)
    {
        if (coinCollectEffect != null)
        {
            Instantiate(coinCollectEffect, playerEffectPos.transform.position, Quaternion.identity);

        }
        DataManager.instance.CurrentMoney += amount;
    }

    public void GetGem(int amount)
    {
        if (gemCollectEffect != null)
        {
            Instantiate(gemCollectEffect, playerEffectPos.transform.position, Quaternion.identity);

        }
        DataManager.instance.CurrentGem += amount;
    }

    public void GetScrollPaper(int amount)
    {
        DataManager.instance.CurrentScrollPaper += amount;
    }

    #endregion
    public bool CheckWalling()
    {
        return isWalling;
    }
}
