using UnityEngine;

namespace br.com.arthurjf.shapewar.Character
{
    // INHERITANCE
    public class EnemyCharacter : CharacterBase
    {
        // POLYMORPHISM
        protected override void Move(float amount)
        {
            throw new System.NotImplementedException();
        }

        // POLYMORPHISM
        protected override void Rotate(float amount)
        {
            throw new System.NotImplementedException();
        }
    }
}
