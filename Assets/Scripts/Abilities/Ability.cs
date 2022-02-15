using GameDevTV.Inventories;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities
{
    [CreateAssetMenu(fileName = "My Abiliity", menuName = "Abilities/Ability", order = 0)]
    public class Ability : ActionItem
    {
        [SerializeField] TargetingStrategy targetingStrategy;
        [SerializeField] FilterStrategy[] filterStrategies;
        [SerializeField] EffectStrategy[] effectStrategies;

        public override void Use(GameObject user)
        {
            targetingStrategy.StartTargeting(user,
                (IEnumerable<GameObject> targets) => {
                    TargetAcquired(user, targets);
                });
        }

        private void TargetAcquired(GameObject user, IEnumerable<GameObject> targets)
        {
            foreach (var filterStrategy in filterStrategies)
            {
                targets = filterStrategy.Filter(targets);
            }

            foreach (var effect in effectStrategies)
            {
                effect.StartEffect(user, targets, EffectFinished);
            }
        }

        private void EffectFinished()
        {
            
        }
    }
}