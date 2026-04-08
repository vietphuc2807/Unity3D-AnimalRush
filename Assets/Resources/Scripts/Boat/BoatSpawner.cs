using UnityEngine;

public class BoatSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private BoatSelectUI popupSelectBoat;

    private GameObject selectedBoat;
    private bool hasSpawned = false;

    public void SelectBoat(GameObject boatPrefab)
    {
        selectedBoat = boatPrefab;
    }

    public void SpawnBoat()
    {
        if (hasSpawned) return;

        if (selectedBoat == null)
        {
            Debug.Log("No boat selected");
            return;
        }

        popupSelectBoat.ClosePopup();

        Instantiate(selectedBoat, spawnPoint.position, spawnPoint.rotation);

        hasSpawned = true;
    }
}