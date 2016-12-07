using UnityEngine;

public class HitEventHandler : MonoBehaviour {
    
    public delegate void OnHitEnterEventHandler(Collider col);
    public event OnHitEnterEventHandler OnHitEnter;
    public delegate void OnHitStayEventHandler(Collider col);
    public event OnHitStayEventHandler OnHitStay;
    public delegate void OnHitExitEventHandler(Collider col);
    public event OnHitExitEventHandler OnHitExit;

    void OnTriggerEnter(Collider other) {
        OnHitEnter(other);
    }
    void OnTriggerStay(Collider other) {
        OnHitStay(other);
    }
    void OnTriggerExit(Collider other) {
        OnHitExit(other);
    }
}
