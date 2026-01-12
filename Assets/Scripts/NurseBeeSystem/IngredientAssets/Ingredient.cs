using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "Ingredients/Ingredient")]
public class Ingredient : ScriptableObject
{
    public int id;
    public string ingredientName;
    public float cooldown;
    public GameObject prefab;
}
