using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "Scriptable Objects/Ingredient")]
public class Ingredient : ScriptableObject
{
   [SerializeField] private string _name;
   [SerializeField] private string _cooldown;
   [SerializeField] private GameObject _prefab;
}
