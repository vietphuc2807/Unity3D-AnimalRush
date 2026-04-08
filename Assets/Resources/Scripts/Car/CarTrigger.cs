using UnityEngine;

public class CarTrigger : MonoBehaviour
{
    [SerializeField] private AutoCarMove car;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered car trigger");
            car.StartMove();
        }
    }
}