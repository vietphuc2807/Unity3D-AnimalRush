using UnityEngine;

public class AutoCarMove : MonoBehaviour
{
    [SerializeField] private Transform targetPoint;
    [SerializeField] private float speed = 5f;

    private bool moveToTarget = false;

    void Update()
    {
        if (!moveToTarget) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPoint.position,
            speed * Time.deltaTime
        );

        transform.LookAt(targetPoint);

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            Debug.Log("Car reached target point");
            gameObject.SetActive(false);
        }
    }

    public void StartMove()
    {
        moveToTarget = true;

        SoundManager.Instance.PlaySFX("carhorn");
    }
}