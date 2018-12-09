namespace birds
{
    /// <summary>
    /// 白色小鸟的动画
    /// </summary>
    public class WhiteBirdAnim : BirdAnim
    {
        private bool isShowSkill; //是否已经展示了技能

        public override void ShowSkill()
        {
            base.ShowSkill();
            isShowSkill = true;
        }

        public override void hurt()
        {
            if (!isShowSkill)
            {
                base.hurt(); //如果没有展示技能那么可以受伤
            }
        }
    }
}