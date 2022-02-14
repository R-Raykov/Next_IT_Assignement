using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InteriorWallGenerator : MonoBehaviour, IGeneratable
{
    public enum WallType { Separator, Closet, LargeInteriorRoom}

    private GameObject prefab;
    private Vector3 startPos;
    private Vector3 startRot;
    private Transform newParent;

    private WallType typeOfWall;    // Only one type is implemented now

    // Start is called before the first frame update
    void Start()
    {
        typeOfWall = WallType.Separator;
    }

    /// <summary>
    /// Initialize the generator, works almost like a normal C# constructor
    /// </summary>
    /// <param name="pPrefab">prefab to generate</param>
    /// <param name="pStartPos">initial postion to begin from</param>
    /// <param name="pStartRot">initial rotation</param>
    /// <param name="pParent">parent object</param>
    public void Initialize(GameObject pPrefab, Vector3 pStartPos, Vector3 pStartRot, Transform pParent)
    {
        prefab = pPrefab;
        startPos = pStartPos;
        startRot = pStartRot;
        newParent = pParent;
    }

    /// <summary>
    /// Generate walls
    /// </summary>
    public void Generate()
    {
        switch (typeOfWall)
        {
            // Basic separator wall
            case WallType.Separator:
                //Check which axis is shorter and divide the room on it
                int _dirToSeparate = GeneratorManager.Instance.GetVerticalLenght <= GeneratorManager.Instance.GetHorizontalLenght ?
                    GeneratorManager.Instance.GetVerticalLenght : GeneratorManager.Instance.GetHorizontalLenght;

                //Gets renderer and adjusts the offset of the wall
                Renderer _wallRenderer = prefab.GetComponent<Renderer>();
                float _wallOffset = _wallRenderer.bounds.size.x;

                for (int i = 0; i < _dirToSeparate; i++)
                {

                    Vector3 _newPos = Vector3.zero;
                    //Based on the separation axis, add new wall at the end of the last wall, this is were we use the renderer offset
                    if (_dirToSeparate == GeneratorManager.Instance.GetHorizontalLenght)
                    {
                        _newPos = new Vector3((i * _wallOffset), 0.0f, startPos.z);
                    }
                    else if (_dirToSeparate == GeneratorManager.Instance.GetVerticalLenght)
                    {
                        _newPos = new Vector3(startPos.x, 0.0f, (i * _wallOffset));
                    }

                    GameObject newWall = Instantiate(prefab, _newPos, Quaternion.Euler(startRot), newParent);

                    GeneratorManager.Instance.SeparatingWalls.Add(newWall);

                    _newPos = newWall.transform.localPosition;

                    //Need to do this because of wall pivot
                    if (Mathf.RoundToInt(startRot.y) == 0)
                        _newPos.x += _wallOffset;
                    else if (Mathf.RoundToInt(startRot.y) == 270)
                        _newPos.z += _wallOffset;
                    
                    newWall.transform.localPosition = _newPos;
                }

                break;
                //Closet, should be a small room 
            case WallType.Closet:
                break;
                //Large interior room
            case WallType.LargeInteriorRoom:
                break;
            default:
                break;
        }
    }
}
