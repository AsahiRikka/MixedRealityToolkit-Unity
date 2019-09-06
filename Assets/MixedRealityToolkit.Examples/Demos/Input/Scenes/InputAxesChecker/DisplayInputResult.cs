﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.MixedReality.Toolkit.Utilities;
using System;
using UnityEngine;

namespace Microsoft.MixedReality.Toolkit.Examples.Demos
{
    /// <summary>
    /// Displays a specified axis / button value on a specific TextMesh.
    /// Will display all active axes and buttons if the input type is None.
    /// </summary>
    public class DisplayInputResult : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Used for displaying data from input.")]
        private TextMesh displayTextMesh = null;

        [SerializeField]
        [Tooltip("The type of input to read. Will read all active if set to None.")]
        private AxisType inputType = AxisType.None;

        [SerializeField]
        [Tooltip("The axis number to read.")]
        [Range(1, 28)]
        private int axisNumber = 1;

        [SerializeField]
        [Tooltip("The button number to read.")]
        [Range(0, 19)]
        private int buttonNumber = 0;

        private void OnValidate()
        {
            switch (inputType)
            {
                case AxisType.SingleAxis:
                    name = $"{inputType}{axisNumber}";
                    break;
                case AxisType.Digital:
                    name = $"{inputType}{buttonNumber}";
                    break;
                case AxisType.None:
                    name = "AllActiveAxes";
                    break;
            }
        }

        private void Update()
        {
            if (displayTextMesh == null)
            {
                return;
            }

            switch (inputType)
            {
                case AxisType.SingleAxis:
                    displayTextMesh.text = $"Axis {axisNumber}: {UnityEngine.Input.GetAxis($"AXIS_{axisNumber}")}";
                    break;
                case AxisType.Digital:
                    if (Enum.TryParse($"JoystickButton{buttonNumber}", out KeyCode keyCode))
                    {
                        displayTextMesh.text = $"Button {buttonNumber}: {UnityEngine.Input.GetKey(keyCode)}";
                    }
                    break;
                case AxisType.None:
                    displayTextMesh.text = "All active:\n";
                    for (int i = 1; i <= 28; i++)
                    {
                        float reading = UnityEngine.Input.GetAxis($"AXIS_{i}");

                        if (reading != 0.0)
                        {
                            displayTextMesh.text += $"Axis {i}: {reading}\n";
                        }
                    }

                    for (int i = 0; i <= 19; i++)
                    {

                        if (Enum.TryParse($"JoystickButton{i}", out KeyCode buttonCode))
                        {
                            bool isPressed = UnityEngine.Input.GetKey(buttonCode);
                            if (isPressed)
                            {
                                displayTextMesh.text += $"Button {i}: {isPressed}\n";
                            }
                        }
                    }

                    break;
            }
        }
    }
}
