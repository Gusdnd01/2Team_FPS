using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseController : MonoBehaviour
{
    [SerializeField] private Slider mouse = null;
    [SerializeField] private float tlqkf = 0f;

    private void Update() {
        tlqkffdd();
    }
    private void tlqkffdd() {
        tlqkf = mouse.value;
    }
}
