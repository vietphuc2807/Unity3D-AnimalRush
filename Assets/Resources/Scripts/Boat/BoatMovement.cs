using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class BoatMovement : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private BoatData boatData;

    [SerializeField] private ParticleSystem waterWake;
    [SerializeField] private Collider boatTrigger;

    [SerializeField] private TrailRenderer leftTrail;
    [SerializeField] private TrailRenderer rightTrail;

    private Rigidbody rb;
    private float collisionTimer;

    private bool canControl;

    private float currentSpeed;
    private Vector3 velocity;

    public float TurnInput { get; private set; }

    private bool isDrifting;
    private bool driftKey;
    private float driftFactor;
    
    
    #region Awake
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    #endregion

    #region SetControl
    public void SetControl(bool value)
    {
        canControl = value;

        if (canControl)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (boatTrigger != null) boatTrigger.enabled = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (boatTrigger != null) boatTrigger.enabled = true;
        }
    }
    #endregion

    #region Update
    private void Update()
    {
        if (canControl)
        {
            HandleInput();

            HandleDriftState();
            HandleSteering();
        }

        HandleWaterWake();

        if (boostTimer > 0)
        {
            boostTimer -= Time.deltaTime;

            if (boostTimer <= 0) 
            {
                boostMultiplier = 1f;
                waterSpellEffect.SetActive(false);
            }
        }

        if (transform.position.y < -10f)
        {
            GameStats.Instance.AddDeath();
            RespawnBlink.wasRespawned = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    #endregion

    #region FixedUpdate
    private void FixedUpdate()
    {
        if (!canControl) return;

        HandleMovement();
    }
    #endregion

    #region HandleInput
    private void HandleInput()
    {
        float turn = Input.GetAxis("Horizontal"); // PC

#if UNITY_ANDROID || UNITY_IOS
        float tilt = Input.acceleration.x;

        if (Mathf.Abs(tilt) < 0.05f) tilt = 0;

        tilt = Mathf.Clamp(tilt, -1f, 1f);

        turn = tilt * 2f;
#endif

        TurnInput = Mathf.Lerp(TurnInput, turn, Time.deltaTime * 5f);


        bool driftInput = Input.GetKey(KeyCode.LeftShift); 

#if UNITY_ANDROID || UNITY_IOS
        driftInput = false;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId) &&
                touch.position.x > Screen.width / 2)
            {
                driftInput = true;
            }
        }
#endif

        driftKey = driftInput;
    }
    #endregion

    #region HandleDriftState
    private void HandleDriftState()
    {
        if (!isDrifting && driftKey && Mathf.Abs(TurnInput) > 0.1f)
        {
            isDrifting = true;
        }

        if (isDrifting && (!driftKey || Mathf.Abs(TurnInput) < 0.1f))
        {
            isDrifting = false;
        }

        bool driftActive = isDrifting && currentSpeed > 10f;

        leftTrail.emitting = driftActive;
        rightTrail.emitting = driftActive;
    }
    #endregion

    #region HandleMovement
    private void HandleMovement()
    {
        if (collisionTimer > 0)
        {
            collisionTimer -= Time.deltaTime;
            return;
        }

        float targetSpeed = boatData.MoveSpeed * boostMultiplier;

        if (Mathf.Abs(TurnInput) > 0.1f)
            targetSpeed *= boatData.TurnSpeedPenalty;

        float targetDrift = isDrifting ? 1f : 0f;
        driftFactor = Mathf.Lerp(driftFactor, targetDrift, 3f * Time.deltaTime);

        float driftSpeed = targetSpeed * boatData.DriftSpeedPenalty;
        targetSpeed = Mathf.Lerp(targetSpeed, driftSpeed, driftFactor);

        currentSpeed = Mathf.Lerp(
            currentSpeed,
            targetSpeed,
            boatData.Acceleration * Time.deltaTime
        );

        Vector3 forwardVel = transform.forward * currentSpeed;

        Vector3 sideVel = transform.right * Vector3.Dot(rb.linearVelocity, transform.right);

        float grip = Mathf.Lerp(1f, boatData.DriftGrip, driftFactor);

        Vector3 finalVelocity = forwardVel + sideVel * grip;

        finalVelocity.y = rb.linearVelocity.y;

        rb.linearVelocity = finalVelocity;

        rb.AddForce(Vector3.down * 60f, ForceMode.Acceleration);
    }
    #endregion

    #region HandleSteering
    private void HandleSteering()
    {
        float turnSpeed = boatData.TurnSpeed;

        if (isDrifting)
            turnSpeed *= boatData.DriftMultiplier;

        float turn = TurnInput * turnSpeed * Time.deltaTime;

        rb.MoveRotation(rb.rotation * Quaternion.Euler(0, turn, 0));
    }
    #endregion

    #region IsMoving
    public bool IsMoving()
    {
        return currentSpeed > 0.5f;
    }
    #endregion

    #region HandleWaterWake
    private void HandleWaterWake()
    {
         if (!canControl)
        {
            if (waterWake.isPlaying)
                waterWake.Stop();
            return;
        }

        if (IsMoving())
        {
            if (!waterWake.isPlaying)
                waterWake.Play();
        }
        else
        {
            if (waterWake.isPlaying)
                waterWake.Stop();
        }
    }
    #endregion

    #region OnCollisionEnter
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("KnifeObstacle"))
        {
            SoundManager.Instance.PlaySFX("hurt");

            SetControl(false);

            if (DeathUI.Instance != null)
            {
                GameStats.Instance.AddDeath();
                DeathUI.Instance.ShowDeath();
            }

            return;
        }


        if (collision.gameObject.CompareTag("Obstacle"))
        {
            SoundManager.Instance.PlaySFX("hit");

            collisionTimer = 0.15f;

            Vector3 normal = collision.contacts[0].normal;

            normal.y = 0;
            normal.Normalize();

            Vector3 pushBack = normal * 3f;

            rb.linearVelocity *= 0.2f;

            rb.AddForce(pushBack, ForceMode.Impulse);

            currentSpeed *= 0.3f;
        }
    }
    #endregion

    #region OnTriggerEnter
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            SetControl(false);

            if (WinUI.Instance != null)
            {
                WinUI.Instance.ShowWin();
            }
        }
    }
    #endregion

    private float boostTimer;
    private float boostMultiplier = 1f;
    [SerializeField] private GameObject waterSpellEffect;
    [SerializeField] private Animator waterSpellAnimator;

    #region StartBoost
    public void StartBoost(BoostData boostData)
    {
        GameStats.Instance.AddBoost();

        boostTimer = boostData.BoostTime;

        boostMultiplier = boostData.BoostMultiplier;

        waterSpellEffect.SetActive(true);

        if (waterSpellAnimator != null) waterSpellAnimator.Play("WaterSpellStart", 0, 0f);
    }
    #endregion
}