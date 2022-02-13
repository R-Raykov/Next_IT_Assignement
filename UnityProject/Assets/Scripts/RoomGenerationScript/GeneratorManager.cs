using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct GeneratableObject
{
    public int NumberOfObjects;
    public bool GenerateOnHeight;
    public GameObject Prefab;
}

public class GeneratorManager : MonoBehaviour
{
    public enum ObjectType { Shelf, Bed, Desk, Wardrobe, Carpet, Chest}

    private static GeneratorManager instance;
    public static GeneratorManager Instance { get => instance; }

    public List<GameObject> SeparatingWalls;

    [SerializeField] private List<GeneratableObject> generators;

    [Range(1, 10)]
    [SerializeField] private int verticalLenght;
    [Range(1, 10)]
    [SerializeField] private int horizontalLenght;

    private void Awake()
    {
        instance = this;
        SeparatingWalls = new List<GameObject>();
    }

    private void Start()
    {
        //foreach (IGenerate generator in generators)
        //{
        //    generator.Generate();
        //}
    }

    public int GetVerticalLenght { get => verticalLenght; }

    public int GetHorizontalLenght { get => horizontalLenght; }

    public List<GeneratableObject> GetGenerators { get => generators; }
}
