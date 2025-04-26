using UnityEngine;

[CreateAssetMenu()]
public class ElementSO : ScriptableObject
{
    public string elementName;
    public ElementSO strongAgainst;
    public ElementSO weakAgainst;
}
