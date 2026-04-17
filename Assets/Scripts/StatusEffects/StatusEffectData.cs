using CustomInspector;
using InflationSurvivor.StatusEffect;
using UnityEngine;

namespace InflationSurvivor.StatusEffects;

public abstract class StatusEffectData : ScriptableObject
{
    public string ID {
        get
        {
            if (!string.IsNullOrEmpty(id))
            {
                return id;
            }
            return uniqueID;
        }
    }
    
    [SerializeField] private string id;
    [SerializeField, ReadOnly] private string uniqueID;
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [SerializeField] private float duration;
    [SerializeField] private int maxStack;
    
    public abstract float Power { get; }

    public void AddEffect(StatusEffectManager manager)
    {
        if (!manager.TryGetStatusEffect(ID, out StatusEffectInstance effect))
        {
            manager.AddStatusEffect(ID, CreateInstance(1, duration));
            return;
        }
        
        int stack = Mathf.Min(effect.Stack + 1, maxStack);
        float newDuration = Mathf.Max(effect.RemainingTime, duration);

        if (effect.Power <= Power)
        {
            effect = CreateInstance(stack, newDuration);
            manager.ChangeStatusEffect(ID, effect);
        }
        else
        {
            effect.Refresh(stack, newDuration);
        }
    }
    
    protected abstract StatusEffectInstance CreateInstance(int stack, float duration);

#if UNITY_EDITOR
    private void OnValidate()
    {
        string path = UnityEditor.AssetDatabase.GetAssetPath(this);
        string guid = UnityEditor.AssetDatabase.AssetPathToGUID(path);

        if (!string.IsNullOrEmpty(guid) && uniqueID != guid)
        {
            uniqueID = guid;
        }
    }
#endif
}