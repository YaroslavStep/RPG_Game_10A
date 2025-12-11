using System.Security.Authentication;

namespace RPG_Game
{

    public class Player : Character, ISpellCaster
    {
        private int _mana;
        private int _maxMana;
        private int _experience;
        private int _level;
        private IEquippable _equippableWeapon;
        private List<Item> _inventory;

        public class Weapon
        {
            private int _damage;

            public int Damage
            {
                get => _damage;
                protected set => _damage = value;
            }
            public Weapon(int damage)
            {
                _damage = damage;
            }
        }

        public int Mana
        {
            get => _mana;
            private set => _mana = Math.Max(0, Math.Min(_mana, value));
        }

        public int MaxMana
        {
            get => _maxMana;
            private set => _maxMana = value;
        }
        public int Experience
        {
            get => _experience;
            private set => _experience = value;
        }
        public int Level
        {
            get => _level;
            private set => _level = value;
        }

        public IReadOnlyList<Item> Inventory => _inventory.AsReadOnly();
        public Player(string name) : base(name, 100, 10)
        {
        }

        public override void Attack(Character target)
        {
            var rand = new Random();
            var baseDamage = Strength;

            if (_equippableWeapon != null && _equippableWeapon is Weapon weapon)
            {
                baseDamage += weapon.Damage;
            }

            var isCritical = rand.Next(100) < 20;
            var damage = isCritical ? baseDamage * 2 : baseDamage;

            if (isCritical)
            {
                Console.WriteLine($"КРИТИЧНИЙ УДАР! {Name} завдає {damage} пошкоджень {target.Name}!");
            }
            else
            {
                Console.WriteLine($"{Name} атакує {target.Name} і завдає {damage} пошкоджень!");
            }

            target.TakeDamage(damage);
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
