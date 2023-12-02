using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private static Controller _instance;
    private List<Connector> _connectors;
    private List<Room> _rooms;
    [SerializeField] private Settings _settings;

    public static Controller Instance
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

    void Update() // majd Ienumerator kell ami frame-enként csinálja ezeket
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            _connectors = Connector.Instances;
            Debug.Log("connector references collecetd");
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            foreach (Connector connector in _connectors)
            {
                connector.ConnectorsSetUp();
            }
            Debug.Log("connectors set up");
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            _rooms = Room.Instances;
            Debug.Log("room references collected");
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            foreach (Room room in _rooms)
            {
                room.RoomsSetUp();
            }
            Debug.Log("rooms set up");
        }
        if (Input.GetKeyUp(KeyCode.U))
        {
            _settings.MazeStart.GetComponent<Room>().CutMaze(0, null);
            Debug.Log("maze cut");
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            foreach (Room room in _rooms)
            {
                try
                {
                    room.DeleteRooms();
                }
                catch (System.Exception)
                {
                    continue;
                }
            }
            Debug.Log("rooms killed");
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            foreach (Connector connector in _connectors)
            {
                connector.DeleteConnectors();
            }
            Debug.Log("connectors killed");
        }
    }
}

