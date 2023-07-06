using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using URPGlitch.Runtime.DigitalGlitch;
using URPGlitch.Runtime.AnalogGlitch;

public class GlitchEffect : MonoBehaviour
{
    bool _isComplete = false;

    [SerializeField]
    Volume volume;

    [SerializeField]
    VolumeProfile profile;

    [SerializeField]
    AudioClip turnOnClip, turnOffClip;

    DigitalGlitchVolume digitalGlitchVolume;
    AnalogGlitchVolume analogGlitchVolume;

    
    private void Awake() {
        profile = volume.profile;

        profile.TryGet<DigitalGlitchVolume>(out digitalGlitchVolume);
        profile.TryGet<AnalogGlitchVolume>(out analogGlitchVolume);
    }

    float glitch_amount = 0.0f;
    [SerializeField] float glitch_speed = 1.0f;

    [ContextMenu("turnOn")]
    public void TurnOn(){
        SoundManager.Instance.SFXPlay(turnOnClip);
        StartCoroutine(TurnOnCo());
    }
    [ContextMenu("TurnOff")]
    public void TurnOff(){
        SoundManager.Instance.SFXPlay(turnOffClip);
        StartCoroutine(TurnOffCo());
    }

    IEnumerator TurnOffCo()
    {
        glitch_amount = 1.0f;
        while (true)
        {
            glitch_amount -= Time.deltaTime * glitch_speed;

            digitalGlitchVolume.intensity.Override(glitch_amount);
            analogGlitchVolume.scanLineJitter.Override(glitch_amount);
            analogGlitchVolume.horizontalShake.Override(glitch_amount);
            analogGlitchVolume.colorDrift.Override(glitch_amount);
            analogGlitchVolume.verticalJump.Override(glitch_amount);

            if (glitch_amount <= 0)
            {
                glitch_amount = 0.0f;
                break;
            }

            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
    IEnumerator TurnOnCo()
    {
        glitch_amount = 0.0f;
        while (true)
        {
            glitch_amount += Time.deltaTime * glitch_speed;

            digitalGlitchVolume.intensity.Override(glitch_amount);
            analogGlitchVolume.scanLineJitter.Override(glitch_amount);
            analogGlitchVolume.horizontalShake.Override(glitch_amount);
            analogGlitchVolume.colorDrift.Override(glitch_amount);
            analogGlitchVolume.verticalJump.Override(glitch_amount);

            if (glitch_amount >= 1)
            {
                glitch_amount = 1.0f;
                break;
            }

            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
