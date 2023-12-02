using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Settings : MonoBehaviour
{
    private static Settings _instance;
    [SerializeField] private GameObject _mazeStart;
    [SerializeField] private int _verticalChance;
    [SerializeField] private int _horizontalChance;
    [SerializeField] private int _deph;
    [SerializeField] private int _maxRooms;
    [SerializeField] private int _minRooms;
    [SerializeField] private int _bonusDephChance;

    public static Settings Instance
    {
        get
        {
            return _instance;
        }

        private set
        {
            _instance = value;
        }
    }
    public int BonusDephChance
    {
        get { return _bonusDephChance; }
    }
    public GameObject MazeStart
    {
        get { return _mazeStart; }
    }
    public int VerticalChance
    {
        get { return _verticalChance; }
    }
    public  int HorizontalChance
    {
        get { return _horizontalChance; }
    }
    public int Deph
    {
        get { return _deph; }
    }
    public int MaxRooms
    {
        get { return _maxRooms; }
    }
    public int MinRooms
    {
        get { return _minRooms; }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Multiple instances of Singleton exist, destroying self.");
            Destroy(this.gameObject);
        }
    }
}
