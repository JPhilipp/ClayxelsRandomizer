using UnityEngine;

[RequireComponent(typeof(ClayObjectRandomizationStrength))]

public class ClayxelRandomizer : MonoBehaviour
{
    // Can be attached to a Clayxels container to randomize the ClayObjects
    // position, rotation, color and such.

    [Tooltip("How long to wait between randomizing again.")]
    [SerializeField] float secondsPerRound = 1f;

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
            RandomizeClayxel(clayObject);
        }
    }

    void RandomizeClayxel(ClayObject clay)
    {
        ClayObjectRandomizationStrength strength =
            GetAddComponent<ClayObjectRandomizationStrength>(clay.gameObject);

        clay.transform.localPosition = RandomizeNonZeroOfVector3(
            clay.transform.localPosition,
            0.25f * defaultStrength.all * defaultStrength.position * strength.all * strength.position);
        
        clay.transform.localEulerAngles = RandomizeNonZeroOfVector3(
            clay.transform.localEulerAngles,
            5f * defaultStrength.all * defaultStrength.rotation * strength.all * strength.rotation);
        
        clay.transform.localScale = RandomizeVector3(
            clay.transform.localScale,
            0.25f * defaultStrength.all * defaultStrength.scale * strength.all * strength.scale);

        clay.color = RandomizeColor(
            clay.color,
            0.15f * defaultStrength.all * defaultStrength.color * strength.all * strength.color);

        clay.blend = RandomizeFloat(
            clay.blend,
            0.1f * defaultStrength.all * defaultStrength.blend * strength.all * strength.blend);
    }

    T GetAddComponent<T>(GameObject thisObject) where T : Component
    {
        Component component = thisObject.GetComponent<T>();
        if (component == null)
        {
            component = thisObject.AddComponent<T>();
        }
        return (T) component;
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

    Vector3 RandomizeNonZeroOfVector3(Vector3 vector3, float max)
    {
        return vector3 + new Vector3(
            vector3.x != 0f ? RandomRange(max) : 0f,
            vector3.y != 0f ? RandomRange(max) : 0f,
            vector3.z != 0f ? RandomRange(max) : 0f
        );
    }

    float RandomRange(float max)
    {
        return UnityEngine.Random.Range(-max, max);
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
        target.transform.localPosition = source.transform.localPosition;
        target.transform.localRotation = source.transform.localRotation;
        target.transform.localScale = source.transform.localScale;

        target.color = source.color;

        target.blend = source.blend;
    }
}