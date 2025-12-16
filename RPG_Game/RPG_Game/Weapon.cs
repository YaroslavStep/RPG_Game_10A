namespace RPG_Game
{
    public class Weapon : Item, IEquippable
    {
        private int _damage;

        public int Damage
        {
            get => _damage;
            protected set => _damage = value;
        }

        public Weapon(int damage, string name, string description) : base(name, description)
        {
            _damage = damage;
        }

        public void Equip(Player player)
        {
            Console.WriteLine($"{Name} екіпіровано! (+{Damage} до атаки)");
        }

        public void Unequip(Player player)
        {
            Console.WriteLine($"{Name} знято.");
        }

        public override void Use(Player player)
        {
            Console.WriteLine($"{player.Name} екіпірує {Name}!");
            Equip(player);
        }
    }
}
