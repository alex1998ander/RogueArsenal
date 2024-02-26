using System;
using UnityEngine;

public class EnemyContactDamage : MonoBehaviour
{
    private enum ZoneType
    {
        DamageOnEntering,
        DamageOnStaying
    }

    [SerializeField] private ZoneType zoneType = ZoneType.DamageOnEntering;
    [SerializeField] private float contactDamage = 10f;
    [SerializeField] private float damageTicksPerSecond = 1f;

    private float _timer;
    private bool _damageTickReady;

    private void FixedUpdate()
    {
        _timer = Mathf.Repeat(_timer + Time.fixedDeltaTime, damageTicksPerSecond);

        // if smaller than fixedDeltaTime, it must have looped around
        if (_timer < Time.fixedDeltaTime)
        {
            _damageTickReady = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (zoneType != ZoneType.DamageOnEntering)
            return;

        // Enemy deals contact damage to Player
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponentInParent<PlayerHealth>().InflictContactDamage(contactDamage);
            EventManager.OnPlayerHit.Trigger();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (zoneType != ZoneType.DamageOnStaying)
            return;

        if (_damageTickReady)
        {
            other.GetComponentInParent<PlayerHealth>().InflictContactDamage(contactDamage, true);
            EventManager.OnPlayerHit.Trigger();
        }
    }
}