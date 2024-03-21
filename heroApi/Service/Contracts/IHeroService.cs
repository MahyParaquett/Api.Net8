using Domain.Model;

namespace Service.Contracts
{
    public interface IHeroService
    {
        Task<IEnumerable<Hero>> GetHeros();
        Task<Hero> InsertHero(Hero hero);
        Task<Hero> UpdateHero(Hero hero);
        Task<Hero> GetHero(Guid heroId);
        Task<bool> DeleteHero(Guid heroId);
    }
}
