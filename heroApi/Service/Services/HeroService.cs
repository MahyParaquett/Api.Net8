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

        public async Task<Hero> GetHero(Guid heroId)
        {
            return await repository.GetHero(heroId);
        }

        public async Task<IEnumerable<Hero>> GetHeros()
        {
            return await repository.GetHeros();
        }

        public async Task<Hero> InsertHero(Hero hero)
        {
            return await repository.InsertHero(hero);
        }

        public async Task<Hero> UpdateHero(Hero hero)
        {
            return await repository.UpdateHero(hero);
        }

        public async Task<bool> DeleteHero(Guid heroId)
        {
            return await repository.DeleteHero(heroId);
        }
    }
}
