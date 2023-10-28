using UnityEngine;

namespace BehaviorTree
{
    public class BossAttackSpawnObject: Node
    {
        private Transform _body;
        private GameObject _object;
        GameObject _objectToBe;
        private Vector3 _scaling = new Vector3(1,1,1);
        public BossAttackSpawnObject(Transform body, GameObject objectToSpawn)
        {
            this._body = body;
            this._object = objectToSpawn;
            
        }
        
        public BossAttackSpawnObject(Transform body, GameObject objectToSpawn, Vector3 scaling)
        {
            this._body = body;
            this._object = objectToSpawn;
            this._scaling = scaling;
        }

        public override NodeState Evaluate()
        {
            _objectToBe = Object.Instantiate(_object,
                _body.position,
                Quaternion.identity);
            
                //_objectToBe.GetComponent<EnemyShieldGenerator>().shield = _shield;
                _objectToBe.transform.localScale = _scaling;
            
            
            return NodeState.SUCCESS;
        }
    }
}