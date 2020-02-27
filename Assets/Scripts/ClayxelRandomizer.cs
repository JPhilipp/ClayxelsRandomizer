using UnityEngine;

[RequireComponent(typeof(ClayObjectRandomizationStrength))]

public class ClayxelRandomizer : MonoBehaviour
{
    // Can be attached to a Clayxels container to randomize the ClayObjects
    // position, rotation, color and such.

    [Tooltip("How long to wait between randomizing again.")]
    [SerializeField] float secondsPerRound = 0.5f;

    ClayObject[] referenceClayObjects = null;
    ClayObject[] randomizedClayObjects = null;

    float lastRandomizationTime = -1f;
    bool active = true;

    ClayObjectRandomizationStrength defaultStrength = null;

    void Start()
    {
        randomizedClayObjects = GetComponentsInChildren<ClayObject>();
        CreateReferenceClayxels();

        InitDefaultStrength();

        RandomizeClayxels();
    }

    void CreateReferenceClayxels()
    {
        GameObject reference = Instantiate(gameObject);
        reference.name = gameObject.name + " (Reference)";

        Destroy(reference.GetComponentInChildren<Clayxel>());        
        Destroy(reference.GetComponent<ClayxelRandomizer>());
        Destroy(reference.GetComponent<ClayObjectRandomizationStrength>());

        referenceClayObjects = reference.GetComponentsInChildren<ClayObject>();

        reference.SetActive(false);
    }

    void InitDefaultStrength()
    {
        defaultStrength = GetComponent<ClayObjectRandomizationStrength>();
        if (defaultStrength == null || !defaultStrength.enabled)
        {
            defaultStrength = gameObject.AddComponent<ClayObjectRandomizationStrength>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            active = !active;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RandomizeClayxels();
        }

        if (active && Time.time >= lastRandomizationTime + secondsPerRound)
        {
            RandomizeClayxels();
        }
    }

    void RandomizeClayxels()
    {
        lastRandomizationTime = Time.time;

        ResetClayxels();
        
        foreach (ClayObject clayObject in randomizedClayObjects)
        {
            ClayObjectRandomizationStrength strength = clayObject.GetComponent<ClayObjectRandomizationStrength>();
            if (strength == null) { strength = defaultStrength; }

            clayObject.transform.localPosition = RandomizeNonZeroOfVector3(
                clayObject.transform.position, 0.25f * strength.baseStrength * strength.position);
            clayObject.transform.localEulerAngles = RandomizeNonZeroOfVector3(
                clayObject.transform.localEulerAngles, 5f * strength.baseStrength * strength.rotation);
            clayObject.transform.localScale = RandomizeVector3(
                clayObject.transform.localScale, 0.25f * strength.baseStrength * strength.scale);

            clayObject.color = RandomizeColor(
                clayObject.color, 0.15f * strength.baseStrength * strength.color);

            clayObject.blend = RandomizeFloat(
                clayObject.blend, 0.1f * strength.baseStrength * strength.blend);
        }
    }

    float RandomizeFloat(float value, float max)
    {
        return value + RandomRange(max);
    }

    Color RandomizeColor(Color color, float max)
    {
        const float colorMax = 1f;
        return new Color(
            Mathf.Clamp(color.r + RandomRange(max), 0f, colorMax),
            Mathf.Clamp(color.g + RandomRange(max), 0f, colorMax),
            Mathf.Clamp(color.b + RandomRange(max), 0f, colorMax),
            color.a
        );
    }

    Vector3 RandomizeVector3(Vector3 vector3, float max)
    {
        return vector3 + new Vector3(
            RandomRange(max),
            RandomRange(max),
            RandomRange(max)
        );
    }

    float RandomRange(float max)
    {
        return UnityEngine.Random.Range(-max, max);
    }

    Vector3 RandomizeNonZeroOfVector3(Vector3 vector3, float max)
    {
        return vector3 + new Vector3(
            vector3.x != 0f ? RandomRange(max) : 0f,
            vector3.y != 0f ? RandomRange(max) : 0f,
            vector3.z != 0f ? RandomRange(max) : 0f
        );
    }

    void ResetClayxels()
    {
        for (int i = 0; i < referenceClayObjects.Length; i++)
        {
            SetClayObjectFromSource(referenceClayObjects[i], randomizedClayObjects[i]);
        }
    }

    void SetClayObjectFromSource(ClayObject source, ClayObject target)
    {
        target.transform.position = source.transform.position;
        target.transform.rotation = source.transform.rotation;
        target.transform.localScale = source.transform.localScale;

        target.color = source.color;

        target.blend = source.blend;
    }
}