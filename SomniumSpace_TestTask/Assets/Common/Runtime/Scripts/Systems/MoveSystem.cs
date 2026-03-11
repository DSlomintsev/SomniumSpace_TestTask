using System;
using System.Collections.Generic;
using Common.Services;
using Common.Services.Tick;
using Common.Utils;
using UnityEngine;

namespace Common.Systems
{
    public class MoveSystem:IService
    {
        private readonly List<MoveSystemData> _moveDatasToAdd = new();
        private readonly List<MoveSystemData> _moveDatasToRemove = new();
        private readonly List<MoveSystemData> _moveDatas = new();
        
        public void Init()
        {
            ServiceLocator.Get<TickService>().AddFixTickIndexAction(1, Update);
        }

        public void DeInit()
        {
            ServiceLocator.Get<TickService>().RemoveFixTickIndexAction(Update);
        }

        public void Add(MoveSystemData data)
        {
            if (!_moveDatasToAdd.Contains(data))
                _moveDatasToAdd.Add(data);
        }

        public void Remove(MoveSystemData data)
        {
            if (!_moveDatasToRemove.Contains(data))
                _moveDatasToRemove.Add(data);
        }

        public void Start()
        {
            
        }
        
        public void Stop()
        {
            
        }

        private void Update(float time)
        {
            foreach (var data in _moveDatasToRemove)
                _moveDatas.RemoveUnordered(_moveDatas.IndexOf(data));
            _moveDatasToRemove.Clear();
            
            _moveDatas.AddRange(_moveDatasToAdd);
            _moveDatasToAdd.Clear();

            foreach (var data in _moveDatas)
            {
                var targetDir=data.TargetDir;
                Vector3 targetDirNormalized;
                if (targetDir.Equals(Vector3.zero))
                {
                    var targetPos = data.TargetObj == null ? data.TargetPos : data.TargetObj.position;
                    targetDir = targetPos - data.SourceObj.position;
                    targetDirNormalized = targetDir.normalized;
                }
                else
                {
                    targetDirNormalized = targetDir;
                }

                data.SourceObj.position += targetDirNormalized * data.Speed * time;
                if (targetDir.sqrMagnitude < .1f)
                {
                    _moveDatasToRemove.Add(data);
                    data.ReachCallback.Call();
                }
            }
            
            foreach (var data in _moveDatasToRemove)
                _moveDatas.RemoveUnordered(_moveDatas.IndexOf(data));
            _moveDatasToRemove.Clear();
        }
    }
    
    public struct MoveSystemData : IEquatable<MoveSystemData>
    {
        public Transform SourceObj;

        public Vector3 TargetDir;
        
        public Vector3 TargetPos;
        public Transform TargetObj;

        public float Speed;
        public Action ReachCallback;

        public MoveSystemData(Transform sourceObj, Vector3 targetDir, Vector3 targetPos, Transform targetObj, float speed, Action reachCallback)
        {
            SourceObj = sourceObj;
            TargetDir = targetDir;
            TargetPos = targetPos;
            TargetObj = targetObj;
            Speed = speed;
            ReachCallback = reachCallback;
        }

        public bool Equals(MoveSystemData other) => Equals(SourceObj, other.SourceObj);

        public override int GetHashCode() => HashCode.Combine(SourceObj, TargetDir, TargetPos, TargetObj, Speed, ReachCallback);
    }
}