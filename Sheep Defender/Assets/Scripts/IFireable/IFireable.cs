using UnityEngine;

public interface IFireable {    
    void Fire(string targetTag);
    void Stop();
}