using UnityEngine;

public class ClayObjectRandomizationStrength : MonoBehaviour
{
    // Allows adjusting of randomization strength via multipliers
    // on a global as well as per-ClayObject basis.

    [Space]
    [Header("Note you can also add this component to individual ClayObjects.")]

    [Tooltip("The base randomization range multiplier applied to all properties. Higher values increase randomization.")]
    public float baseStrength = 1f;
    
    [Space]

    [Header("Individual Properties")]

    [Tooltip("The position randomization range multiplier. Set 0 to disable.")]
    public float position = 1f;

    [Tooltip("The rotation randomization range multiplier. Set 0 to disable.")]
    public float rotation = 1f;
    
    [Tooltip("The scale randomization range multiplier. Set 0 to disable.")]
    public float scale = 1f;
    
    [Space]
    
    [Tooltip("The color randomization range multiplier. Set 0 to disable.")]
    public float color = 1f;

    [Space]

    [Tooltip("The blend randomization range multiplier. Set 0 to disable.")]
    public float blend = 0f;
    
    /*
    // Maybe for later.
    [Space]
    public float round = 1f;
    public float radius = 1f;
    public float fat = 1f;
    */
}
