
namespace Game.Scripts.Enemy.EnemyBodySpace
{
    /// <summary>
    /// This class is used to control the behavior of the enemy pelvis body part.
    /// Inherit EnemyBodyPart.cs.
    /// </summary>
    public class EnemyPelvis : EnemyBodyPart
    {
        public override void OnHit(int damage)
        {
            base.OnHit(damage);
            
            if (currentHp <= 0)
                ReleaseMainJoint();
        }
    }
}