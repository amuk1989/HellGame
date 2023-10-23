﻿using System;
using System.Globalization;
using Scanning.Interfaces;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI.Views
{
    public class ProgressUI : BaseUI
    {
        [SerializeField] private TMP_Text _progressIndication;

        private IScanningService _scanningService;

        [Inject]
        private void Construct(IScanningService scanningService)
        {
            _scanningService = scanningService;
        }

        private void Start()
        {
            _scanningService
                .ScannedAreaAsObservable()
                .Subscribe(value => _progressIndication.text = value.ToString(CultureInfo.CurrentCulture))
                .AddTo(this);
        }
    }
}