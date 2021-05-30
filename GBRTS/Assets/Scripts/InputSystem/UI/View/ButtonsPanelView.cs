using System;
using System.Collections.Generic;
using System.Linq;
using Abstractions;
using UnityEngine;
using UnityEngine.UI;

namespace UI.View
{
    public class ButtonsPanelView : MonoBehaviour
    {
        public Action<ICommandExecutor> OnClick;
        
        [SerializeField] private Button _produceUnitButton;
        [SerializeField] private Button _moveButton;
        [SerializeField] private Button _attackButton;
        [SerializeField] private Button _stopButton;
        [SerializeField] private Button _patrolButton;

        private Dictionary<Type, Button> _buttons;

        protected void Awake()
        {
            _buttons = new Dictionary<Type, Button>
            {
                {typeof(CommandExecutorBase<IProduceUnitCommand>), _produceUnitButton},
                {typeof(CommandExecutorBase<IMoveCommand>), _moveButton},
                {typeof(CommandExecutorBase<IAttackCommand>), _attackButton},
                {typeof(CommandExecutorBase<IStopCommand>), _stopButton},
                {typeof(CommandExecutorBase<IPatrolCommand>), _patrolButton}
            };
        }

        public void SetButtons(List<ICommandExecutor> commandExecutors)
        {
            if (commandExecutors == null)
                return;
            
            foreach (var executor in commandExecutors)
            {
                var button = _buttons.FirstOrDefault(kvp => kvp.Key.IsInstanceOfType(executor)).Value;
                if (button != null)
                {
                    button.gameObject.SetActive(true);
                    button.onClick.AddListener(() => OnClick?.Invoke(executor));
                }
                else
                {
                    Debug.LogError("No button found for executor type " + executor.GetType());
                }
            }
        }

        public void ClearButtons()
        {
            foreach (var kvp in _buttons)
            {
                kvp.Value.gameObject.SetActive(false);
                kvp.Value.onClick.RemoveAllListeners();
            }
        }
    }
}