using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Common.Utils
{
    public class ObjectPoolManager : MonoBehaviour
    {
        [SerializeField] private bool addToDontDestroyOnLoad = false;

        private GameObject _emptyHolder;

        private static GameObject _particleSystemsEmpty;
        private static GameObject _gameObjectsEmpty;
        private static GameObject _soundFXEmpty;

        private static Dictionary<GameObject, ObjectPool<GameObject>> _objectPools;
        private static Dictionary<GameObject, GameObject> _cloneToPrefabMap;

        public enum PoolType
        {
            ParticleSystems,
            GameObjects,
            SoundFX,
        }

        public static PoolType PoolingType;

        private void Awake()
        {
            _objectPools = new Dictionary<GameObject, ObjectPool<GameObject>>();
            _cloneToPrefabMap = new Dictionary<GameObject, GameObject>();

            SetupEmpties();
        }

        private void SetupEmpties()
        {
            _emptyHolder = new GameObject("ObjectPools");
            
            _particleSystemsEmpty = new  GameObject("ParticleSystems");
            _particleSystemsEmpty.transform.SetParent(_emptyHolder.transform);
            
            _gameObjectsEmpty = new  GameObject("GameObjects");
            _gameObjectsEmpty.transform.SetParent(_emptyHolder.transform);
            
            _soundFXEmpty = new  GameObject("SoundFX");
            _soundFXEmpty.transform.SetParent(_emptyHolder.transform);
            
            if(addToDontDestroyOnLoad)
                DontDestroyOnLoad(_emptyHolder);
        }

        private static void CreatePool(GameObject prefab, Vector3 pos, Quaternion rot, PoolType poolType =  PoolType.GameObjects)
        {
            var pool = new ObjectPool<GameObject>(
                createFunc: () => CreateObject(prefab, pos, rot, poolType),
                actionOnGet: OnGetObject,
                actionOnRelease: OnReleaseObject,
                actionOnDestroy: OnDestroyObject);
            
            _objectPools.Add(prefab, pool);
        }
        
        private static void CreatePool(GameObject prefab, Transform parent, Quaternion rot, PoolType poolType =  PoolType.GameObjects)
        {
            var pool = new ObjectPool<GameObject>(
                createFunc: () => CreateObject(prefab, parent, rot, poolType),
                actionOnGet: OnGetObject,
                actionOnRelease: OnReleaseObject,
                actionOnDestroy: OnDestroyObject);
            
            _objectPools.Add(prefab, pool);
        }
        
        private static GameObject CreateObject(GameObject prefab, Vector3 pos, Quaternion rot, PoolType poolType =  PoolType.GameObjects)
        {
            prefab.SetActive(false);
            var obj = Instantiate(prefab, pos, rot);
            prefab.SetActive(true);
            var parentObject = SetParentObject(poolType);
            obj.transform.SetParent(parentObject.transform);
            return obj;
        }
        
        private static GameObject CreateObject(GameObject prefab, Transform parent, Quaternion rot, PoolType poolType =  PoolType.GameObjects)
        {
            prefab.SetActive(false);
            var obj = Instantiate(prefab, parent);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = rot;
            obj.transform.localScale = Vector3.one;
            prefab.SetActive(true);
            return obj;
        }

        private static void OnGetObject(GameObject obj)
        {
            // Optional logic
        }
        
        private static void OnReleaseObject(GameObject obj)
        {
            obj.SetActive(false);
        }
        
        private static void OnDestroyObject(GameObject obj)
        {
            if (_cloneToPrefabMap.ContainsKey(obj))
                _cloneToPrefabMap.Remove(obj);
        }

        private static GameObject SetParentObject(PoolType poolType) => poolType switch
        {
            PoolType.ParticleSystems => _particleSystemsEmpty,
            PoolType.GameObjects => _gameObjectsEmpty,
            PoolType.SoundFX => _soundFXEmpty,
            _ => throw new ArgumentOutOfRangeException(nameof(poolType), poolType, null)
        };

        private static T SpawnObject<T>(GameObject objectToSpawn, Vector3 pos, Quaternion rot, PoolType poolType = PoolType.GameObjects) where T : Object
        {
            if (!_objectPools.ContainsKey(objectToSpawn))
                CreatePool(objectToSpawn, pos, rot, poolType);
            
            var obj = _objectPools[objectToSpawn].Get();

            if (obj != null)
            {
                if (!_cloneToPrefabMap.ContainsKey(obj))
                    _cloneToPrefabMap.Add(obj, objectToSpawn);
                
                obj.transform.position = pos;
                obj.transform.rotation = rot;
                obj.SetActive(true);

                if (typeof(T) == typeof(GameObject))
                    return obj as T;
                
                var component = obj.GetComponent<T>();
                if (component == null)
                {
                    Debug.LogError($"Object {objectToSpawn.name} doesn't have a component of type {typeof(T)}");
                    return null;
                }

                return component;
            }

            return null;
        }
        
        private static T SpawnObject<T>(GameObject objectToSpawn, Transform parent, Quaternion rot, PoolType poolType = PoolType.GameObjects) where T : Object
        {
            if (!_objectPools.ContainsKey(objectToSpawn))
                CreatePool(objectToSpawn, parent, rot, poolType);
            
            var obj = _objectPools[objectToSpawn].Get();

            if (obj != null)
            {
                if (!_cloneToPrefabMap.ContainsKey(obj))
                    _cloneToPrefabMap.Add(obj, objectToSpawn);
                
                obj.transform.SetParent(parent);
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localRotation = Quaternion.identity;
                obj.SetActive(true);

                if (typeof(T) == typeof(GameObject))
                    return obj as T;
                
                var component = obj.GetComponent<T>();
                if (component == null)
                {
                    Debug.LogError($"Object {objectToSpawn.name} doesn't have a component of type {typeof(T)}");
                    return null;
                }

                return component;
            }

            return null;
        }

        public static T SpawnObject<T>(T typePrefab, Vector3 pos, Quaternion rot, PoolType poolType = PoolType.GameObjects) where T : Component
            => SpawnObject<T>(typePrefab.gameObject, pos, rot, poolType);
        
        public static GameObject SpawnObject(GameObject objectToSpawn, Vector3 pos, Quaternion rot, PoolType poolType = PoolType.GameObjects)
            => SpawnObject<GameObject>(objectToSpawn, pos, rot, poolType);
        
        public static T SpawnObject<T>(T typePrefab, Transform parent, Quaternion rot, PoolType poolType = PoolType.GameObjects) where T : Component
            => SpawnObject<T>(typePrefab.gameObject, parent, rot, poolType);
        
        public static GameObject SpawnObject(GameObject objectToSpawn, Transform parent, Quaternion rot, PoolType poolType = PoolType.GameObjects)
            => SpawnObject<GameObject>(objectToSpawn, parent, rot, poolType);

        public static void ReturnObjectToPool(GameObject obj, PoolType poolType = PoolType.GameObjects)
        {
            if (_cloneToPrefabMap.TryGetValue(obj, out GameObject prefab))
            {
                var parentObject = SetParentObject(poolType);

                if (obj.transform.parent != parentObject.transform)
                    obj.transform.SetParent(parentObject.transform);
                
                if(_objectPools.TryGetValue(prefab, out ObjectPool<GameObject> pool))
                    pool.Release(obj);
            }
            else
            {
                Debug.LogError($"Trying to return an object than is not pooled: {obj.name}");
            }
        }
    }
}