using System.Security.Authentication;

namespace RPG_Game
{
    public class WoodenSword : Weapon
    {
        public WoodenSword() : base(20, "Дерев'яний меч", "+5 до атаки")
        {
        }
    }

    public class Game
    {
        private Player player;
        private Random rand;
        private int battlesWon;

        public Game()
        {
            rand = new Random();
            battlesWon = 0;
        }

        public void Start()
        {
            Console.Clear();
            Console.WriteLine("╔═══════════════════════════════════════╗");
            Console.WriteLine("║   ТЕКСТОВА РОЛЬОВА ГРА (Text RPG)    ║");
            Console.WriteLine("╚═══════════════════════════════════════╝\n");

            Console.Write("Введіть ім'я вашого героя: ");
            string name = Console.ReadLine();

            player = new Player(name);
            player.AddItem(new HealthPotion());
            player.AddItem(new ManaPotion());
            player.EquipWeapon(new WoodenSword());

            Console.WriteLine($"\n🎮 Вітаємо, {name}! Ваша пригода починається...\n");
            Console.WriteLine("Натисніть будь-яку клавішу для продовження...");
            Console.ReadKey();

            MainGameLoop();
        }

        private void MainGameLoop()
        {
            while (player.IsAlive)
            {
                Console.Clear();
                player.ShowStats();

                Console.WriteLine("╔═══════════════════════════════════════╗");
                Console.WriteLine("║           ГОЛОВНЕ МЕНЮ                ║");
                Console.WriteLine("╠═══════════════════════════════════════╣");
                Console.WriteLine("║ 1. Почати бій                         ║");
                Console.WriteLine("║ 2. Переглянути інвентар               ║");
                Console.WriteLine("║ 3. Відпочити (відновити HP та ману)  ║");
                Console.WriteLine("║ 4. Вийти з гри                        ║");
                Console.WriteLine("╚═══════════════════════════════════════╝");
                Console.Write("\nВаш вибір: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        StartBattle();
                        break;
                    case "2":
                        ManageInventory();
                        break;
                    case "3":
                        Rest();
                        break;
                    case "4":
                        Console.WriteLine("\n👋 Дякуємо за гру! До зустрічі!");
                        return;
                    default:
                        Console.WriteLine("\n❌ Невірний вибір!");
                        Console.ReadKey();
                        break;
                }
            }

            Console.WriteLine("\n💀 ГРА ЗАКІНЧЕНА 💀");
            Console.WriteLine($"Ви перемогли {battlesWon} ворогів.");
            Console.ReadKey();
        }

        private void StartBattle()
        {
            Enemy enemy = GenerateEnemy();
            Console.Clear();
            Console.WriteLine("\n⚔️ ═══════════════════════════════════════ ⚔️");
            Console.WriteLine($"   {enemy.Name} з'являється! БИТВА ПОЧИНАЄТЬСЯ!");
            Console.WriteLine("⚔️ ═══════════════════════════════════════ ⚔️\n");
            Console.WriteLine("Натисніть будь-яку клавішу...");
            Console.ReadKey();

            while (player.IsAlive && enemy.IsAlive)
            {
                Console.Clear();
                Console.WriteLine($"\n👤 {player.Name}: {player.Health}/{player.MaxHealth} HP | {player.Mana}/{player.MaxMana} Мана");
                Console.WriteLine($"👹 {enemy.Name}: {enemy.Health}/{enemy.MaxHealth} HP\n");

                Console.WriteLine("╔═══════════════════════════════════════╗");
                Console.WriteLine("║           ДІЇ В БОЮ                   ║");
                Console.WriteLine("╠═══════════════════════════════════════╣");
                Console.WriteLine("║ 1. Атакувати                          ║");
                Console.WriteLine("║ 2. Використати магію (20 мани)        ║");
                Console.WriteLine("║ 3. Використати предмет                ║");
                Console.WriteLine("╚═══════════════════════════════════════╝");
                Console.Write("\nВаш вибір: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                bool playerTurnTaken = false;

                switch (choice)
                {
                    case "1":
                        player.Attack(enemy);
                        playerTurnTaken = true;
                        break;
                    case "2":
                        player.CastSpell(enemy);
                        playerTurnTaken = true;
                        break;
                    case "3":
                        player.ShowInventory();
                        Console.Write("Виберіть предмет (номер): ");
                        if (int.TryParse(Console.ReadLine(), out int itemIndex))
                        {
                            player.UseItem(itemIndex);
                        }
                        else
                        {
                            Console.WriteLine("❌ Невірний вибір!");
                        }
                        break;
                    default:
                        Console.WriteLine("❌ Невірний вибір! Ви пропускаєте хід.");
                        playerTurnTaken = true;
                        break;
                }

                if (playerTurnTaken && enemy.IsAlive)
                {
                    Console.WriteLine();
                    enemy.Attack(player);
                }

                Console.WriteLine("\nНатисніть будь-яку клавішу...");
                Console.ReadKey();
            }

            if (player.IsAlive)
            {
                battlesWon++;
                Console.WriteLine($"\n🎉 Перемога! Ви перемогли {enemy.Name}!");
                player.GainExperience(enemy.ExperienceReward);

                List<Item> loot = enemy.GetLoot();
                if (loot.Count > 0)
                {
                    Console.WriteLine("\n💰 Здобич:");
                    foreach (Item item in loot)
                    {
                        player.AddItem(item);
                    }
                }

                Console.WriteLine("\nНатисніть будь-яку клавішу...");
                Console.ReadKey();
            }
        }

        private Enemy GenerateEnemy()
        {
            int enemyType = rand.Next(3);

            switch (enemyType)
            {
                case 0:
                    return new Goblin();
                case 1:
                    return new Troll();
                default:
                    return new DarkMage();
            }
        }

        private void ManageInventory()
        {
            Console.Clear();
            player.ShowInventory();
            Console.WriteLine("1. Використати предмет");
            Console.WriteLine("2. Повернутися");
            Console.Write("\nВаш вибір: ");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("Виберіть предмет (номер): ");
                if (int.TryParse(Console.ReadLine(), out int itemIndex))
                {
                    player.UseItem(itemIndex);
                }
                else
                {
                    Console.WriteLine("❌ Невірний вибір!");
                }
                Console.WriteLine("\nНатисніть будь-яку клавішу...");
                Console.ReadKey();
            }
        }

        private void Rest()
        {
            Console.Clear();
            Console.WriteLine("\n😴 Ви відпочиваєте біля вогнища...\n");
            player.Heal(player.MaxHealth);
            player.RestoreMana(player.MaxMana);
            Console.WriteLine("\n✨ Ви повністю відновилися!");
            Console.WriteLine("\nНатисніть будь-яку клавішу...");
            Console.ReadKey();
        }
    }


    public class Player : Character, ISpellCaster
    {
        private int _mana;
        private int _maxMana;
        private int _experience;
        private int _level;
        private IEquippable _equippableWeapon;
        private List<Item> _inventory;

       

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
