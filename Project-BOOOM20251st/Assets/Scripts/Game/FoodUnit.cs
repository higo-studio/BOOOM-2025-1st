using UnityEngine;

public class FoodUnit : MonoBehaviour
{
    public int ResourceNum = 1;
    public bool BeingTargeted = false;
    
    private void Start() 
    {
        FoodSystem.Instance.JoinFood(this);
    }

    public void BeenCarryed()
    {

    }

    public void BeenPut()
    {
    }

    public void BeenDrop()
    {
    }

}

