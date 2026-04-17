using System;
using System.Collections.Generic;
using System.Linq;
using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Interfaces;
using InflationSurvivor.Core;
using InflationSurvivor.Core.Attributes.Editor;
using UnityEditor;
using UnityEngine;

namespace InflationSurvivor.Combat.Attributes.Editor;

[CustomPropertyDrawer(typeof(FormulaSelectorAttribute))]
public class FormulaSelectorDrawer : SubclassSelectorDrawer
{
    private static readonly Dictionary<Type, List<Type>> _formulaChildrenTypes = new Dictionary<Type, List<Type>>();
    
    protected override List<Type> GetConcreteTypes(Type baseType)
    {
        var types = base.GetConcreteTypes(baseType);
        if (!baseType.IsGenericType || baseType.GetGenericTypeDefinition() != typeof(IFormulaDefinition<>))
            return types;
        
        Type contextType = baseType.GetGenericArguments()[0];

        if (!_formulaChildrenTypes.ContainsKey(contextType))
        {
            GenerateChildrenTypes(contextType);
        }
        
        types.AddRange(_formulaChildrenTypes[contextType]);

        return types;
    }

    protected override string GetTypeName(Type type)
    {
        string name = type.Name;
        
        if (type.IsGenericType)
        {
            name = name.Contains("`") ? name[..^(name.IndexOf('`')-1)] : name;
        }
        
        return name.EndsWith("Definition") ? name[..^"Definition".Length] : name;
    }

    [InitializeOnLoadMethod]
    private static void InitializeOnLoadMethod()
    {
        _formulaChildrenTypes.Clear();
        GenerateChildrenTypes(typeof(SkillContext));
        GenerateChildrenTypes(typeof(StatusEffectContext));
    }

    private static void GenerateChildrenTypes(Type contextType)
    {
        _formulaChildrenTypes[contextType] = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(t => t.IsGenericTypeDefinition && 
                        !t.IsAbstract &&
                        !t.IsInterface &&
                        t.GetInterfaces().Any(i =>
                            i.IsGenericType &&
                            i.GetGenericTypeDefinition() == typeof(IFormulaDefinition<>)))
            .Select(t => t.MakeGenericType(contextType))
            .ToList();
    }
}