using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRoom : MonoBehaviour, IGenerate
{
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject groundPrefab;
    [SerializeField] private GameObject roofPrefab;
    [SerializeField] private GameObject doorPrefab;
    [SerializeField] private GameObject windowPrefab;

    private void Start()
    {
        Generate();
    }

    public void Generate()
    {
        GameObject _wallParent = new GameObject("WallParent");
        GameObject _floorParent = new GameObject("FloorParent");

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

                    if ((h == GeneratorManager.Instance.GetVerticalLenght / 2 &&
                        GeneratorManager.Instance.GetVerticalLenght >= GeneratorManager.Instance.GetHorizontalLenght) ||
                        (w == GeneratorManager.Instance.GetHorizontalLenght / 2 &&
                        GeneratorManager.Instance.GetVerticalLenght < GeneratorManager.Instance.GetHorizontalLenght))
                    {
                        InteriorWallGenerator _wallGenerator = new InteriorWallGenerator();
                        _newRot.y += 90;
                        _wallGenerator.Initialize(wallPrefab, _newWall.transform.position, _newRot);
                        _wallGenerator.Generate();
                    }

                    else
                    {
                        int _rngWindow = Random.Range(0, 100);
                        if (_rngWindow > 90)
                        {
                            GenerateWindow(_newWall.transform.position, _newWall.transform.rotation);
                            Destroy(_newWall);
                        }
                        else
                        {
                            LargeObjectGenerator _randomItem = new LargeObjectGenerator();
                            GeneratableObject _itemToGenerate = GeneratorManager.Instance.GenerateRandomItem();


                            if (!_itemToGenerate.GenerateOnHeight)
                                _pivotOffset.z = -1;

                            _randomItem.Initialize(_itemToGenerate, _newWall.transform);
                            _randomItem.Generate();

                            _randomItem.Initialize(GeneratorManager.Instance.GetItem("Shelf"), _newWall.transform);
                            _randomItem.Generate();
                        }
                    }

                    if (w == 0 || h == 0)
                        continue;

                }


                GameObject _newGround = Instantiate(groundPrefab, new Vector3(w * _groundOffset, 0, h * _groundOffset),
                                                        Quaternion.identity, _floorParent.transform);

                Instantiate(roofPrefab, new Vector3(w * _groundOffset - 1.25f, _wallRenderer.bounds.size.y, h * _groundOffset - 1.25f),
                                Quaternion.identity, _floorParent.transform);

                int _rng = Random.Range(0, 100);
                if (_rng > 75)
                {
                    GameObject _carpet = Instantiate(GeneratorManager.Instance.GetItem("Carpet").Prefab, _newGround.transform);
                    _pivotOffset.z = -1;
                    _carpet.transform.localPosition = _pivotOffset;
                    _carpet.transform.eulerAngles = _rng > 88 ? Vector3.zero : new Vector3(0, 90, 0);
                }
            }
        }

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


        GenerateDoor(_doorPos, GeneratorManager.Instance.SeparatingWalls[_separatingWallDoor].transform.rotation);
    }

    private void GenerateDoor(Vector3 pPos, Quaternion pRotation)
    {
        Instantiate(doorPrefab, pPos, pRotation);
    }
    private void GenerateWindow(Vector3 pPos, Quaternion pRotation)
    {
        GameObject _window = Instantiate(windowPrefab, pPos, pRotation);
        float _offset = _window.GetComponent<Renderer>().bounds.extents.z;
        Vector3 _newPos = _window.transform.position;
        _newPos -= _window.transform.right * _offset;
        _window.transform.position = _newPos;
    }

}
