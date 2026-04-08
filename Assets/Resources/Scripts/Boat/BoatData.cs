using UnityEngine;

[CreateAssetMenu(fileName = "BoatData", menuName = "GameData/Boat Data")]
public class BoatData : ScriptableObject
{
    [Header("Speed")]
    [SerializeField] private float moveSpeed = 25f;
    [SerializeField] private float acceleration = 8f;

    [Header("Steering")]
    [SerializeField] private float turnSpeed = 120f;

    [Header("Drift")]
    [SerializeField] private float driftGrip = 0.92f;
    [SerializeField] private float driftMultiplier = 1.7f;
    [SerializeField] private float turnSpeedPenalty = 0.5f;
    [SerializeField] private float driftSpeedPenalty = 0.8f;
    [SerializeField] private float driftSlowRate = 3f;

    public float MoveSpeed => moveSpeed;
    public float Acceleration => acceleration;
    public float TurnSpeed => turnSpeed;
    public float DriftGrip => driftGrip;
    public float DriftMultiplier => driftMultiplier;
    public float TurnSpeedPenalty => turnSpeedPenalty;
    public float DriftSpeedPenalty => driftSpeedPenalty;
    public float DriftSlowRate => driftSlowRate;
}