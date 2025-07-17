using UnityEngine;
using System.Collections.Generic;
using Tuleeeeee.CoreComponet;

using static SoundEffect;

[System.Serializable]
public class Sound
{
    public SoundEffectType SoundEffectType;
    public AudioSource AudioSource;
}

public class SoundEffect : CoreComponent
{
    public enum SoundEffectType
    {
        Move,
        Jump,
        Land,
        WallJump
    }

    [SerializeField] private List<Sound> soundEffects = new();

    private void PlaySoundEffect(SoundEffectType effectType)
    {
        foreach (var dustEffect in soundEffects)
        {
            if (dustEffect.SoundEffectType == effectType)
            {
                dustEffect.AudioSource.Play();
                break; // Optional: Stops after the first matching effect is found
            }
        }
    }

    // Simplified public methods using the helper method
    public void MoveSoundFX() => PlaySoundEffect(SoundEffectType.Move);
    public void JumpSoundFX() => PlaySoundEffect(SoundEffectType.Jump);
    public void LandSoundFX() => PlaySoundEffect(SoundEffectType.Land);
    public void WallJumpSoundFX() => PlaySoundEffect(SoundEffectType.WallJump);
}