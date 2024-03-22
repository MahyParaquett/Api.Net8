using Dapper;
using Domain.Model;
using Repository.Contracts;


namespace Repository.Repositories
{
    public class HeroRepository : IHeroRepository
    {
        private readonly IConnectionFactory connection;

        public HeroRepository(IConnectionFactory connection)
        {
            this.connection = connection;
        }

        public async Task<bool> DeleteHero(Guid heroId)
        {
            string sql = "Delete from public.hero where HeroId = @HeroId";
            string sqlFriendHero = "Delete from public.herofriends where HeroId = @HeroId";

            using (var connectionDb = connection.Connection())
            {
                connectionDb.Open();

                await connectionDb.QueryAsync<Hero>(sqlFriendHero,
                    new
                    {
                        HeroId = heroId
                    });

                await connectionDb.QueryAsync<Hero>(sql,
                    new
                    {
                        HeroId = heroId
                    });

                return true;
            }
        }

        public async Task<Hero> GetHero(Guid heroId)
        {
            string sql = "select * from public.hero where HeroId = @HeroId";
            string sqlHeroFriend = "select Name from HeroFriends where HeroId = @HeroId";

            IList<Hero> listHero = new List<Hero>();

            using (var connectionDb = connection.Connection())
            {
                connectionDb.Open();

                var result = await connectionDb.QueryFirstOrDefaultAsync<Hero>(sql,
                    new
                    {
                        HeroId = heroId
                    });

                if (result != null)
                {
                    var resultHeroFriend = await connectionDb.QueryAsync<dynamic>(sqlHeroFriend,
                           new
                           {
                               HeroId = result.HeroId
                           });

                    foreach (var friend in resultHeroFriend.ToList())
                    {
                        result.AddHeroFriend((string)friend.Name);
                    }
                }

                return result;
            }
        }

        public async Task<IEnumerable<Hero>> GetHeros()
        {
            string sql = "select * from public.hero";
            string sqlHeroFriend = "select Name from HeroFriends where HeroId = @HeroId";

            IList<Hero> listHero = new List<Hero>();

            using (var connectionDb = connection.Connection())
            {
                connectionDb.Open();

                var result = await connectionDb.QueryAsync<dynamic>(sql);

                if (result.Any())
                {
                    foreach (var item in result.ToList())
                    {
                        var hero = new Hero((Guid)item.HeroId, (string)item.Name, (string)item.Power);
                        var resultHeroFriend = await connectionDb.QueryAsync<dynamic>(sqlHeroFriend,
                            new
                            {
                                HeroId = hero.HeroId
                            });
                        if (resultHeroFriend.Any())
                        {
                            foreach (var friend in resultHeroFriend.ToList())
                            {
                                hero.AddHeroFriend((string)friend.Name);
                            }
                        }
                        listHero.Add(hero);
                    }
                }
                
            }
            return listHero;
        }

        public async Task<Hero> InsertHero(Hero hero)
        {
            string sql = "Insert into public.hero (HeroId,Name,Power) values (@HeroId, @Name, @Power)";

            using (var connectionDb = connection.Connection())
            {
                connectionDb.Open();

                var heroResult = await connectionDb.ExecuteAsync(sql,
                    new
                    {
                        HeroId = hero.HeroId,
                        Name = hero.Name,
                        Power = hero.Power
                    });

                if (hero.HeroFriend.Any())
                {
                    string sqlHeroFriend = "Insert into public.herofriends (HeroId, Name) values (@HeroId, @Name)";

                    foreach (var friend in hero.HeroFriend)
                    {
                        var heroFriendResult = await connectionDb.ExecuteAsync(sqlHeroFriend,
                        new
                        {
                            HeroId = hero.HeroId,
                            Name = friend
                        });
                    }
                }

                return hero;
            }
        }

        public async Task<Hero> UpdateHero(Hero hero)
        {
            string sql = "UPDATE public.hero SET Name = @Name, Power= @Power WHERE HeroId = @HeroId;";

            using (var connectionDb = connection.Connection())
            {
                connectionDb.Open();

                var heroResult = await connectionDb.ExecuteAsync(sql,
                    new
                    {
                        HeroId = hero.HeroId,
                        Name = hero.Name,
                        Power = hero.Power
                    });

                return hero;

            }
        }
    }
}
