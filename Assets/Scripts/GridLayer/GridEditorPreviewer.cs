﻿using System;
using UnityEditor;
using UnityEngine;

namespace GridLayer
{
    [CustomEditor(typeof(GridController))]
    public class GridEditorPreviewer : Editor
    {
        private void OnEnable()
        {
            // GridController instance = (GridController) target;
            // instance.instantiateObject();
        }
    }
}
