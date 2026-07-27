using UnityEngine;

[System.Serializable]
public class AiComponnent
{
    private Enemy _parent;

    public AiComponnent(Enemy newparent)
    {
        _parent = newparent;

    }

    public Vector2 DirectionTowards(Vector3 objectivePos)
    {
        Vector3 newDir = Vector3.Normalize(objectivePos - _parent.transform.position );
        
        Vector2 onlydir = new Vector2(newDir.x, newDir.z);
        return onlydir;
    }
}
