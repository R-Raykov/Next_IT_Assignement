using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorGenerator : MonoBehaviour, IGenerate
{
    [SerializeField] private List<GeneratableObject> objectsToGenerate;

    private void Start()
    {
        //Generate();
    }

    public void Generate()
    {
        foreach (GeneratableObject generatable in objectsToGenerate)
        {
            for (int i = 0; i < generatable.NumberOfObjects; i++)
            {
                if (generatable.GenerateOnHeight)
                {
                    GameObject newObject = Instantiate(generatable.Prefab, transform);

                    Vector3 _newPos = transform.forward * Random.Range(0.5f, 1.5f);
                    float _renderBounds = GetComponent<Renderer>().bounds.size.x;
                    _newPos.x = Random.Range(-_renderBounds, _renderBounds);

                    newObject.transform.localPosition = _newPos;
                    newObject.transform.LookAt(transform, Vector3.up);
                }
                else
                {
                    GameObject newObject = Instantiate(generatable.Prefab, transform);

                    Vector3 _newPos = transform.forward * Random.Range(0.5f, 1.5f);
                    float _renderBounds = GetComponent<Renderer>().bounds.size.x;
                    _newPos.x = Random.Range(-_renderBounds, _renderBounds);

                    newObject.transform.localPosition = _newPos;
                    newObject.transform.LookAt(transform, Vector3.up);
                }
            }
        }
    }
}
