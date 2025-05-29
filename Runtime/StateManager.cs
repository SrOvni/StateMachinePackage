using System;
using System.Collections.Generic;
using UnityEngine;
namespace StateMachine
{
    public abstract class StateManager<EState> : MonoBehaviour where EState : Enum
    {
        protected Dictionary<EState, BaseState<EState>> States = new Dictionary<EState, BaseState<EState>>();
        protected BaseState<EState> PreviousState;
        protected  BaseState<EState> CurrentState;
        protected bool _isTransitioningStates = false;
        protected bool _updateStateMachine = true;
        private void Awake() {
            AwakeStateMachine();
        }
        private void Start() {
            StartStateMachine();
            CurrentState.OnEnterState();
        }
        private void Update() {
            if(!_updateStateMachine)return;
            UpdateStateMachine();
            EState nextState = CurrentState.GetNextState();
            if(!nextState.Equals(CurrentState.StateKey))
                TransitionTo(nextState);
            else{
                CurrentState.UpdateState();
            }
        }
        private void FixedUpdate()
        {
            if(!_updateStateMachine)return;
            FixedUpdateStateMachine();
            CurrentState.FixedUpdate();   
        }
        protected void TransitionTo(EState nextState)
        {
            if(CurrentState.IsTransitioningState)return;
            _isTransitioningStates = true;
            CurrentState.OnExitState();
            PreviousState = CurrentState;
            CurrentState = States[nextState];
            CurrentState.OnEnterState();
            _isTransitioningStates = false;
        }
        private void OnTriggerEnter(Collider other) {
            CurrentState.OnTriggerEnter(other);
        }
        private void OnTriggerStay(Collider other) {
            CurrentState.OnTriggerStay(other);
        }
        private void OnTriggerExit(Collider other) {
            CurrentState.OnTriggerExit(other);
        }
        protected virtual void UpdateStateMachine(){}
        protected virtual void StartStateMachine(){}
        protected virtual void AwakeStateMachine(){}
        protected virtual void FixedUpdateStateMachine(){}
        protected virtual void InitializeStates(){}
        
    }
}
