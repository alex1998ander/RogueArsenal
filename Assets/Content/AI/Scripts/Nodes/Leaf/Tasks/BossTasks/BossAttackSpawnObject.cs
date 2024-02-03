using UnityEngine;

namespace BehaviorTree
{
    public class BossAttackSpawnObject : Node
    {
        private Transform _positionToSpawn;
        private GameObject [] _positionsToSpawn;
        private GameObject _object;
        GameObject _objectToBe;
        private Vector3 _scaling = new Vector3(1, 1, 1);
        private int _amountToSpawn = 1;

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
        public BossAttackSpawnObject(Transform positionToSpawn, GameObject objectToSpawn, Vector3 scaling, int amountToSpawn)
        {
            this._positionToSpawn = positionToSpawn;
            this._object = objectToSpawn;
            this._scaling = scaling;
            this._amountToSpawn = amountToSpawn;
        }

        public BossAttackSpawnObject(GameObject [] positionToSpawn, GameObject objectToSpawn, Vector3 scaling)
        {
            this._positionsToSpawn = positionToSpawn;
            this._object = objectToSpawn;
            this._scaling = scaling;
        }

        public override NodeState Evaluate()
        {
            state = NodeState.RUNNING;

            if (_positionToSpawn != null)
            {
                for (int i = 0; i < _amountToSpawn; i++)
                {
                    _objectToBe = Object.Instantiate(_object,
                        _positionToSpawn.position,
                        Quaternion.identity);

                    _objectToBe.transform.localScale = _scaling;
                }
                state = NodeState.SUCCESS;
            }
            else
            {
                for (int i = 0; i < _positionsToSpawn.Length; i++)
                {
                    _objectToBe = Object.Instantiate(_object,
                        _positionsToSpawn[i].transform.position,
                        Quaternion.identity);

                    _objectToBe.transform.localScale = _scaling; 
                }

                state = NodeState.SUCCESS;
            }


            return state;
        }
    }
}