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
        public Action<ICommandExecutor, ICommandsQueue> OnClick;
        
        [SerializeField] private Button _produceUnitEllenButton;
        [SerializeField] private Button _produceUnitChomperButton;
        [SerializeField] private Button _moveButton;
        [SerializeField] private Button _attackButton;
        [SerializeField] private Button _stopButton;
        [SerializeField] private Button _patrolButton;
        [SerializeField] private Button _setRallyButton;

        private Dictionary<Type, Button> _buttons;

        protected void Awake()
        {
            _buttons = new Dictionary<Type, Button>
            {
                {typeof(ICommandExecutor<IProduceUnitCommandEllen>), _produceUnitEllenButton},
                {typeof(ICommandExecutor<IProduceUnitCommandChomper>), _produceUnitChomperButton},
                {typeof(ICommandExecutor<IMoveCommand>), _moveButton},
                {typeof(ICommandExecutor<IAttackCommand>), _attackButton},
                {typeof(ICommandExecutor<IStopCommand>), _stopButton},
                {typeof(ICommandExecutor<IPatrolCommand>), _patrolButton},
                {typeof(ICommandExecutor<ISetRallyPointCommand>), _setRallyButton}
            };
        }

        public void SetButtons(List<ICommandExecutor> commandExecutors, ICommandsQueue commandsQueue)
        {
            if (commandExecutors == null)
                return;
            
            foreach (var executor in commandExecutors)
            {
                var button = _buttons.FirstOrDefault(kvp => kvp.Key.IsInstanceOfType(executor)).Value;
                if (button != null)
                {
                    button.gameObject.SetActive(true);
                    button.onClick.AddListener(() => OnClick?.Invoke(executor, commandsQueue));
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