using UnityEngine;

[CreateAssetMenu(fileName = "BoostData", menuName = "GameData/Boost Data")]
public class BoostData : ScriptableObject
{
    [Header("Boost Settings")]
    [SerializeField] private float boostMultiplier = 2f;
    [SerializeField] private float boostTime = 2f;

    public float BoostMultiplier => boostMultiplier;
    public float BoostTime => boostTime;
}
