namespace EpicDuelTheGame.Models
{
    public class ChooseHeroModel
    {
        public HeroType HeroType { get; set; }
        public string Description { get; set; }
        public string UltDescription { get; set; }
        public string FirstSpellDescription { get; set; }
        public string SecondSpellDescription { get; set; }
        public string Path { get; set; }

        public int Hp { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Intelligence { get; set; }
        public int Mana { get; set; }

        public ChooseHeroModel(HeroType heroType, string path, string description, string ultDescription, string firstSpellDescription, string secondSpellDescription)
        {
            HeroType = heroType;
            Path = path;
            Description = description;
            UltDescription = ultDescription;
            FirstSpellDescription = firstSpellDescription;
            SecondSpellDescription = secondSpellDescription;

            if (HeroType == HeroType.Warrior)
            {
                Hp = Globals.WARRIOR_START_HP;
                Strength = Globals.WARRIOR_START_STRENGTH;
                Dexterity = Globals.WARRIOR_START_DEXTERITY;
                Intelligence = Globals.WARRIOR_START_INTELLIGENCE;
                Mana = Globals.WARRIOR_START_MANA;
            }
            else if (HeroType == HeroType.Sorcerer)
            {
                Hp = Globals.SORCERER_START_HP;
                Strength = Globals.SORCERER_START_STRENGTH;
                Dexterity = Globals.SORCERER_START_DEXTERITY;
                Intelligence = Globals.SORCERER_START_INTELLIGENCE;
                Mana = Globals.SORCERER_START_MANA;
            }
            else if (HeroType == HeroType.Ranger)
            {
                Hp = Globals.RANGER_START_HP;
                Strength = Globals.RANGER_START_STRENGTH;
                Dexterity = Globals.RANGER_START_DEXTERITY;
                Intelligence = Globals.RANGER_START_INTELLIGENCE;
                Mana = Globals.RANGER_START_MANA;

            }
        }
    }
}