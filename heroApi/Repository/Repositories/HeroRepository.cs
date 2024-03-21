using Domain.Model;
using Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class HeroRepository: IHeroRepository
    {
        public async Task<Hero> InsertHero(Hero hero)
        {
            return hero;
        }
    }
}
