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

    private int verticalLenght;
    private int horizontalLenght;

    private void Awake()
    {
        instance = this;
        SeparatingWalls = new List<GameObject>();
        verticalLenght = Random.Range(5, 9);
        horizontalLenght = Random.Range(2, 6);
    }

    public int GetVerticalLenght { get => verticalLenght; }

    public int GetHorizontalLenght { get => horizontalLenght; }

    public List<GeneratableObject> GetGenerators { get => generators; }

    public GeneratableObject GenerateRandomItem()
    {
        int _count = GeneratorManager.Instance.GetGenerators.Count;

        int _rng = Random.Range(0, _count);

        if (GeneratorManager.Instance.GetGenerators[_rng].Name.Equals("Carpet"))
        {
            _rng -= 1;
        }

        GeneratableObject generatable = GeneratorManager.Instance.GetGenerators[_rng];

        return generatable;
    }

    public GeneratableObject GetItem(string pItem)
    {
        foreach (GeneratableObject generatableObject in GeneratorManager.Instance.GetGenerators)
        {
            if (generatableObject.Name.Equals(pItem))
                return generatableObject;
        }
        return new GeneratableObject();
    }
}
