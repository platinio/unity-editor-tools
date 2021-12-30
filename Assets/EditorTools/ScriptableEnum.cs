using System.Collections.Generic;
using UnityEngine;

namespace Platinio.SDK.EditorTools
{
    [CreateAssetMenu(fileName = "Enum" , menuName = "Platinio/Scriptable Enum")]
    public class ScriptableEnum : ScriptableObject
    {
        [SerializeField] private string m_enumName = null;
        [SerializeField] private string m_path = null;
        [SerializeField] private string m_nameSpace = null;
        [SerializeField] private List<string> m_enumValues;

        public string EnumName => m_enumName;
        public string Path => m_path;
        public string NameSpace => m_nameSpace;
        public List<string> EnumValues => m_enumValues;

    }
}