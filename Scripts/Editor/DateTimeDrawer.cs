using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Kyoto.DateTime))]
public class DateTimeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // for prefab overrides
        EditorGUI.BeginProperty(position, label, property);

        //draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        Rect amountRect = new Rect(position.x, position.y, position.width, position.height);
        EditorGUI.PropertyField(amountRect, property.FindPropertyRelative("rawString"), GUIContent.none);

        EditorGUI.EndProperty();
    }
}
