using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public ParticleSystem dust;
    void CreateDust()
    {
        dust.Play();
    }
}
