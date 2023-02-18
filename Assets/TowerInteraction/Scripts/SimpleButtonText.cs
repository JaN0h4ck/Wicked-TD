using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleButtonText : SimpleButton
{
    [Header("Text Colors")]
    [SerializeField] private Color _defaultTextColor;
    [SerializeField] private Color _pressedTextColor;
}
