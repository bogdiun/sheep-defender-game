using UnityEngine;

public interface IFireable {
    void Fire(string layerName, Vector2 direction);
    void Stop();
}