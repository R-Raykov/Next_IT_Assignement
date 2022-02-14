using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRoom : MonoBehaviour, IGeneratable
{
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject groundPrefab;
    [SerializeField] private GameObject roofPrefab;
    [SerializeField] private GameObject doorPrefab;
    [SerializeField] private GameObject windowPrefab;

    /// <summary>
    /// Generates the base random room
    /// </summary>
    public void Generate()
    {
        //Creates parent objects to keep things cleaner
        GameObject _wallParent = new GameObject("WallParent");
        GameObject _floorParent = new GameObject("FloorParent");

        _wallParent.transform.parent = transform;
        _floorParent.transform.parent = transform;

        //caches the renderers so we don't have to call GetComponent in the double for loop
        Renderer _wallRenderer = wallPrefab.GetComponent<Renderer>();
        Renderer _groundRenderer = groundPrefab.GetComponent<Renderer>();

        float _wallOffset = _wallRenderer.bounds.size.x;
        float _groundOffset = _groundRenderer.bounds.size.x;

        for (int w = 0; w <= GeneratorManager.Instance.GetHorizontalLenght; w++)
        {
            for (int h = 0; h <= GeneratorManager.Instance.GetVerticalLenght; h++)
            {
                Vector3 _pivotOffset = Vector3.zero;
                _pivotOffset.x = -1;

                //Adds the walls around the edges
                #region Walls
                if (w == 0 || h == 0 ||
                    w == GeneratorManager.Instance.GetHorizontalLenght || h == GeneratorManager.Instance.GetVerticalLenght)
                {
                    Vector3 _newRot = Vector3.zero;

                    if (w == GeneratorManager.Instance.GetHorizontalLenght)
                    {
                        _newRot.y = -90;
                    }
                    else if (w == 0)
                    {
                        _newRot.y = 90;
                    }

                    if (h == GeneratorManager.Instance.GetVerticalLenght && w != GeneratorManager.Instance.GetHorizontalLenght)
                    {
                        _newRot.y = 180;
                    }
                    else if (h == 0 && w != 0)
                    {
                        _newRot.y = 0;
                    }

                    GameObject _newWall = Instantiate(wallPrefab, new Vector3(w * _wallOffset, 0, h * _wallOffset),
                                                        Quaternion.Euler(_newRot), _wallParent.transform);

                    // Adds the separator wall
                    if ((h == GeneratorManager.Instance.GetVerticalLenght / 2 &&
                        GeneratorManager.Instance.GetVerticalLenght >= GeneratorManager.Instance.GetHorizontalLenght) ||
                        (w == GeneratorManager.Instance.GetHorizontalLenght / 2 &&
                        GeneratorManager.Instance.GetVerticalLenght < GeneratorManager.Instance.GetHorizontalLenght))
                    {
                        InteriorWallGenerator _wallGenerator = new InteriorWallGenerator();
                        _newRot.y += 90;
                        _wallGenerator.Initialize(wallPrefab, _newWall.transform.position, _newRot, _wallParent.transform);
                        _wallGenerator.Generate();
                    }
                    // If we aren't adding a separation wall on this block, try to add a window
                    else
                    {
                        if (GameManager.Instance.GetRandomNumber() > 90)
                        {
                            GenerateWindow(_newWall.transform.position, _newWall.transform.rotation, _wallParent.transform);
                            Destroy(_newWall);
                        }
                        else
                        {
                            //if we arent adding a wall or window here, place an object
                            LargeObjectGenerator _randomItem = new LargeObjectGenerator();
                            GeneratableObject _itemToGenerate = GeneratorManager.Instance.GenerateRandomItem();


                            if (!_itemToGenerate.GenerateOnHeight)
                                _pivotOffset.z = -1;

                            _randomItem.Initialize(_itemToGenerate, _newWall.transform);
                            _randomItem.Generate();

                            // Since I want more shelfs around (they have lights and make the room feel less empty) try adding a shelf
                            _randomItem.Initialize(GeneratorManager.Instance.GetItem("Shelf"), _newWall.transform);
                            _randomItem.Generate();
                        }
                    }

                    if (w == 0 || h == 0)
                        continue;
                    #endregion

                }

                #region Ground

                //Generate the ground and roof
                GameObject _newGround = Instantiate(groundPrefab, new Vector3(w * _groundOffset, 0, h * _groundOffset),
                                                        Quaternion.identity, _floorParent.transform);

                Instantiate(roofPrefab, new Vector3(w * _groundOffset - 1.25f, _wallRenderer.bounds.size.y, h * _groundOffset - 1.25f),
                                Quaternion.identity, _floorParent.transform);

                int _rng = GameManager.Instance.GetRandomNumber();
                if (_rng > 75)
                {
                    GameObject _carpet = Instantiate(GeneratorManager.Instance.GetItem("Carpet").Prefab, _newGround.transform);
                    _pivotOffset.z = -1;
                    _carpet.transform.localPosition = _pivotOffset;
                    _carpet.transform.eulerAngles = _rng > 88 ? Vector3.zero : new Vector3(0, 90, 0);
                }
            }
        }
        #endregion

        #region SeparationWall
        // the wall is double sided because of the missing texture on the back
        // this could have also been fixed by adding another wall in the prefab or fixing the model
        // but I already did this before it came to mind

        int _dirToSeparate = GeneratorManager.Instance.GetVerticalLenght <= GeneratorManager.Instance.GetHorizontalLenght ?
                                GeneratorManager.Instance.GetVerticalLenght : GeneratorManager.Instance.GetHorizontalLenght;

        int _separatingWallDoor = Random.Range(GeneratorManager.Instance.SeparatingWalls.Count/2, 
            GeneratorManager.Instance.SeparatingWalls.Count/2 + _dirToSeparate);

        GeneratorManager.Instance.SeparatingWalls[_separatingWallDoor].SetActive(false);
        GeneratorManager.Instance.SeparatingWalls[_separatingWallDoor - _dirToSeparate].SetActive(false);

        Vector3 _doorPos = GeneratorManager.Instance.SeparatingWalls[_separatingWallDoor].transform.position;

        if (_dirToSeparate == GeneratorManager.Instance.GetHorizontalLenght)
        {
            _doorPos.x -= _wallOffset / 2;
            _doorPos.z += _wallRenderer.bounds.extents.z;
        }
        else if (_dirToSeparate == GeneratorManager.Instance.GetVerticalLenght)
        {
            _doorPos.z -= _wallOffset / 2;
            _doorPos.x -= _wallRenderer.bounds.extents.z;
        }

        GenerateDoor(_doorPos, GeneratorManager.Instance.SeparatingWalls[_separatingWallDoor].transform.rotation, _wallParent.transform);
        #endregion
    }

    /// <summary>
    /// Generates a door at the specified location
    /// </summary>
    /// <param name="pPos">position to generte at</param>
    /// <param name="pRotation">rotation to generate at</param>
    /// <param name="pParent">parent</param>
    private void GenerateDoor(Vector3 pPos, Quaternion pRotation, Transform pParent)
    {
        Instantiate(doorPrefab, pPos, pRotation, pParent);
    }

    /// <summary>
    /// Generates a window at the specified location
    /// </summary>
    /// <param name="pPos">position to generte at</param>
    /// <param name="pRotation">rotation to generate at</param>
    /// <param name="pParent">parent</param>
    private void GenerateWindow(Vector3 pPos, Quaternion pRotation, Transform pParent)
    {
        GameObject _window = Instantiate(windowPrefab, pPos, pRotation, pParent);
        Vector3 _offset = _window.GetComponent<Renderer>().bounds.extents;

        Vector3 _newPos = _window.transform.localPosition;

        // Similarly to the interior wall, I have to add an offset
        // here I did it with the right transform because it has to be done regardless of orientation
        // the orientation only determines which renderer extent to use
        // also, I wanted to show it can be done this way as well

        if (Mathf.RoundToInt(_window.transform.eulerAngles.y) == 0 || Mathf.RoundToInt(_window.transform.eulerAngles.y) == 180)
            _newPos -= _window.transform.right * _offset.x;
        else
            _newPos -= _window.transform.right * _offset.z;

        _window.transform.localPosition = _newPos;
    }

}
