using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Connector : MonoBehaviour
{
    static private List<Connector> _instances = new List<Connector>();
    [SerializeField] private GameObject _room1;
    [SerializeField] private GameObject _room2;
    private Room _room1Script;
    private Room _room2Script;
    [SerializeField] private bool _isVertical;
    private bool _putWall = false;
    private GameObject _door;
    private GameObject _wall;

    public static List<Connector> Instances
    {
        get { return _instances; }
    }
    public GameObject Room1
    {
        get { return _room1; }
    }
    public GameObject Room2
    {
        get { return _room2; }
    }
    public bool IsVertical
    {
        get { return _isVertical; }
    }
    public Room Room1Script
    {
        get { return _room1Script; }
    }
    public Room Room2Script
    {
        get { return _room2Script; }
    }

    private void Awake()
    {
        _instances.Add(this);
    }

    public void ConnectorsSetUp()
    {

        _room2Script = _room2.GetComponent<Room>();
        _room1Script = _room1.GetComponent<Room>();

        _room2Script.AddConnector(this.GetComponent<Connector>());
        _room1Script.AddConnector(this.GetComponent<Connector>());

        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (this.transform.GetChild(i).name == "Door")
            {
                _door = this.transform.GetChild(i).gameObject;
            }
            if (this.transform.GetChild(i).name == "Wall")
            {
                _wall = this.transform.GetChild(i).gameObject;
            }
        }
    }

    public void PutWall()
    {
        _putWall = true;
    }

    public void DeleteConnectors()
    {
        if (_room1 == null && _room2 == null)
        {
            Destroy(_wall);
            Destroy(_door);
            Destroy(this.gameObject);
        }
        else
        {
            if (_room1 == null || _room2 == null)
            {
                {
                    Destroy(_door);
                    _wall.transform.SetParent(null);
                    Destroy(this.gameObject);
                    return;
                }
            }
            if (_room1 != null && _room2 != null)
            {
                if (_putWall)
                {
                    Destroy(_door);
                    _wall.transform.SetParent(null);
                    Destroy(this.gameObject);
                    return;
                }
                else
                {
                    Destroy(_wall);
                    _door.transform.SetParent(null);
                    Destroy(this.gameObject);
                    return;
                }
            }
        }
    }
}
