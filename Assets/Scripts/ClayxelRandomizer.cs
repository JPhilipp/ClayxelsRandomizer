﻿using UnityEngine;

public class ClayxelRandomizer : MonoBehaviour
{
    [SerializeField] Transform referenceClayxelsRoot = null;
    [SerializeField] Transform randomizedClayxelsRoot = null;
    [Space]
    [SerializeField] float randomizationStrength = 1f;
    [SerializeField] float secondsPerRound = 0.5f;
    [Space]
    [SerializeField] bool randomizeBlend = false;
    [SerializeField] bool randomizeRound = false;

    ClayObject[] referenceClayObjects = null;
    ClayObject[] randomizedClayObjects = null;

    float lastRandomizationTime = -1f;
    bool active = true;

    ClayObjectRandomizationStrength defaultStrength = null;

    void Start()
    {
        referenceClayxelsRoot.gameObject.SetActive(false);
        randomizedClayxelsRoot.gameObject.SetActive(true);

        referenceClayObjects = referenceClayxelsRoot.GetComponentsInChildren<ClayObject>(true);
        randomizedClayObjects = randomizedClayxelsRoot.GetComponentsInChildren<ClayObject>(true);

        defaultStrength = GetComponent<ClayObjectRandomizationStrength>();
        if (defaultStrength == null || !defaultStrength.enabled)
        {
            defaultStrength = gameObject.AddComponent<ClayObjectRandomizationStrength>();
        }

        RandomizeClayxels();
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
                clayObject.transform.position, 0.25f * randomizationStrength * strength.position);
            clayObject.transform.localEulerAngles = RandomizeNonZeroOfVector3(
                clayObject.transform.localEulerAngles, 5f * randomizationStrength * strength.rotation);
            clayObject.transform.localScale = RandomizeVector3(
                clayObject.transform.localScale, 0.25f * randomizationStrength * strength.scale);

            clayObject.color = RandomizeColor(
                clayObject.color, 0.15f * randomizationStrength * strength.color);

            if (randomizeBlend)
            {
                clayObject.blend = RandomizeFloat(
                    clayObject.blend, 0.1f * randomizationStrength * strength.blend);
            }
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
