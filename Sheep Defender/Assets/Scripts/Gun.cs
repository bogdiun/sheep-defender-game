using UnityEngine;

public interface Gun {
    void Fire(string layerName, Vector2 direction);
    void Stop();
}