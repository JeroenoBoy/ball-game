using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode, RequireComponent(typeof(MeshFilter), typeof(PolygonCollider2D), typeof(MeshRenderer))]
public class leveleditor : MonoBehaviour
{
    private PolygonCollider2D collider2d;
    
    private Mesh mesh;
    private MeshFilter filter;
    [SerializeField]private Vector3[] vertices;
    private Bounds bounds;
    [SerializeField] bool generateMesh;
    [SerializeField] bool gridsnapping;
    [SerializeField] Vector3 selectedVertice;
    

    // Start is called before the first frame update
    void Start()
    {
        generateMesh = true;
        collider2d = GetComponent<PolygonCollider2D>();
        filter = GetComponent<MeshFilter>();    

        CreateMesh();        
      
        
    }

#if UNITY_EDITOR
    void Update()
    {        
        if (Application.isPlaying == false)
        {

            if (gridsnapping == true)
            {
                Vector2[] path = collider2d.GetPath(0);
                for(int i = 0; i < path.Length  ; i++)
                {
                    float x = Mathf.Round(path[i].x);
                    float y = Mathf.Round(path[i].y);              

                    Vector2 vector2 = new Vector2(x, y);
                    path[i] = vector2;
                }
                collider2d.SetPath(0, path);
            }
            
            CreateMesh();
           
        }      
    }
#endif
    private void CreateMesh()
    {
        if (generateMesh == true)
        {
            
            mesh = collider2d.CreateMesh(false, false);
            bounds = mesh.bounds;
            vertices = mesh.vertices;

            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] = transform.InverseTransformPoint(mesh.vertices[i]);
            }

            bounds.max = transform.InverseTransformPoint(bounds.max);
            bounds.min = transform.InverseTransformPoint(bounds.min);

            mesh.vertices = vertices;
            mesh.bounds = bounds;
            filter.mesh = mesh;
        }
        
    }
}
