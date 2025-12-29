using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace adfa.Utility.Attributes
{
    [CustomPropertyDrawer(typeof(SubclassSelectorAttribute))]
    public class SubclassSelectorDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            if (property.propertyType != SerializedPropertyType.ManagedReference)
            {
                return new Label("SubclassSelector can only be used on [SerializeReference] fields.");
            }

            var container = new VisualElement();
            var (typeOptions, typeMap) = GetTypeOptions(property);
            string currentTypeName = GetTypeName(property.managedReferenceValue?.GetType());
            var dropdown = new PopupField<string>(property.displayName, typeOptions, currentTypeName);

            var fieldsContainer = new VisualElement
            {
                style =
                {
                    paddingLeft = 15
                }
            };

            container.Add(dropdown);
            container.Add(fieldsContainer);

            dropdown.RegisterValueChangedCallback(evt =>
            {
                Type selectedType = typeMap[evt.newValue];
                SetManagedReference(property, selectedType);
                RebuildPropertyUI(fieldsContainer, property);
            });

            RebuildPropertyUI(fieldsContainer, property);

            return container;
        }

        private void RebuildPropertyUI(VisualElement container, SerializedProperty property)
        {
            container.Clear();
            if (property.managedReferenceValue != null)
            {
                foreach (var child in GetChildren(property))
                {
                    container.Add(new PropertyField(child));
                }
            }
        }

        private (List<string> typeOptions, Dictionary<string, Type> typeMap) GetTypeOptions(SerializedProperty property)
        {
            var typeOptions = new List<string> { "(Null)" };
            var typeMap = new Dictionary<string, Type> { { "(Null)", null } };

            Type baseType = GetBaseType(property);
            if (baseType == null) return (typeOptions, typeMap);

            var assignableTypes = TypeCache.GetTypesDerivedFrom(baseType)
                .Where(t =>
                    t.IsClass &&
                    !t.IsAbstract &&
                    !t.IsGenericTypeDefinition &&
                    t.IsSerializable
                );

            foreach (var type in assignableTypes.OrderBy(t => t.Name))
            {
                string name = GetTypeName(type);
                if (!typeMap.ContainsKey(name))
                {
                    typeOptions.Add(name);
                    typeMap[name] = type;
                }
            }

            return (typeOptions, typeMap);
        }

        private void SetManagedReference(SerializedProperty property, Type type)
        {
            object instance = (type != null) ? Activator.CreateInstance(type) : null;
            property.managedReferenceValue = instance;
            property.serializedObject.ApplyModifiedProperties();
        }

        private Type GetBaseType(SerializedProperty property)
        {
            string typeName = property.managedReferenceFieldTypename;
            if (string.IsNullOrEmpty(typeName)) return null;

            var parts = typeName.Split(' ');
            if (parts.Length < 2) return null;
            
            string assemblyQualifiedName = parts[1] + ", " + parts[0];
            return Type.GetType(assemblyQualifiedName);
        }

        private string GetTypeName(Type type)
        {
            if (type == null) return "(Null)";
            return type.Name;
        }

        private static IEnumerable<SerializedProperty> GetChildren(SerializedProperty property)
        {
            var currentProperty = property.Copy();
            var nextSiblingProperty = property.Copy();
            nextSiblingProperty.Next(false);

            if (currentProperty.Next(true))
            {
                do
                {
                    if (SerializedProperty.EqualContents(currentProperty, nextSiblingProperty))
                        break;
                    yield return currentProperty;
                }
                while (currentProperty.Next(false));
            }
        }
    }
}