using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILookAtFace : MonoBehaviour
{
	private GetHeadPosition textLookTargetTransform;

    // Start is called before the first frame update
    void Start()
    {
		textLookTargetTransform = GetHeadPosition.instance;
    }

    // Update is called once per frame
    void Update()
    {
		FaceTextMeshToCamera();
    }

	public void FaceTextMeshToCamera()
	{
		Vector3 origRot = transform.eulerAngles;
		transform.LookAt(textLookTargetTransform.transform);
		origRot.y = transform.eulerAngles.y + 180;
		transform.eulerAngles = origRot;
	}
}
