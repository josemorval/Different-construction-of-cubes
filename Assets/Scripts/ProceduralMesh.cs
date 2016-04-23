using UnityEngine;
using System.Collections;

public class ProceduralMesh : MonoBehaviour {

	public Material mat;
	public float distanceOriginZ;
	public float velRot;

	Vector3[] vsShared;
	int[] indsShared;

	Vector3[] vsUnique;
	int[] indsUnique;

	Mesh cubeSharedVertices;
	Mesh cubeUniqueVertices;

	void Start () {
	
		CreateCubeSharedVertices();
		CreateCubeUniqueVertices();

	}

	void CreateCubeSharedVertices(){
		vsShared = new Vector3[8];

		for(int i=0;i<2;i++){
			for(int j=0;j<2;j++){
				for(int k=0;k<2;k++){
					vsShared[k+2*j+4*i] = new Vector3(-1+2*i,-1+2*j,-1+2*k);
				}
			}
		}

		indsShared = new int[]{0,1,3,2,1,5,7,3,5,4,6,7,4,0,2,6,6,2,3,7,5,1,0,4};

		cubeSharedVertices = new Mesh();
		cubeSharedVertices.vertices = vsShared;
		cubeSharedVertices.SetIndices(indsShared,MeshTopology.Quads,0);

		cubeSharedVertices.RecalculateNormals();
	}

	void CreateCubeUniqueVertices(){
		vsUnique = new Vector3[24];
		indsUnique = new int[24];

		for(int i=0;i<vsUnique.Length;i++){
			vsUnique[i] = vsShared[indsShared[i]];
			indsUnique[i] = i;
		}


		cubeUniqueVertices = new Mesh();
		cubeUniqueVertices.vertices = vsUnique;
		cubeUniqueVertices.SetIndices(indsUnique,MeshTopology.Quads,0);

		cubeUniqueVertices.RecalculateNormals();
	}
	
	void Update () {

		MaterialPropertyBlock props = new MaterialPropertyBlock();
		Quaternion rot;
		Matrix4x4 m;

		//First cube
		rot = Quaternion.Euler(0f,velRot*Time.time,0f);
		m = Matrix4x4.TRS(Vector3.zero, rot, Vector3.one);

		props.SetMatrix("_rotMatrix",m);
		props.SetFloat("_zoffset",distanceOriginZ);
		Graphics.DrawMesh(cubeSharedVertices,transform.localToWorldMatrix,mat,0,null,0,props);

		//Second cube
		rot = Quaternion.Euler(0f,velRot*Time.time,0f);
		m = Matrix4x4.TRS(Vector3.zero, rot, Vector3.one);

		props.SetMatrix("_rotMatrix",m);
		props.SetFloat("_zoffset",-distanceOriginZ);
		Graphics.DrawMesh(cubeUniqueVertices,transform.localToWorldMatrix,mat,0,null,0,props);
	}
}
