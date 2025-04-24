using UnityEngine;
using UnityCommunity.UnitySingleton;

public class Floor : MonoSingleton<Floor>
{
    
    private Queen queen;

    private void Start() 
    {
        queen = Queen.Instance;  
    }

    
}
