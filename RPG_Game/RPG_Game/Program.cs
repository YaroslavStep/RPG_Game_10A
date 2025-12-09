using System.Security.Authentication;

namespace RPG_Game
{
    public interface IEquippable
    {
        string Name { get; }

        void Equip();
        void Unequip();
    }

    public abstract class Item
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }

        protected Item(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public abstract void Use(Player player);
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}
