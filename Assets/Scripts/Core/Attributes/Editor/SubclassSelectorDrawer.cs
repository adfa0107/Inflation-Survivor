using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace InflationSurvivor.Core.Attributes.Editor;

[CustomPropertyDrawer(typeof(SubclassSelectorAttribute))]
public class SubclassSelectorDrawer : PropertyDrawer
{
    private const string nullLabel = "<Null>";
    private const float spacing = 2f;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType != SerializedPropertyType.ManagedReference)
        {
            EditorGUI.LabelField(position, label.text, "Use with [SerializeReference] only");
            return;
        }
        string currentTypeName = GetCurrentTypeName(property);
        GUIContent buttonContent = new GUIContent(currentTypeName);
        float typeButtonWidth = EditorStyles.miniPullDown.CalcSize(buttonContent).x;

        EditorGUI.BeginProperty(position, label, property);

        float lineHeight = EditorGUIUtility.singleLineHeight;

        Rect headerRect = new Rect(position.x, position.y, position.width, lineHeight);

        Rect typeButtonRect = new Rect(
            headerRect.xMax - typeButtonWidth,
            headerRect.y,
            typeButtonWidth,
            headerRect.height);

        Rect foldoutRect = new Rect(
            headerRect.x,
            headerRect.y,
            typeButtonRect.xMax,
            headerRect.height);

        Rect labelRect = new Rect(
            headerRect.x,
            headerRect.y,
            typeButtonRect.x - headerRect.x - spacing,
            headerRect.height);
        
        if (EditorGUI.DropdownButton(typeButtonRect, new GUIContent(currentTypeName), FocusType.Passive))
        {
            ShowTypeMenu(property);
        }
        
        property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, GUIContent.none, true);
        EditorGUI.LabelField(labelRect, label);

        if (property.isExpanded && property.managedReferenceValue != null)
        {
            DrawChildProperties(position, property, headerRect.yMax + EditorGUIUtility.standardVerticalSpacing);
        }

        if (foldoutRect.Contains(Event.current.mousePosition))
        {
            GUI.Box(foldoutRect, GUIContent.none, EditorStyles.selectionRect);
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float height = EditorGUIUtility.singleLineHeight;

        if (property.isExpanded && property.managedReferenceValue != null)
        {
            SerializedProperty iterator = property.Copy();
            SerializedProperty end = iterator.GetEndProperty();

            bool enterChildren = true;
            while (iterator.NextVisible(enterChildren) &&
                   !SerializedProperty.EqualContents(iterator, end))
            {
                height += EditorGUIUtility.standardVerticalSpacing;
                height += EditorGUI.GetPropertyHeight(iterator, true);
                enterChildren = false;
            }
        }

        return height;
    }

    private static void DrawChildProperties(Rect position, SerializedProperty property, float startY)
    {
        SerializedProperty iterator = property.Copy();
        SerializedProperty end = iterator.GetEndProperty();

        float y = startY;
        bool enterChildren = true;

        while (iterator.NextVisible(enterChildren) &&
               !SerializedProperty.EqualContents(iterator, end))
        {
            float height = EditorGUI.GetPropertyHeight(iterator, true);
            Rect childRect = EditorGUI.IndentedRect(new Rect(position.x, y, position.width, height));
            EditorGUI.PropertyField(childRect, iterator, true);
            y += height + EditorGUIUtility.standardVerticalSpacing;
            enterChildren = false;
        }
    }

    private void ShowTypeMenu(SerializedProperty property)
    {
        Type baseType = GetBaseType(property);
        if (baseType == null)
            return;

        List<Type> types = GetConcreteTypes(baseType);
        GenericMenu menu = new GenericMenu();

        SerializedObject serializedObject = property.serializedObject;
        string propertyPath = property.propertyPath;

        menu.AddItem(
            new GUIContent(nullLabel),
            property.managedReferenceValue == null,
            () =>
            {
                SerializedProperty targetProperty = serializedObject.FindProperty(propertyPath);
                if (targetProperty == null)
                    return;

                serializedObject.Update();
                targetProperty.managedReferenceValue = null;
                serializedObject.ApplyModifiedProperties();
                serializedObject.Update();
            });

        foreach (Type type in types)
        {
            string displayName = GetTypeName(type);
            bool isCurrent = property.managedReferenceValue?.GetType() == type;
            Type capturedType = type;

            menu.AddItem(
                new GUIContent(displayName),
                isCurrent,
                () =>
                {
                    SerializedProperty targetProperty = serializedObject.FindProperty(propertyPath);
                    if (targetProperty == null)
                        return;

                    serializedObject.Update();
                    targetProperty.managedReferenceValue = Activator.CreateInstance(capturedType);
                    serializedObject.ApplyModifiedProperties();
                    serializedObject.Update();
                });
        }

        menu.ShowAsContext();
    }

    protected virtual string GetTypeName(Type type)
    {
        string name = type.Name;
        
        return name.EndsWith("Definition") ? name[..^"Definition".Length] : name;
    }
    
    private string GetCurrentTypeName(SerializedProperty property)
    {
        return property.managedReferenceValue == null
            ? nullLabel
            : GetTypeName(property.managedReferenceValue.GetType());
    }

    private Type GetBaseType(SerializedProperty property)
    {
        string typename = property.managedReferenceFieldTypename;
        if (!string.IsNullOrEmpty(typename))
        {
            string[] split = typename.Split(' ');
            if (split.Length == 2)
            {
                Type resolved = Type.GetType($"{split[1]}, {split[0]}");
                if (resolved != null)
                    return resolved;
            }
        }

        if (fieldInfo == null)
            return null;

        Type fieldType = fieldInfo.FieldType;

        if (fieldType.IsArray)
            return fieldType.GetElementType();

        if (fieldType.IsGenericType && fieldType.GetGenericTypeDefinition() == typeof(List<>))
            return fieldType.GetGenericArguments()[0];

        return fieldType;
    }

    protected virtual List<Type> GetConcreteTypes(Type baseType)
    {
        var list = TypeCache.GetTypesDerivedFrom(baseType)
            .Where(t =>
                !t.IsAbstract &&
                !t.IsInterface &&
                !t.IsGenericType &&
                !t.ContainsGenericParameters &&
                t.GetConstructor(Type.EmptyTypes) != null)
            .OrderBy(GetTypeName)
            .ToList();

        if (!baseType.IsAbstract &&
            !baseType.IsInterface &&
            !baseType.IsGenericType &&
            !baseType.ContainsGenericParameters &&
            baseType.GetConstructor(Type.EmptyTypes) != null)
        {
            list.Insert(0, baseType);
        }

        return list;
    }
}