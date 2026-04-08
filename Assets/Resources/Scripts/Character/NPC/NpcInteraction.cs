using UnityEngine;

public class NpcInteraction : MonoBehaviour
{
    [SerializeField] private GameObject interactionUI;
    [SerializeField] private GameObject boatSelectUI;

    private bool playerInRange = false;

    private void Start()
    {
        interactionUI.SetActive(true);
        boatSelectUI.SetActive(true);
        
        interactionUI.SetActive(false);
        boatSelectUI.SetActive(false);
    }

    private void Update()
    {
        if (BoatSelectUI.isOpen) return;

        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            interactionUI.SetActive(false);
            boatSelectUI.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (BoatSelectUI.isOpen) return;

        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            interactionUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            interactionUI.SetActive(false);
        }
    }
}