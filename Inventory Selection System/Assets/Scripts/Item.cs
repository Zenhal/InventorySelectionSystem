using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu]
public class Item : ScriptableObject
{
    [SerializeField] string id;
    public string ID { get { return id; } }
    public string ItemName;
    public Sprite Icon;

#if UNITY_EDITOR
    private void OnValidate()
    {
        string path = AssetDatabase.GetAssetPath(this);
        id = AssetDatabase.AssetPathToGUID(path);
    }
#endif

    public virtual void Destroy()
    {

    }
    public virtual Item GetCopy()
    {
        return this;
    }

}
