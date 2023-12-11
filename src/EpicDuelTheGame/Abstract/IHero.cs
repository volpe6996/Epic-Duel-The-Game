namespace EpicDuelTheGame.Abstract
{
    public interface IHero
    {
        protected virtual void Init() { }

        public int Hp { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Intelligence { get; set; }
        public int Mana { get; set; }
        public int? Counterattack { get; set; }
        public int? Healing { get; set; }
        public int? Dodge { get; set; }
    }
}
