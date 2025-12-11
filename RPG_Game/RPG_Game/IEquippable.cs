namespace RPG_Game
{
    public interface IEquippable
    {
        string Name { get; }

        void Equip(Player player);
        void Unequip(Player player);
    }
}
