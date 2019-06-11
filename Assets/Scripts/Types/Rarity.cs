namespace Akangatu
{
    public enum Rarity
    {
        Frequent = 0,
        Unusual = 1,
        Rare = 2,
    }

    public static class RarityMethods
    {
        public static void FromF(float r, out Rarity rarity)
        {
            const float P1 = 0.5f;
            const float P2 = 0.8f;

            if (r >= 0f && r < P1)
                rarity = Rarity.Frequent;
            else if (r >= P1 && r < P2)
                rarity = Rarity.Unusual;
            else
                rarity = Rarity.Rare;
        }
    }
}
