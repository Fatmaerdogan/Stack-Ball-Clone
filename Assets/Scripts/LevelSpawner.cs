using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _allPlatforms;
    [SerializeField] private GameObject[] _selectedPlatforms = new GameObject[4];
    [SerializeField] private GameObject _winPrefab;
    
    private GameObject _normalPlatforms, _winPlatform;
    public int _level, _platformAddition = 7;
    [SerializeField] private float _rotationSpeed = 10f;
    private float i = 0;
    public Color plateColor;
    public Material baseMaterial;
    public Material planeMaterial;

    void Start()
    {
        LevelManagement();
    }
    private void LevelManagement()
    {
        _level = GameManager.Instance.Level;
        if (_level > 9)
            _platformAddition = 0;

        PlatformSelection();
        for (i = 0; i > -_level - _platformAddition; i -= 0.5f)
        {
            if (_level <= 40)_normalPlatforms = Instantiate(_selectedPlatforms[Random.Range(0, 2)]);
            if (_level > 40 && _level <= 80)_normalPlatforms = Instantiate(_selectedPlatforms[Random.Range(1, 3)]);
            if (_level > 80 && _level <= 140)_normalPlatforms = Instantiate(_selectedPlatforms[Random.Range(2, 4)]);
            if (_level > 140)_normalPlatforms = Instantiate(_selectedPlatforms[Random.Range(3, 4)]);

            _normalPlatforms.transform.position = new Vector3(0, i, 0);
            _normalPlatforms.transform.eulerAngles = new Vector3(0, i * _rotationSpeed, 0);

            if (Mathf.Abs(i) >= _level * .3f && Mathf.Abs(i) <= _level * .6f) 
            {
                _normalPlatforms.transform.eulerAngles += Vector3.up * 180;
            }

            _normalPlatforms.transform.parent = FindObjectOfType<Platforms>().transform;
        }

        _winPlatform = Instantiate(_winPrefab);
        _winPlatform.transform.position = new Vector3(0, i, 0);
        plateColor = planeMaterial.color = Random.ColorHSV(0, 1, .5f, 1, 1, 1);
        Events.Color?.Invoke(plateColor);
        baseMaterial.color = plateColor + Color.gray;
    }

    void PlatformSelection()
    {
        int randomModel = Random.Range(0, 5);
        switch (randomModel)
        {
            case 0:
                for (int i = 0; i < 4; i++)
                    _selectedPlatforms[i] = _allPlatforms[i];
                break;
            case 1:
                for (int i = 0; i < 4; i++)
                    _selectedPlatforms[i] = _allPlatforms[i + 4];
                break;
            case 2:
                for (int i = 0; i < 4; i++)
                    _selectedPlatforms[i] = _allPlatforms[i + 8];
                break;
            case 3:
                for (int i = 0; i < 4; i++)
                    _selectedPlatforms[i] = _allPlatforms[i + 12];
                break;
            case 4:
                for (int i = 0; i < 4; i++)
                    _selectedPlatforms[i] = _allPlatforms[i + 16];
                break;
        }
    }
}
