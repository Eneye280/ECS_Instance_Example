using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public class ECS_RotationSystem : MonoBehaviour, IConvertGameObjectToEntity
{
    public float m_rotationSpeedInDegreePerSecond;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        var component = new RotateAroundY
        {
            m_rotationSpeedInRadianPerSecond = math.radians(m_rotationSpeedInDegreePerSecond)
        };

        dstManager.AddComponentData(entity,component);
    }
}

public struct RotateAroundY : IComponentData
{
    public float m_rotationSpeedInRadianPerSecond;
}
