using System;
using UnityEngine;
using UnityEngine.Events;

namespace StateMachine
{
    public abstract class BaseState<EState> where EState : Enum
    {
        private EState _stateKey;
        public EState StateKey => _stateKey;
        protected bool _isTransitioningStates = false;
        public bool IsTransitioningState => _isTransitioningStates;
        public UnityEvent<EState> OnStateChange = new UnityEvent<EState>();
        public BaseState(EState stateKey)
        {
            _stateKey = stateKey;
        }
        public abstract void OnEnterState();
        public abstract EState GetNextState();
        public abstract void OnExitState();
        public virtual void UpdateState(){}
        public virtual void FixedUpdate(){}
        public virtual void OnTriggerEnter(Collider other){}
        public virtual void OnTriggerStay(Collider other){}
        public virtual void OnTriggerExit(Collider other){}
        public string DebugCurrentState()
        {
            return "Current state: " + _stateKey;
        }
    }
}
