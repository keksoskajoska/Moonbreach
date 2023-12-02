using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using System.Security.Cryptography;

public class Room : MonoBehaviour
{
    static private List<Room> _instances = new List<Room>();
    static private System.Random rnd = new System.Random();
    static public int Rooms;
    private int _maxRooms;
    [SerializeField] private List<Connector> _connectors;
    [SerializeField] private bool _beenHere;
    [SerializeField] public bool ToDestroy;
    [SerializeField] private int _deph;
    
    public bool BeenHere
    {
        get { return _beenHere; }
    }
    public static List<Room> Instances
    {
        get { return _instances; }
    }

    private void Awake()
    {
        _instances.Add(this);
    }

    public void RoomsSetUp()
    {
        _deph = Settings.Instance.Deph;
        _maxRooms = Settings.Instance.MaxRooms;
        _beenHere = false;
        ToDestroy = false;
        Rooms = 0;
        ShuffleMe(_connectors);
    }

    private void ShuffleMe( List<Connector> list)
    {
        //fisher-yates
        int n = list.Count;

        System.Random rand = new System.Random(System.Guid.NewGuid().GetHashCode());

        for (int i = list.Count - 1; i > 1; i--)
        {
            int randInt = rand.Next(i + 1);

            Connector value = list[randInt];
            list[randInt] = list[i];
            list[i] = value;
        }
    }

    public void AddConnector(Connector connetor)
    {
        _connectors.Add(connetor);
    }

    public int GetConnectorCount()
    {
        return _connectors.Count;
    }

    public void DeleteRooms()
    {
        if (!BeenHere|| ToDestroy)
        {
            Destroy(this.gameObject);
        }
    }

    public void CutMaze(int deph, Room previousRoom)
    {
        int chance;
        if (_beenHere || ToDestroy)
        {
            return;
        }
        if (Rooms >= _maxRooms)
        {
            Destroy(this.gameObject);
            return;
        }

        _beenHere = true;

        if (deph > _deph && rnd.Next(1, 101) > Settings.Instance.BonusDephChance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Rooms++;
            foreach (Connector connector in _connectors)
            {
                if (connector.Room1Script == previousRoom || connector.Room2Script == previousRoom)
                {
                    continue;
                }
                if (connector.IsVertical)
                {
                    chance = Settings.Instance.VerticalChance;
                }
                else
                {
                    chance = Settings.Instance.HorizontalChance;
                }

                if (connector.Room1Script == this)
                {
                    if ((rnd.Next(1, 101) < chance * (_connectors.Count/2) ) && Rooms > Settings.Instance.MinRooms)
                    {
                        if (connector.Room2Script.BeenHere)
                        {
                            connector.PutWall();
                        }
                        else
                        {
                            connector.Room2Script.ToDestroy = true;
                            return;
                        }
                    }
                    else
                    {
                        connector.Room2Script.CutMaze(deph + 1,this);
                    }
                }
                else
                {
                    if ((rnd.Next(1, 101) < (chance * (_connectors.Count / 2)) ) && Rooms > Settings.Instance.MinRooms)
                    {
                        if (connector.Room1Script.BeenHere)
                        {
                            connector.PutWall();
                        }
                        else
                        {
                            connector.Room1Script.ToDestroy = true;
                            return;
                        }
                    }
                    else
                    {
                        connector.Room1Script.CutMaze(deph + 1,this);
                    }
                }
            }
        }
    }
}
