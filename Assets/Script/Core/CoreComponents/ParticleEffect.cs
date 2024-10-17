
using System.Collections.Generic;
using UnityEngine;
using static ParticleEffect;

[System.Serializable]
public class DustEffect
{
    public DustEffectType DustEffectType;
    public ParticleSystem Effect;
}
public class ParticleEffect : CoreComponent
{
    public enum DustEffectType
    {
        Move,
        Jump,
        Land,
        WallJump
    }
    [SerializeField]
    private List<DustEffect> dustEffects = new List<DustEffect>();


    private void PlayDustEffect(DustEffectType effectType)
    {
        foreach (var dustEffect in dustEffects)
        {
            if (dustEffect.DustEffectType == effectType)
            {
                dustEffect.Effect.Play();
                break;  // Optional: Stops after the first matching effect is found
            }
        }
    }

    // Simplified public methods using the helper method
    public void DustMove() => PlayDustEffect(DustEffectType.Move);
    public void DustJump() => PlayDustEffect(DustEffectType.Jump);
    public void DustLand() => PlayDustEffect(DustEffectType.Land);
    public void DustWallJump() => PlayDustEffect(DustEffectType.WallJump);

}
