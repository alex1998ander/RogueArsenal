using UnityEngine;

namespace BehaviorTree
{
    public class BossAttackSpawnObject: Node
    {
        private Transform _positionToSpawn;
        private GameObject _object;
        GameObject _objectToBe;
        private Vector3 _scaling = new Vector3(1,1,1);
        private bool shieldActivate = false;
        private GameObject _shield;
        public BossAttackSpawnObject(Transform positionToSpawn, GameObject objectToSpawn)
        {
            this._positionToSpawn = positionToSpawn;
            this._object = objectToSpawn;
            
        }
        
        public BossAttackSpawnObject(Transform positionToSpawn, GameObject objectToSpawn, Vector3 scaling)
        {
            this._positionToSpawn = positionToSpawn;
            this._object = objectToSpawn;
            this._scaling = scaling;
        }

        public override NodeState Evaluate()
        {
            _objectToBe = Object.Instantiate(_object,
                _positionToSpawn.position,
                Quaternion.identity);
            
                //_objectToBe.GetComponent<EnemyShieldGenerator>().shield = _shield;
                _objectToBe.transform.localScale = _scaling;
                if (shieldActivate)
                {
                    shieldActivate = false;
                    _shield.SetActive(true);
                }    
            
            return NodeState.SUCCESS;
        }
    }
}