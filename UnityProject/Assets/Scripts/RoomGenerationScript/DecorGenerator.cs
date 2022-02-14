using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorGenerator : MonoBehaviour, IGenerate
{
    [SerializeField] private List<GeneratableObject> objectsToGenerate;

    [SerializeField] private bool isShelf;

    public void Generate()
    {
        foreach (GeneratableObject generatable in objectsToGenerate)
        {
            if (generatable.GenerateOnHeight)
            {
                int _generationThreshhold = Random.Range(0, 100);
                if (_generationThreshhold < 70)
                    return;

                GameObject newObject = Instantiate(generatable.Prefab, transform);
                Vector3 _newPos = Vector3.zero;
                Vector3 _renderBounds = GetComponent<Renderer>().bounds.extents;
                
                _newPos.y = _renderBounds.y;

                if (!isShelf)
                { 
                    _newPos.x = Random.Range(-_renderBounds.x, _renderBounds.x);
                    _newPos.z = Random.Range(-_renderBounds.z, _renderBounds.z);
                }
                else
                {
                    _newPos.z = Random.Range(-_renderBounds.x, _renderBounds.x);
                    _newPos.x = Random.Range(-_renderBounds.z, _renderBounds.z);
                }

                newObject.transform.localPosition = _newPos;

                break;
            }
            else
            {
                GameObject newObject = Instantiate(generatable.Prefab, transform);

                Vector3 _newPos = transform.forward * Random.Range(0.5f, 1.5f);
                float _renderBounds = GetComponent<Renderer>().bounds.extents.x;
                _newPos.x = Random.Range(-_renderBounds, _renderBounds);

                newObject.transform.localPosition = _newPos;
                newObject.transform.LookAt(transform, Vector3.up);
            }
        }
    }
}
