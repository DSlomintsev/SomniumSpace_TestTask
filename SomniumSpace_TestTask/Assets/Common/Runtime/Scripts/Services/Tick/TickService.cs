using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common.Services.Cameras;
using Common.Utils;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Common.Services.Tick
{
    public class TickService : BaseService
    {
        private readonly Dictionary<string, DataAction> _tickableDataActionDict = new();
        private readonly Dictionary<string, Action> _tickableActionsDict = new();

        private readonly List<Action> _tickableSecActions = new();
        private readonly List<Action> _tickableSecActionsToAdd = new();
        private readonly List<Action> _tickableSecActionsToRemove = new();

        private readonly List<Action> _tickableActions = new();
        private readonly List<Action> _tickableActionsToAdd = new();
        private readonly List<Action> _tickableActionsToRemove = new();

        private readonly List<TimeTickIndexActionData> _tickIndexActions = new();
        private readonly List<TimeTickIndexActionData> _fixTickIndexActions = new();
        private readonly List<Action> _fixedTickableActions = new();
        private readonly List<Action> _fixedTickableActionsRemove = new();

        private readonly List<Action> _lateTickableActions = new();
        private readonly List<Action> _lateTickableActionsRemove = new();
        private bool _isLateTickProcessing;

        private readonly List<NextFrameTickActionData> _nextFrameAction = new();

        private readonly List<TimeTickActionData> _timeTickableActions = new();

        private CancellationTokenSource _oneSecUpdateCts;

        private TickMonoBeh _tickMonoBeh;

        public void Construct(TickMonoBeh tickMonoBehPrefab)
        {
            _tickMonoBeh = SpawnUtils.Instantiate(tickMonoBehPrefab);
            _tickMonoBeh.Init(this);
        }

        protected override void OnInit()
        {
            base.OnInit();

            var go = new GameObject("CoroutineRunner");
            Object.DontDestroyOnLoad(go);

            _oneSecUpdateCts?.Cancel();
            _oneSecUpdateCts = new CancellationTokenSource();
            _ = OneSecUpdate(_oneSecUpdateCts.Token);
        }

        protected override void OnDeInit()
        {
            base.OnDeInit();

            _oneSecUpdateCts?.Cancel();

            _timeTickableActions.Clear();
            _tickableActions.Clear();
            _tickableSecActions.Clear();
            _fixedTickableActions.Clear();
            _lateTickableActions.Clear();
            _lateTickableActionsRemove.Clear();

            NewDayTimer();
        }

        private System.Timers.Timer _timer;

        private void NewDayTimer()
        {
            var now = DateTime.UtcNow;
            var midnight = now.Date.AddDays(1);
            var timeToGo = midnight - now;

            _timer = new System.Timers.Timer(timeToGo.TotalMilliseconds);
            _timer.Elapsed += (_, _) =>
            {
                // DSiT Fix it
                //_eventService.NewDayEvent.Call();
                NewDayTimer();
            };
            _timer.AutoReset = false;
            _timer.Start();
        }

        public void AddTickAction(string key, Action action)=>_tickableActionsDict.TryAdd(key, action);

        public void RemoveTickAction(string key) => _tickableActionsDict.Remove(key);

        public void AddTickDataAction(string key, DataAction action) => _tickableDataActionDict.TryAdd(key, action);
        public void RemoveTickDataAction(string key) => _tickableDataActionDict.Remove(key);
        
        public void AddTickIndexAction(int index, Action<float> action)
        {
            var actionIndex = _tickIndexActions.GetIndexByAction(action);
            if (actionIndex == -1)
            {
                _tickIndexActions.Add(new TimeTickIndexActionData(index,action));
                DoSort(_tickIndexActions);
            }
        }

        private async Task OneSecUpdate(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                foreach (var action in _tickableSecActionsToAdd)
                    _tickableSecActions.Add(action);
                _tickableSecActionsToAdd.Clear();
                foreach (var action in _tickableSecActionsToRemove)
                    _tickableSecActions.Remove(action);
                _tickableSecActionsToRemove.Clear();
                foreach (var action in _tickableSecActions)
                    action();

                await Task.Delay(1000, cancellationToken: cancellationToken);    
            }
        }
        
        private readonly List<Action<float>> _tickIndexActionsToRemove = new ();

        public void RemoveTickIndexAction(Action<float> action) => _tickIndexActionsToRemove.Add(action);
        
        public void AddFixTickIndexAction(int index, Action<float> action)
        {
            var actionIndex = _fixTickIndexActions.GetIndexByAction(action);
            if (actionIndex == -1)
            {
                _fixTickIndexActions.Add(new TimeTickIndexActionData(index,action));
                DoSort(_fixTickIndexActions);
                //_tickableIndexActions = _tickableIndexActions.OrderBy(o => o.Index).ToList();
            }
        }
        
        public void RemoveFixTickIndexAction(Action<float> action)
        {
            var actionIndex = _fixTickIndexActions.GetIndexByAction(action);
            if (actionIndex != -1)
            {
                _fixTickIndexActions.RemoveAt(actionIndex);
                if (_fixTickIndexActionsIndex != -1)
                {
                    if (actionIndex <= _fixTickIndexActionsIndex)
                        _fixTickIndexActionsIndex--;
                }
            }
        }

        private static void DoSort(List<TimeTickIndexActionData> list)
        {
            for (var write = 0; write < list.Count; write++) {
                for (var sort = 0; sort < list.Count - 1; sort++) {
                    if (list[sort].Index > list[sort + 1].Index) {
                        (list[sort + 1], list[sort]) = (list[sort], list[sort + 1]);
                    }
                }
            }
        }
        
        public void AddTickAction(Action action)
        {
            if (!_tickableActions.Contains(action) && !_tickableActionsToAdd.Contains(action))
                _tickableActionsToAdd.Add(action);
        }
        
        public void RemoveTickAction(Action action)
        {
            if (_tickableActions.Contains(action) && !_tickableActionsToRemove.Contains(action))
                _tickableActionsToRemove.Add(action);
        }
        
        public void AddTickSecAction(Action action)
        {
            if (!_tickableSecActions.Contains(action) && !_tickableSecActionsToAdd.Contains(action))
                _tickableSecActionsToAdd.Add(action);
        }
        
        public void RemoveTickSecAction(Action action)
        {
            if (_tickableSecActions.Contains(action) && !_tickableSecActionsToRemove.Contains(action))
                _tickableSecActionsToRemove.Add(action);
        }
        
        public void AddTimeTickAction(TimeTickActionData actionData)
        {
            if (!_timeTickableActions.Contains(actionData))
            {
                actionData.StartTime = Time.realtimeSinceStartup;
                actionData.EndTime = actionData.StartTime+actionData.Interval;
                _timeTickableActions.Add(actionData);   
            }
        }
        
        public void RemoveTimeTickAction(TimeTickActionData actionData)
        {
            if (_timeTickableActions.Contains(actionData))
                _timeTickableActions.Remove(actionData);
        }

        public void RemoveTimeTickAction(string id)
        {
            var item = _timeTickableActions.GetById(id);
            if (!item.Equals(default(TimeTickActionData)))
                _timeTickableActions.Remove(item);
        }

        public void AddFixedTickAction(Action action)
        {
            if (!_fixedTickableActions.Contains(action))
                _fixedTickableActions.Add(action);
        }
        
        public void RemoveFixedTickAction(Action action)
        {
            if (_fixedTickableActions.Contains(action) && !_fixedTickableActionsRemove.Contains(action))
                _fixedTickableActionsRemove.Add(action);
                
        }

        public void AddLateTickAction(Action action)
        {
            if (!_lateTickableActions.Contains(action))
                _lateTickableActions.Add(action);
        }

        public void RemoveLateTickAction(Action action)
        {
            if (_isLateTickProcessing)
            {
                if (!_lateTickableActionsRemove.Contains(action))
                    _lateTickableActionsRemove.Add(action);    
            }
            else
            {
                if (_lateTickableActions.Contains(action))
                    _lateTickableActions.Remove(action);
            }
        }

        public void Tick()
        {
            for (var i = _tickIndexActionsToRemove.Count - 1; i >= 0; i--)
            {
                var actionIndex = _tickIndexActions.GetIndexByAction(_tickIndexActionsToRemove[i]);
                if (actionIndex != -1)
                    _tickIndexActions.RemoveAt(actionIndex);
            }
            _tickIndexActionsToRemove.Clear();
            
            var time =  Time.deltaTime;
            foreach (var data in _tickIndexActions)
                data.Action(time);

            foreach (var action in _tickableActionsToAdd)
                _tickableActions.Add(action);
            _tickableActionsToAdd.Clear();
            
            foreach (var action in _tickableActionsToRemove)
                _tickableActions.Remove(action);
            _tickableActionsToRemove.Clear();

            var frameCount = Time.frameCount;
            for (var i = 0; i < _nextFrameAction.Count; i++)
            {
                var action = _nextFrameAction[i];
                if (action.Frame == frameCount) continue;

                action.Action();
                _nextFrameAction.RemoveAt(i);
                i--;
            }

            foreach (var action in _tickableActions)
                action();

            // DSiT CheckPerformance
            foreach (var tickableAction in _tickableActionsDict)
                tickableAction.Value();

            foreach (var tickableAction in _tickableDataActionDict)
                tickableAction.Value.Action(tickableAction.Value.Data);
        }

        private int _fixTickIndexActionsIndex = -1;

        public void FixedTick()
        {
            Debug.Log("F");
            var time =  Time.fixedDeltaTime;
            for (_fixTickIndexActionsIndex = 0; _fixTickIndexActionsIndex < _fixTickIndexActions.Count; _fixTickIndexActionsIndex++)
            {
                var data = _fixTickIndexActions[_fixTickIndexActionsIndex];
                data.Action(time);
            }
            _fixTickIndexActionsIndex = -1;

            foreach (var fixTickAction in _fixedTickableActionsRemove)
                _fixedTickableActions.Remove(fixTickAction);
            _fixedTickableActionsRemove.Clear();

            var isAnyShouldBeRemoved = false;
            foreach (var action in _fixedTickableActions)
                action();

            var realtimeSinceStartup = Time.realtimeSinceStartup;
            for (var i = 0; i < _timeTickableActions.Count; i++)
            {
                var action = _timeTickableActions[i];
                if (realtimeSinceStartup >= action.EndTime)
                {
                    action.EndTime = realtimeSinceStartup + action.Interval;
                    action.Action();
                    if (action.IsOneTime)
                    {
                        action.ShouldBeRemoved = true;
                        isAnyShouldBeRemoved = true;
                    }
                    _timeTickableActions[i] = action;
                }
            }

            if (isAnyShouldBeRemoved)
            {
                for (var i = _timeTickableActions.Count-1; i >= 0; i--)
                {
                    var action = _timeTickableActions[i];
                    if (action.ShouldBeRemoved)
                        _timeTickableActions.RemoveAt(i);
                }   
            }
        }

        public void LateTick()
        {
            _isLateTickProcessing = true;
            foreach (var action in _lateTickableActions)
                action();

            foreach (var action in _lateTickableActionsRemove)
                _lateTickableActions.Remove(action);

            _lateTickableActionsRemove.Clear();
            _isLateTickProcessing = false;
        }

        public void AddNextFrameTick(Action action)
        {
            _nextFrameAction.Add(new NextFrameTickActionData(Time.frameCount, action));;
        }
    }
}