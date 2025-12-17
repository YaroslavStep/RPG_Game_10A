using System.Security.Authentication;

namespace RPG_Game
{
    public class WoodenSword : Weapon
    {
        public WoodenSword() : base(20, "Дерев'яний меч", "+5 до атаки")
        {
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
