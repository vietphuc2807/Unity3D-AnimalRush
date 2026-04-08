using UnityEngine;

public class BoatSeat : MonoBehaviour
{
    public Transform seatPoint;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerBoatInteraction player = other.GetComponent<PlayerBoatInteraction>();

            player.EnterBoat(seatPoint);
        }
    }
}