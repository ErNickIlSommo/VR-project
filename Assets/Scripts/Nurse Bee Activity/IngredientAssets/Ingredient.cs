using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "Ingredients/Ingredient")]
public class Ingredient : ScriptableObject
{
    public string name;
    public float cooldown;
    public GameObject prefab;
}
