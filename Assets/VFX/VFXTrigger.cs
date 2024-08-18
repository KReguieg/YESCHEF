using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFXTrigger : MonoBehaviour
{
    public GameObject VFX_system_gameobject;
    private VisualEffect VFX_system;

    // Start is called before the first frame update
    void Start()
    {

        VFX_system = VFX_system_gameobject.GetComponent<VisualEffect>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        VFX_system.Play();
    }

}
