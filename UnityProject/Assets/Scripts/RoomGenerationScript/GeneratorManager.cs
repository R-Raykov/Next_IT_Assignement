using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct GeneratableObject
{
    public bool GenerateOnHeight;
    public GameObject Prefab;
    public string Name;
}

public class GeneratorManager : MonoBehaviour
{
    public enum ObjectType { Shelf, Bed, Desk, Wardrobe, Carpet, Chest}

    private static GeneratorManager instance;
    public static GeneratorManager Instance { get => instance; }

    public List<GameObject> SeparatingWalls;

    [SerializeField] private List<GeneratableObject> generators;

    [SerializeField] private GenerateRoom roomGenerator;

    private int verticalLenght;
    private int horizontalLenght;

    private void Awake()
    {
        instance = this;
        SeparatingWalls = new List<GameObject>();
        RandomizeRoomSize();
    }

    public int GetVerticalLenght { get => verticalLenght; }

    public int GetHorizontalLenght { get => horizontalLenght; }

    /// <summary>
    /// Gets the generator list. Note, generators is not the best name as most of these objects don't generate anything
    /// </summary>
    public List<GeneratableObject> GetGenerators { get => generators; }

    /// <summary>
    /// Randomize the size of the room
    /// </summary>
    private void RandomizeRoomSize()
    {
        verticalLenght = Random.Range(2, 9);
        horizontalLenght = Random.Range(2, 9);
    }

    /// <summary>
    /// Clear everything from a previous random room and generate a new room
    /// </summary>
    public void GenerateNewRoom()
    {
        RandomizeRoomSize();

        SeparatingWalls.Clear();

        for (int i = 0; i < roomGenerator.transform.childCount; i++)
        {
            Destroy(roomGenerator.transform.GetChild(i).gameObject);
        }

        roomGenerator.Generate();
    }

    /// <summary>
    /// Get a random item from the generators list, excluding the carpet 
    /// Carpets are added on ground tiles and everything else is added onto walls
    /// </summary>
    public GeneratableObject GenerateRandomItem()
    {
        int _count = GetGenerators.Count;

        int _rng = Random.Range(0, _count);

        if (GetGenerators[_rng].Name.Equals("Carpet"))
        {
            _rng -= 1;
        }

        GeneratableObject generatable = GetGenerators[_rng];

        return generatable;
    }

    /// <summary>
    /// Get an item by name
    /// </summary>
    /// <param name="pItem">name of item to return</param>
    public GeneratableObject GetItem(string pItem)
    {
        foreach (GeneratableObject generatableObject in GetGenerators)
        {
            if (generatableObject.Name.Equals(pItem))
                return generatableObject;
        }
        return new GeneratableObject();
    }
}
