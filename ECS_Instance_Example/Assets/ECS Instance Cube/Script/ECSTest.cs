using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

public class ECSTest : MonoBehaviour
{
    public int howManyMeshInstantiator;
    public float distanceMeshWithMesh;
    public Mesh m_cubeMesh;
    public Material[] m_cubeMaterials;

    private EntityManager m_entityManager;
    private EntityArchetype m_CubeArchetype;

    private void Start()
    {
        m_entityManager = World.Active.EntityManager;
        m_CubeArchetype = m_entityManager.CreateArchetype(
            typeof(RenderMesh),
            typeof(Translation),
            typeof(LocalToWorld)
            );

        CreateCube();
    }

    private void CreateCube()
    {

        NativeArray<Entity> nativeArray = new NativeArray<Entity>(howManyMeshInstantiator, Allocator.Temp);

        m_entityManager.CreateEntity(m_CubeArchetype, nativeArray);

        foreach (var entity in nativeArray)
        {
            int randomMaterialIndex = UnityEngine.Random.Range(0, m_cubeMaterials.Length);

            m_entityManager.SetComponentData(entity, new Translation
            {
                Value = new float3(UnityEngine.Random.Range(-distanceMeshWithMesh, distanceMeshWithMesh), 
                0, UnityEngine.Random.Range(-distanceMeshWithMesh, distanceMeshWithMesh))
            }); ;

            m_entityManager.SetSharedComponentData(entity, new RenderMesh
            {
                mesh = m_cubeMesh,
                material = m_cubeMaterials[randomMaterialIndex]
            });
        }
    }
} 


