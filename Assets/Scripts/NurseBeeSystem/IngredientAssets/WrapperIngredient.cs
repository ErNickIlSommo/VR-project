using UnityEngine;

public class WrapperIngredient : MonoBehaviour
{
    [SerializeField] Ingredient _ingredient;
    
    public int GetId()
    {
        return _ingredient.id;
    }
}
