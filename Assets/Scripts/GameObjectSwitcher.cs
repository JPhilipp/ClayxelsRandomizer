using UnityEngine;

public class GameObjectSwitcher : MonoBehaviour
{
    // Given a list of gameObjects, toggles setting one in the order
    // active by pressing left arrow/ right arrow.

    [SerializeField] GameObject[] gameObjects = null;
    
    int index = 0;

    void Start()
    {
        SetIndexByGameObjects();
        ActivateGameObjectsByIndex();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveIndex(-1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveIndex(1);
        }
    }

    void MoveIndex(int direction)
    {
        index += direction;
        if (index < 0)
        {
            index = gameObjects.Length - 1;
        }
        else if (index >= gameObjects.Length)
        {
            index = 0;
        }

        ActivateGameObjectsByIndex();
    }

    void ActivateGameObjectsByIndex()
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].gameObject.SetActive(i == index);
        }
    }

    void SetIndexByGameObjects()
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (gameObjects[i].activeSelf)
            {
                index = i;
                break;
            }
        }
    }
}
