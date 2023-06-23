using System;
using Unity.VisualScripting;
using UnityEngine;
public class ControllerInstantiate : MonoBehaviour {
    public GameObject banzaiPrefab;
    public Transform spawnPoint;
    public Transform target;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("cualquier baina");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("detection Area");
        GameObject newProjectile = Instantiate(banzaiPrefab, spawnPoint.position, spawnPoint.rotation);
        FiniteStateMachine fsmBanzai = newProjectile.GetComponent<FiniteStateMachine>();
        fsmBanzai.target = target;
        this.GameObject().SetActive(false);
    }
}
