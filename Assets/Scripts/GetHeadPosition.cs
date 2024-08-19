using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHeadPosition : MonoBehaviour
{

	public static GetHeadPosition instance;

	private void Awake()
	{
		instance = this;
	}

	[System.NonSerialized] public Transform head_transform;
    
	// Start is called before the first frame update
    void Start()
    {
		head_transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
