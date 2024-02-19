using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

namespace BehaviorTree
{
    public class BossAttackStomp : Node
    {
        private Transform _stompTarget;
        private SpriteRenderer _bossVisual;
        private Transform _body;
        private GameObject _ui;
        private BoxCollider2D _contactDamageZone;
        private GameObject _weapon;

        private float _waitTime = 1.5f;
        private float _timeCounter;

        private bool _landPosSet = false;

        private readonly LayerMask _targetLayer = LayerMask.GetMask("Player_Trigger");

        public BossAttackStomp(Transform body, Transform stompTarget, SpriteRenderer bossVisual, GameObject ui, EnemyWeapon weapon, BoxCollider2D contactDamageZone)
        {
            _body = body;
            _stompTarget = stompTarget;
            _bossVisual = bossVisual;
            _ui = ui;
            _contactDamageZone = contactDamageZone;
            _weapon = weapon.gameObject;
        }

        public override NodeState Evaluate()
        {
            state = NodeState.FAILURE;

            _contactDamageZone.enabled = false;
            _bossVisual.enabled = false;
            _ui.SetActive(false);
            _weapon.SetActive(false);

            _timeCounter += Time.fixedDeltaTime;
            if (_timeCounter >= _waitTime / 2 && !_landPosSet)
            {
                _body.position = _stompTarget.position;
                _landPosSet = true;
            }

            if (_timeCounter >= _waitTime)
            {
                _contactDamageZone.enabled = true;
                _bossVisual.enabled = true;
                _ui.SetActive(true);
                _weapon.SetActive(true);

                Collider2D playerCollider = Physics2D.OverlapCircle(_body.position, Configuration.Boss_StompRadius, _targetLayer);
                playerCollider?.GetComponentInParent<ICharacterHealth>()?.InflictDamage(Configuration.Boss_StompDamage);

                _timeCounter = 0f;
                _landPosSet = false;

                state = NodeState.SUCCESS;
            }

            return state;
        }
    }
}