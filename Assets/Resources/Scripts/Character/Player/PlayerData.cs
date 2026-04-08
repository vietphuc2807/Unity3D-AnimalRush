using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "GameData/Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Jumping")]
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravity = -9.81f;

    [Header("Advanced")]
    [SerializeField] private float groundStickForce = -2f;

    public float MoveSpeed => moveSpeed;
    public float JumpHeight => jumpHeight;
    public float Gravity => gravity;
    public float GroundStickForce => groundStickForce;
}
