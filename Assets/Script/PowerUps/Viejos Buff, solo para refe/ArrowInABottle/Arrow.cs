using UnityEngine;

public class Arrow : Proyectile, IProjectile
{
    public void SetTarget(Transform target)
    {
        if (GameManager.Instance == null) { Debug.LogError("GameManager is null!"); return; }
        if (GameManager.Instance.Player == null) { Debug.LogError("Player is null!"); return; }

        Vector3 aimedPos = GameManager.Instance.Player.GetLookDretirection();
        transform.LookAt(aimedPos + transform.position);
        this._fromPlayer = true;
        this._damage = 10;
        this._speed = 10;
    }
}
