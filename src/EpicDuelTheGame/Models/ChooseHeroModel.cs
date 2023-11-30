using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpicDuelTheGame.Models
{
    public class ChooseHeroModel
    {
        public HeroTypes HeroType { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }

        public int Hp { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Intelligence { get; set; }
        public int Mana { get; set; }

        public int? Counterattack { get; set; } = null;
        public int? Healing { get; set; } = null;
        public int? Dodge { get; set; } = null;

        public ChooseHeroModel(HeroTypes heroType, string description, string path)
        {
            HeroType = heroType;
            Description = description;
            Path = path;

            if (heroType == HeroTypes.Warrior)
            {
                this.Hp = 50;
                this.Strength = 35;
                this.Dexterity = 25;
                this.Intelligence = 20;
                this.Mana = 50;
                this.Counterattack = 40;
            }
            else if (heroType == HeroTypes.Sorcerer)
            {
                this.Hp = 30;
                this.Strength = 40;
                this.Dexterity = 30;
                this.Intelligence = 30;
                this.Mana = 50;
                this.Healing = 15;
            }
            else
            {
                this.Hp = 35;
                this.Strength = 30;
                this.Dexterity = 40;
                this.Intelligence = 35;
                this.Mana = 50;
                this.Dodge = 20;
            }
        }
    }

    public enum HeroTypes
    {
        Warrior,
        Sorcerer,
        Ranger
    }
}