using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Platinio.SDK.EditorTools
{
    public static class PlatinioEditorGUILayout
    {
        /// <summary>
        /// Create a property field from a SerializedProperty
        /// </summary>
        public static void PropertyField(SerializedProperty property, string label = null, bool showChildren = false)
        {
            EditorGUILayout.PropertyField(property, new GUIContent( label == null ? property.displayName : label ), showChildren);
        }
        
        /// <summary>
        /// Create a property field from a SerializedProperty
        /// </summary>
        public static void PropertyField(SerializedProperty property, bool showChildren)
        {
            PropertyField(property, null, showChildren);
        }

        /// <summary>
        /// Create a float slider in the inspector
        /// </summary>
        /// <param name="min">Min slider value</param>
        /// <param name="max">Max slider value</param>
        /// <param name="minLimit">Min slider limit</param>
        /// <param name="maxLimit">Max slider limit</param>
        public static void MinMaxFloatSlider(SerializedProperty min, SerializedProperty max, float minLimit, float maxLimit)
        {
            PropertyField(min);
            PropertyField(max);

            float minValue = min.floatValue;
            float maxValue = max.floatValue;

            EditorGUILayout.MinMaxSlider(ref minValue, ref maxValue, minLimit, maxLimit);

            min.floatValue = minValue;
            max.floatValue = maxValue;
        }
        
        /// <summary>
        /// Creates a foldout style label with indent value
        /// </summary>
        /// <returns>Current foldout value</returns>
        public static bool Foldout(bool foldout, GUIContent content, int indent)
        {
            GUIStyle style = new GUIStyle(EditorStyles.foldout);
            style.fontStyle = FontStyle.Bold;
            style.fontSize = 12;
            style.active.textColor = Color.black;
            style.focused.textColor = Color.black;
            style.onHover.textColor = Color.black;
            style.normal.textColor = Color.black;
            style.onNormal.textColor = Color.black;
            style.onActive.textColor = Color.black;
            style.onFocused.textColor = Color.black;

            Rect rect = GUILayoutUtility.GetRect(40f, 40f, 16f, 16f);
            rect.x += indent * 20;
            return EditorGUI.Foldout(rect, foldout, content , style);
        }
        
       /// <summary>
       /// Creates a foldout style label with indent value
       /// </summary>
       /// <param name="title">Title</param>
       /// <param name="foldout">Foldout value</param>
       /// <param name="indent">Indent level</param>
       /// <param name="drawCallback">Callback called when it is foldout</param>
        public static void Foldout(GUIContent title, ref bool foldout, int indent , Action drawCallback)
        {
            foldout = Foldout(foldout, title , indent);

            if (foldout)
            {
                drawCallback();
            }
        }

       /// <summary>
       /// Creates a foldout style label with indent value
       /// </summary>
       /// <param name="title">Title</param>
       /// <param name="foldout">Foldout value</param>
       /// <param name="drawCallback">Callback called when it is foldout</param>
        public static void Foldout(GUIContent title, ref bool foldout, Action drawCallback)
        {
            Foldout(title, ref foldout, 0, drawCallback);
        }

        /// <summary>
        /// Add space in the inspector
        /// </summary>
        public static void Space(int height)
        {
            for (int n = 0; n < height; n++) EditorGUILayout.Space();
        }

        /// <summary>
        /// Draws a title label
        /// </summary>
        public static void DrawTittle(string text)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField( text, EditorStyles.boldLabel );
            EditorGUILayout.Space();
        }
        
        /// <summary>
        /// Draws a grid of buttons in the inspector
        /// </summary>
        /// <returns>Current selected button index</returns>
        public static int DrawGridButtons(int selection, int xSize, params GUIContent[] labels)
        {
            EditorGUIUtility.SetIconSize( new Vector2(20.0f , 20.0f));
            GUIStyle SelectionGridStyle = new GUIStyle(EditorStyles.miniButton);
            SelectionGridStyle.fixedHeight = 35;

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            int newSelection = GUILayout.SelectionGrid(selection, labels, xSize, SelectionGridStyle, GUILayout.Height(68), GUILayout.Width(85 * Screen.width / Screen.dpi));

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            return newSelection;
        }

        /// <summary>
        /// Draws a tooltip box in the inspector
        /// </summary>
        public static void DrawTooltipBox(Texture icon , string title , string text)
        {
            EditorGUILayout.BeginVertical("Box");
            var style = new GUIStyle(EditorStyles.boldLabel) { alignment = TextAnchor.MiddleCenter };
            EditorGUILayout.LabelField(new GUIContent(icon), style, GUILayout.ExpandWidth(true), GUILayout.Height(32));
            EditorGUILayout.LabelField(title, style, GUILayout.ExpandWidth(true));

            style = EditorStyles.helpBox;
            style.richText = true;
            style.fontSize = 11;

            EditorGUILayout.LabelField(text , style );
            EditorGUILayout.EndVertical();
            GUILayout.FlexibleSpace();
        }

        public static void DrawTooltipBox(MessageType messageType, string title, string text)
        {
            DrawTooltipBox(LoadIcon(messageType.ToString()), title, text);
        }

        /// <summary>
        /// Loads editor icons from "Editor/Icons/"
        /// </summary>
        public static Texture LoadIcon(string iconName)
        {
            return Resources.Load("Editor/Icons/" + iconName) as Texture;
        }

        /// <summary>
        /// Draws a console in the inspector
        /// </summary>
        /// <param name="consoleMessageList">messages for the user</param>
        public static void DrawConsole(List<ConsoleMessage> consoleMessageList)
        {
            for (int n = 0; n < consoleMessageList.Count; n++)
            {
                EditorGUILayout.HelpBox( consoleMessageList[n].text , consoleMessageList[n].messageType );
            }
        }

        /// <summary>
        /// Sometimes is better to use a enum dropdown Yes/No instead of a checkbox
        /// </summary>
        public static bool DrawBoolEnum(GUIContent content, bool value)
        {
            return ((BoolEnum)EditorGUILayout.EnumPopup(content, value? BoolEnum.Yes : BoolEnum.No)) == BoolEnum.Yes;
        }

        /// <summary>
        /// Sometimes is better to use a enum dropdown Yes/No instead of a checkbox
        /// </summary>
        public static bool DrawBoolEnum(GUIContent content, Rect rect , bool value)
        {
            return ((BoolEnum)EditorGUI.EnumPopup(rect , content, value ? BoolEnum.Yes : BoolEnum.No )) == BoolEnum.Yes;
        }
        
        public static void FoldoutInspector(GUIContent title, ref bool foldout, int indent , Action drawCallback)
        {
            foldout = Foldout(foldout, title , indent);

            if (foldout)
            {
                drawCallback();
            }
        }

        public static void FoldoutInspector(GUIContent title, ref bool foldout, Action drawCallback)
        {
            FoldoutInspector(title, ref foldout, 0, drawCallback);
        }
    }
    
    public class ConsoleMessage
    {
        public string text;
        public MessageType messageType;

        public ConsoleMessage(string text , MessageType messageType)
        {
            this.text = text;
            this.messageType = messageType;
        }
    }
    
    public enum BoolEnum
    {
        Yes,
        No
    }

}

