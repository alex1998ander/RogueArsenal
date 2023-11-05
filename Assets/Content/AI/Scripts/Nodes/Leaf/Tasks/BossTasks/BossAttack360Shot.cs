using UnityEngine;

namespace BehaviorTree
{
    public class BossAttack360Shot: Node
    {
        GameObject _enemyBulletPrefab;
        private Transform _body;
        public BossAttack360Shot(Transform body ,GameObject bullet)
        {
            this._enemyBulletPrefab = bullet;
            this._body = body;
        }

        public override NodeState Evaluate()
        {
            for (int i = 1; i < 16; i++)
            {
                GameObject bullet = GameObject.Instantiate(_enemyBulletPrefab, _body.position + new Vector3(2,0,0), _body.rotation);
                bullet.transform.RotateAround(_body.position, Vector3.forward, 24 * i);
                bullet.GetComponent<EnemyBullet>().Init(20, 20, _body.transform.gameObject);
            }
            
            return NodeState.SUCCESS;
        }

        
    }
}