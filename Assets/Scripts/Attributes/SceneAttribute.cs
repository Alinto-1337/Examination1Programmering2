using UnityEngine;
using UnityEngine.Scripting;

[RequireAttributeUsages()]
public class SceneAttribute : PropertyAttribute
{
    // This attribute makes a Object input field.. the Editor code then extracts the scene name and assigns it to the string field
    // Genuinely Dont understand why this isn't a feature in Unity already...
}
