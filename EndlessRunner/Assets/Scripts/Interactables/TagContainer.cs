using UnityEngine;

public class TagContainer : MonoBehaviour
{
    [SerializeField] private TagData tagData;

    public TagData TagData { get => tagData; }
}
