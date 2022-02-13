using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGenerator : MonoBehaviour, IGenerate
{
    [SerializeField] private List<GeneratableObject> objectsToGenerate;
    private RaycastHit hit;

    private List<GameObject> possibleLocations;

    protected void Start()
    {
        //possibleLocations = new List<GameObject>();
        //possibleLocations.AddRange(GameObject.FindGameObjectsWithTag("Wall"));

        //Generate();

    }

    public void Generate()
    {
        GeneratableObject _generatedObject = GenerateRandomItem();
        GameObject _newItem = Instantiate(_generatedObject.Prefab, transform);
        Vector3 _newPos = Vector3.zero;
        if (_generatedObject.GenerateOnHeight)
            _newPos.y = 0.5f;
        
        _newItem.transform.localPosition = _newPos;
    }

    private GeneratableObject GenerateRandomItem()
    {
        int _count = GeneratorManager.Instance.GetGenerators.Count;

        int _rng = Random.Range(0, _count);

        GeneratableObject generatable = GeneratorManager.Instance.GetGenerators[_rng];

        return generatable;
    }

}
