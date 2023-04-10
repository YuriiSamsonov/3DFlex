
namespace Game.Scripts.Enemy.EnemyBodySpace
{
    public class EnemyPelvis : EnemyBodyPart
    {
        public override void OnHit(int damage)
        {
            base.OnHit(damage);
            
            if (currentHp <= 0)
            {
                ReleaseMainJoint();
            }
        }
    }
}