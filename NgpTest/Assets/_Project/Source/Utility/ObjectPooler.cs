using System.Collections.Generic;
using UnityEngine;

namespace Digi.VisualScreen
{
    public class ObjectPooler : MonoBehaviour
    {

        [System.Serializable]
        public class Pool
        {
            public GameObject prefab;
            public int size;
        }

        public static ObjectPooler Instance;

        [SerializeField] private List<Pool> _pools;

        private Dictionary<int, List<GameObject>> _poolDictionary;
        private List<GameObject> _objectPool;

        private void Awake()
        {
            if (Instance != null)
            {
                gameObject.SetActive(false);
                return;
            }
            else
            {
                Instance = this;
            }

            InitializePool();
        }

        private void InitializePool()
        {
            _poolDictionary = new Dictionary<int, List<GameObject>>();

            foreach (Pool pool in _pools)
            {
                _objectPool = new List<GameObject>();

                for (int i = 0; i < pool.size; i++)
                {
                    GameObject obj = Instantiate(pool.prefab, gameObject.transform, true);
                    obj.SetActive(false);
                    _objectPool.Add(obj);
                }

                _poolDictionary.Add(pool.prefab.GetInstanceID(), _objectPool);
            }
        }

        public void AddToPool(GameObject prefab, int count)
        {
            if (_poolDictionary.ContainsKey(prefab.GetInstanceID()))
            {
                return;
            }
            List<GameObject> poolList = new List<GameObject>();

            for (int i = 0; i < count; i++)
            {
                GameObject obj = Instantiate(prefab, gameObject.transform, true);
                obj.SetActive(false);
                poolList.Add(obj);
            }

            _poolDictionary.Add(prefab.GetInstanceID(), poolList);
        }


        public GameObject SpawnFromPool(GameObject obj)
        {
            bool isPoolAvailable = false;
            GameObject objectToSpawn = null;
            int id = obj.GetInstanceID();

            for (int i = 0; i < _poolDictionary[id].Count; i++)
            {
                if (!_poolDictionary[id][i].activeInHierarchy)
                {
                    isPoolAvailable = true;
                    objectToSpawn = _poolDictionary[id][i];
                }
            }

            if (!isPoolAvailable)
            {
                return AddToPool(id);
            }
            else
            {
                objectToSpawn.SetActive(true);
                return objectToSpawn;
            }
        }

        private GameObject AddToPool(int id)
        {
            GameObject newObject = _poolDictionary[id][0];

            GameObject obj = Instantiate(newObject, gameObject.transform, true);
            _poolDictionary[id].Add(obj);
            obj.SetActive(true);
            return obj;
        }
    }
}
