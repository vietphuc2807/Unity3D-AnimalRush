using UnityEngine;

public class BoostPad : MonoBehaviour
{
    [SerializeField] private BoostData boostData;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        BoatMovement boat = other.GetComponentInParent<BoatMovement>();

        SoundManager.Instance.PlaySFX("splat");

        boat.StartBoost(boostData);
    }
}
