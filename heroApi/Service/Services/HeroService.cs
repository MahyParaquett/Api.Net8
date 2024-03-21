using Domain.Model;
using Repository.Contracts;
using Service.Contracts;

namespace Service.Services
{
    public class HeroService : IHeroService
    {
        private readonly IHeroRepository repository;

        public HeroService(IHeroRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Hero> InsertHero(Hero hero)
        {
            if (hero.Name.Equals("Iron Man"))
            {
                var friends = new List<string> { "Hulk", "Capitan America" };
                foreach (var friend in friends)
                {
                    hero.AddHeroFriend(friend);
                }
            }
            return await repository.InsertHero(hero);
        }
    }
}
