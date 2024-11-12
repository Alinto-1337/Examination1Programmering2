using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(TagAttribute))]
public class TagAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Ensure the field is a string to store the tag name
        if (property.propertyType != SerializedPropertyType.String)
        {
            EditorGUI.PropertyField(position, property, label);
            return;
        }

        // Fetch all tags from Unity's tag manager
        string[] tags = UnityEditorInternal.InternalEditorUtility.tags;

        // Find the currently selected tag's index in the array
        int currentIndex = Mathf.Max(0, System.Array.IndexOf(tags, property.stringValue));

        // Display the tag dropdown
        int newIndex = EditorGUI.Popup(position, label.text, currentIndex, tags);

        // Update the property with the selected tag if changed
        if (newIndex != currentIndex)
        {
            property.stringValue = tags[newIndex];
        }
    }
}
