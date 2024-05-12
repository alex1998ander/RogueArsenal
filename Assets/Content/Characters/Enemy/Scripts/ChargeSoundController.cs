using BehaviorTree;
using UnityEngine;

public class ChargeSoundController : MonoBehaviour
{
    [SerializeField] private MovingEnemyBehaviourTree enemy;
    [SerializeField] private AudioSource chargeUpAudioSource;
    [SerializeField] private AudioSource chargeDownAudioSource;
    [SerializeField] private float chargeUpSoundDuration = 0.5f;

    private float _chargeUpSoundFinalVolumeValue;
    private float _chargeUpSoundFadeStartTimestamp;
    private float _chargeUpSoundFadeEndTimestamp;

    private void Start()
    {
        _chargeUpSoundFinalVolumeValue = chargeUpAudioSource.volume;
        chargeUpAudioSource.volume = 0f;
    }

    private void FixedUpdate()
    {
        ChargeState chargeState = enemy.GetChargeState();

        switch (chargeState)
        {
            case ChargeState.PreCharge:
            {
                if (!chargeUpAudioSource.isPlaying)
                {
                    chargeUpAudioSource.Play();
                    _chargeUpSoundFadeStartTimestamp = Time.time;
                    _chargeUpSoundFadeEndTimestamp = Time.time + chargeUpSoundDuration;
                }

                float progress = Mathf.InverseLerp(_chargeUpSoundFadeStartTimestamp, _chargeUpSoundFadeEndTimestamp, Time.time);
                float volume = Mathf.Lerp(0f, _chargeUpSoundFinalVolumeValue, progress);
                chargeUpAudioSource.volume = volume;

                break;
            }
            case ChargeState.MidCharge:
            {
                if (!chargeDownAudioSource.isPlaying)
                {
                    chargeUpAudioSource.Stop();
                    chargeUpAudioSource.volume = 0f;
                    chargeDownAudioSource.Play();
                }

                break;
            }
        }
    }
}