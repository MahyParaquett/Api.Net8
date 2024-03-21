namespace Domain.Model
{
    public class Hero
    {
        private IList<string> heroFriend;

        public Hero(string name, string power)
        {
            HeroId = Guid.NewGuid();
            Name = name;
            Power = power;
            heroFriend = new List<string>();
        }

        public Guid HeroId { get; set; }
        public string Name { get; set; }
        public string Power { get; set; }

        public IReadOnlyCollection<string> HeroFriend
        {
            get
            {
                return heroFriend.ToArray();
            }
        }

        public void AddHeroFriend(string hero)
        {
            heroFriend.Add(hero);
        }
    }
}
