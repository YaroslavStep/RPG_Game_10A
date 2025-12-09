namespace RPG_Game
{
    public abstract class Enemy : Character
    {
        private int _experienceReward;
        private List<Item> _loot;

        public int ExperienceReward
        {
            get => _experienceReward;
            set => _experienceReward = value;
        }
        protected Enemy(string name, int health, int strength, int experienceReward) : base(name, health, strength)
        {
            _experienceReward = experienceReward;
            _loot = new List<Item>();
        }

        protected void AddLoot(Item item)
        {
            _loot.Add(item);
        }

        public List<Item> GetLoot()
        {
            return new List<Item>(_loot);
        }
    }
}
