﻿using Abstractions;
using UI.Model;
using UnityEngine;
using Zenject;

namespace UI.Presenter
{
    public class SelectedItemOutlinePresenter : MonoBehaviour
    {
        [Inject] private SelectedItem _item;
        
        protected void Start()
        {
            _item.OnValueChanged += OnSelect;
            _item.OnDeselected += OnDeselect;
        }

        private void OnSelect(ISelectableItem selectedItem)
        {
            if (selectedItem != null)
            {
                selectedItem.Outline.enabled = true;
            }
        }

        private void OnDeselect(ISelectableItem deselectedItem)
        {
            Debug.Log(deselectedItem != null);
            if (deselectedItem != null)
            {
                deselectedItem.Outline.enabled = false;
            }
        }
    }
}