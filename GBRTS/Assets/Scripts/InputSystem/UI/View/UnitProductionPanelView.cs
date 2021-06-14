using System;
using System.Collections.Generic;
using Abstractions;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.View
{
    public class UnitProductionPanelView : MonoBehaviour
    {
        [SerializeField] private Image _currentProduction;
        [SerializeField] private TMP_Text _currentTime;
        [SerializeField] private Slider _currentProgress;

        [SerializeField] private Sprite _emptyItem;
        [SerializeField] private List<Image> _images;

        public void DisplayQueue(IList<IUnitProductionTask> productionTasks)
        {
            if (productionTasks.Count > 0)
            {
                var currentTask = productionTasks[0];
                
                _currentProduction.sprite = currentTask.Icon;
                currentTask.ProductionTimeLeft.Subscribe((timeLeft) =>
                {
                    UpdateTimeProgress(timeLeft, currentTask.ProductionTime);
                });
            }

            for (int i = 1; i < productionTasks.Count; i++)
            {
                _images[i - 1].sprite = productionTasks[i].Icon;
            }
        }

        private void UpdateTimeProgress(float timeLeft, float fullProdTime)
        {
            _currentTime.text = TimeSpan.FromSeconds((int)timeLeft).ToString();
            _currentProgress.value = timeLeft / fullProdTime;
        }
        
        public void AddNewItem(CollectionAddEvent<IUnitProductionTask> newElement)
        {
            if (newElement.Index == 0)
            {
                var currentTask = newElement.Value;
                _currentProduction.sprite = currentTask.Icon;
                currentTask.ProductionTimeLeft.Subscribe((timeLeft) =>
                {
                    UpdateTimeProgress(timeLeft, currentTask.ProductionTime);
                });
            }
            else
            {
                _images[newElement.Index - 1].sprite = newElement.Value.Icon;
            }
        }

        public void ClearAll()
        {
            _currentProduction.sprite = _emptyItem;
            _currentTime.text = TimeSpan.FromSeconds(0).ToString();
            _currentProgress.value = 0;
            
            foreach (var image in _images)
            {
                image.sprite = _emptyItem;
            }
        }
    }
}