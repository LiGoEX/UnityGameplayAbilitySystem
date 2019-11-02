using Unity.Entities;

namespace GameplayAbilitySystem.Abilities.Fire {
    public struct FireGameplayEffect : _IGameplayEffect, IComponentData {
        public Entity Target { get; set; }
        public Entity Source { get; set; }
        public DurationPolicyComponent DurationPolicy { get; set; }
        const float DamageMultiplier = -0.1f;
        const float DamageAdder = -5;
        public void ApplyGameplayEffect(int index, EntityCommandBuffer.Concurrent Ecb, AttributesComponent attributesComponent, float WorldTime) {
            var attributeModData = new _AttributeModificationComponent()
            {
                Add = DamageAdder,
                Multiply = DamageMultiplier,
                Divide = 0,
                Change = 0,
                Source = Source,
                Target = Target
            };

            var attributeModEntity = Ecb.CreateEntity(index);
            Ecb.AddComponent(index, attributeModEntity, new _HealthAttributeModifier());
            Ecb.AddComponent(index, attributeModEntity, new PermanentAttributeModification());
            Ecb.AddComponent(index, attributeModEntity, attributeModData);
        }

        public void ApplyGameplayEffect(EntityManager EntityManager, AttributesComponent attributesComponent, float WorldTime) {
            var attributeModData = new _AttributeModificationComponent()
            {
                Add = DamageAdder,
                Multiply = DamageMultiplier,
                Divide = 0,
                Change = 0,
                Source = Source,
                Target = Target
            };

            var attributeModEntity = EntityManager.CreateEntity(
                                                        typeof(_HealthAttributeModifier),
                                                        typeof(PermanentAttributeModification),
                                                        typeof(_AttributeModificationComponent)
            );
            EntityManager.SetComponentData(attributeModEntity, attributeModData);
        }
    }
}

