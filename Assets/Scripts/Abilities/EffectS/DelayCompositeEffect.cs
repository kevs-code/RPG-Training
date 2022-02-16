using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities.Effects
{
    [CreateAssetMenu(fileName = "Delay Composite Effect", menuName = "Abilities/Effects/Delay Composite", order = 0)]
    public class DelayCompositeEffect : EffectStrategy
    {
        [SerializeField] float delay = 0;
        [SerializeField] EffectStrategy[] delayedEffects;

        public override void StartEffect(AbilityData data, Action finished)
        {
            data.StartCoroutine(delayedEffect(data, finished));
        }

        private IEnumerator delayedEffect(AbilityData data, Action finished)
        {
            yield return new WaitForSeconds(delay);
            foreach (var effect in delayedEffects)
            {
                effect.StartEffect(data, finished);
            }
        }
    }
}