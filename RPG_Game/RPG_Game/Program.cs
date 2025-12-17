using System.Security.Authentication;

namespace RPG_Game
{
    public class WoodenSword : Weapon
    {
        public WoodenSword() : base(20, "Дерев'яний меч", "+5 до атаки")
        {
        }
    }

    public class Goblin : Enemy
    {
        public Goblin() : base("Goblin", 100, 10, 50)
        {
        }
        public override void Attack(Character target)
        {
            target.TakeDamage(Strength);
        }
    }

    public class Troll : Enemy
    {
        public Troll() : base("Troll", 150, 20, 100)
        {
        }

        public override void Attack(Character target) 
        {
            target.TakeDamage(Strength);
        }
    }

    public class DarkWizard : Enemy, ISpellCaster
    {
        private int _mana;

        public int Mana
        {
            get => _mana;
            private set => _mana = value;
        }

        public DarkWizard() : base("Dark Wizard", 50, 50, 100)
        {
        }

        public override void Attack(Character target)
        {
            throw new NotImplementedException();
        }

        public void CastSpell(Character target)
        {
            throw new NotImplementedException();
        }

        public void RestoreMana(int amount)
        {
            throw new NotImplementedException();
        }
    }

        

        

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}
