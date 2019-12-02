using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public class ECS_SpawnerTank : MonoBehaviour
{
    public GameObject m_prefab;

    private void Start()
    {
        var prefabECS = GameObjectConversionUtility.ConvertGameObjectHierarchy(m_prefab, World.Active);
        var entityManager = World.Active.EntityManager;

        var instanceECS = entityManager.Instantiate(prefabECS);

        var position = new Vector3(0, 0, 1);
        entityManager.SetComponentData(instanceECS, new Translation { Value = position });

        entityManager.AddComponentData(instanceECS, new MovementForward { m_speed = 2f });
    }
}

public struct MovementForward : IComponentData
{
    public float m_speed;
}

public class MovementForwardSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.WithAll<MovementForward>().ForEach(
            (Entity _entity, ref Translation _translation, ref MovementForward _forward) =>
            {
                _translation.Value += (float3)(Vector3.forward * _forward.m_speed * Time.deltaTime);
            });
    }
}

public class RotationAroundYSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.WithAll<RotateAroundY>().ForEach(
            (Entity _entity, ref Rotation _rotation, ref RotateAroundY _rotateAroundY) =>
            {
                _rotation.Value = math.mul(math.normalize(_rotation.Value),
                    quaternion.AxisAngle(math.up(),_rotateAroundY.m_rotationSpeedInRadianPerSecond * Time.deltaTime));
            });
    }
}
