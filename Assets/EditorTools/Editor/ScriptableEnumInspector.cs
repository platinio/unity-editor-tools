using UnityEditor;
using UnityEngine;

namespace Platinio.SDK.EditorTools
{
    [CustomEditor(typeof(ScriptableEnum))]
    public class ScriptableEnumInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Create Enum Script"))
            {
                var scriptableEnum = target as ScriptableEnum;
                CodeGenerationUtil.CreateEnumScript(scriptableEnum.EnumValues, scriptableEnum.Path, scriptableEnum.EnumName, scriptableEnum.NameSpace);
            }
        }
    }

}

