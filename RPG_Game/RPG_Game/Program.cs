using System.Security.Authentication;

namespace RPG_Game
{
    public interface IEquippable
    {
        string Name { get; }

        void Equip();
        void Unequip();
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}
