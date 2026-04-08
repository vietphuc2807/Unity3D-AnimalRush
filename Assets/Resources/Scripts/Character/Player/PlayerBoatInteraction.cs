using UnityEngine;

public class PlayerBoatInteraction : MonoBehaviour
{
    private Animator anim;
    private PlayerMovement movement;
    private BoatMovement boatMovement;

    [SerializeField] private Transform paddleHoldPoint;
    [SerializeField] private GameObject paddlePrefab;

    private GameObject currentPaddle;
    private ThirdPersonCamera cam;
    private bool isOnBoat = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
        cam = FindFirstObjectByType<ThirdPersonCamera>();
    }

    private void Update()
    {
        if (!isOnBoat) return;

        if (boatMovement != null)
        {
            anim.SetBool("IsPaddling", boatMovement.IsMoving());
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ExitBoat();
        }
    }

    public void EnterBoat(Transform seatPoint)
    {
        SoundManager.Instance.PlaySFX("boost");

        if (cam != null) cam.SetBoatCamera(true);

        transform.position = seatPoint.position;
        transform.rotation = Quaternion.Euler(0, seatPoint.eulerAngles.y, 0);

        movement.enabled = false;

        anim.SetFloat("Speed", 0);
        anim.SetBool("IsJumping", false);
        anim.SetBool("IsOnBoat", true);

        isOnBoat = true;

        boatMovement = seatPoint.GetComponentInParent<BoatMovement>();
        if (boatMovement != null)
        {
            boatMovement.SetControl(true);
        }

        transform.SetParent(seatPoint);

        if (currentPaddle == null)
        {
            currentPaddle = Instantiate(paddlePrefab, paddleHoldPoint);
            currentPaddle.transform.localPosition = Vector3.zero;
            currentPaddle.transform.localRotation = Quaternion.identity;
            currentPaddle.transform.localScale = Vector3.one;
        }
    }

    private void ExitBoat()
    {
        if (cam != null) cam.SetBoatCamera(false);

        movement.enabled = true;

        anim.SetBool("IsOnBoat", false);
        anim.SetBool("IsPaddling", false);

        isOnBoat = false;

        if (boatMovement != null)
        {
            boatMovement.SetControl(false);
        }

        transform.SetParent(null);

        transform.position += transform.right * 1.5f;

        if (currentPaddle != null)
        {
            Destroy(currentPaddle);
            currentPaddle = null;
        }
    }
}