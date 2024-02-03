using UnityEngine;

namespace BehaviorTree
{
    public class BossAttack360Shot : Node
    {
        GameObject _enemyBulletPrefab;
        private Transform _body;
        
        private float _fullWaitTime = 3f;

        private float _timeToWait;

        private float _timeCounter;

        private int _waveCounter;

        public BossAttack360Shot(Transform body, GameObject bullet)
        {
            this._enemyBulletPrefab = bullet;
            this._body = body;
            _timeToWait = _fullWaitTime / 3;
        }

        public override NodeState Evaluate()
        {
            state = NodeState.RUNNING;
            _timeCounter += Time.fixedDeltaTime;
            if (_timeCounter >= _timeToWait)
            {
                Fire360Shot();
                _timeToWait += _fullWaitTime / 3;
                _waveCounter++;
            }

            if (_waveCounter == Configuration.Boss_360ShotWaveCount)
            {
                state = NodeState.SUCCESS;
            }
            return state;
        }

        void Fire360Shot()
        {
            for (int i = 1; i < 16; i++)
            {
                GameObject bullet = GameObject.Instantiate(_enemyBulletPrefab, _body.position + new Vector3(2, 0, 0), _body.rotation);
                bullet.transform.RotateAround(_body.position, Vector3.forward, 24 * i);
                bullet.GetComponent<EnemyBullet>().Init(Configuration.Boss_360ShotBulletDamage, Configuration.Boss_360ShotBulletDistance, Configuration.Boss_360ShotBulletSpeed, _body.transform.gameObject);
            }
        }
    }
}