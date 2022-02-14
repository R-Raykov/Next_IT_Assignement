using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorGenerator : MonoBehaviour, IGeneratable
{
    [SerializeField] private List<GeneratableObject> objectsToGenerate;

    [SerializeField] private bool isShelf;

    public void Generate()
    {
        foreach (GeneratableObject generatable in objectsToGenerate)
        {
            //This is used to add candels to surfaces
            if (generatable.GenerateOnHeight)
            {
                if (GameManager.Instance.GetRandomNumber() < 60)
                    return;

                GameObject newObject = Instantiate(generatable.Prefab, transform);
                Vector3 _newPos = Vector3.zero;
                Vector3 _renderBounds = GetComponent<Renderer>().bounds.extents;
                
                _newPos.y = _renderBounds.y;

                // make sure it randomly places it within the extents of the renderer
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
                // this is used just to generate the chair for the desk
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
