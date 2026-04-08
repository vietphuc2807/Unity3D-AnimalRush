using UnityEngine;

public class FootstepDust : MonoBehaviour
{
    public ParticleSystem dustPrefab;

    public Transform leftFoot;
    public Transform rightFoot;

    public void LeftStep()
    {
        ParticleSystem dust = Instantiate(dustPrefab, leftFoot.position, Quaternion.identity);
        Destroy(dust.gameObject, 1f);
    }

    public void RightStep()
    {
        ParticleSystem dust = Instantiate(dustPrefab, rightFoot.position, Quaternion.identity);
        Destroy(dust.gameObject, 1f);
    }
}
