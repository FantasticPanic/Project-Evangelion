using UnityEngine;
[CreateAssetMenu(fileName = "Create Shop Item", menuName = "Create Shop Item")]
public class SOShopSelection : ScriptableObject
{
    public Sprite icon;
    public string iconName;
    public string description;
    public int cost;
}