
using Unity.VisualScripting;
using UnityEngine;

public class FogUnit : MonoBehaviour 
{   
    [SerializeField]
    [Tooltip("该单位提供的视野范围半径 单位为1逻辑grid")]
    private short _radius;
    public short Radius { get { return _radius; } }

    public short X { 
        get 
        { 
            return 0;
        } 
    }

    public short Y { 
        get 
        { 
            return 0;
        } 
    }

    public short PreX { get; private set; }
    public short PreY { get; private set; }

    public void Step()
    {
        PreX = X;
        PreY = Y;
    }
}
