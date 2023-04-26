using UnityEngine;
using Utils;

namespace TRYINGSTUFFOUT.CursesV2
{
    [CreateAssetMenu]
    public class CurseModifier : ScriptableObject
    {
        public Stats stat;
        public ValueType statValueType;
        public float value;
        public string description;
        public float timer;

        public enum ValueType
        {
            Numeric,
            Boolean
        }
    }
}