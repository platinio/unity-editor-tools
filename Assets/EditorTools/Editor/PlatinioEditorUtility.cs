
namespace Platinio.SDK.EditorTools
{
    public static class PlatinioEditorUtility
    {
        public static string ConvertToInspectorName(string varName)
        {
            string inspectorName = varName.Replace("m_", string.Empty).Replace("_", string.Empty);
            inspectorName = $"{char.ToUpper(inspectorName[0])}{inspectorName.Substring(1)}";
          
            for (int n = inspectorName.Length - 1; n > 0; n--)
            {
                if (char.IsUpper(inspectorName[n]))
                {
                    inspectorName = $"{inspectorName.Substring(0, n)} {inspectorName.Substring(n, inspectorName.Length - n )}";
                   
                }
            }
         
            
            return inspectorName;
        }
    }

}

