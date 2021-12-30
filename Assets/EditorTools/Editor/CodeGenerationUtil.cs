using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;


namespace Platinio.SDK.EditorTools
{
    public static class CodeGenerationUtil
    {
        public static void CreateEnumScript(List<string> values, string path, string enumName, string nameSpace = "Platinio")
        {
            string script = "namespace " + nameSpace +
                            "{ " +
                            "public enum " + enumName +
                            "{";

            if (values.Count > 0)
            {
                for (int n = 0; n <values.Count; n++)
                {
                    script += values[n];

                    if (n != values.Count - 1) script += ",";
                }
            }
            else
            {
                script += "Empty";
            }


            script += "}";
            script += "}";

            Debug.Log("File Created at path: " + Path.GetDirectoryName( Application.dataPath ) + "\\Assets\\" + path);
            File.WriteAllText( Path.GetDirectoryName( Application.dataPath ) + "\\Assets\\" + path, script );

            //rebuild
            AssetDatabase.Refresh();
            AssetDatabase.ImportAsset( path, ImportAssetOptions.ForceUpdate );
        }
    }

}

