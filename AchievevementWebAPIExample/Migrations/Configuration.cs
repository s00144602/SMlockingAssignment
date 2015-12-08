namespace AchievevementWebAPIExample.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;
    using Microsoft.AspNet.Identity;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<AchievevementWebAPIExample.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "AchievevementWebAPIExample.Models.ApplicationDbContext";
        }
        int _usercounter = 0;
        public int Counter { get { return _usercounter++; } }

        int _emailCounter = 0;
        public int EmailCounter { get { return _emailCounter++; } }

        protected override void Seed(AchievevementWebAPIExample.Models.ApplicationDbContext context)
        {

            context.Games.AddOrUpdate(g => g.GameName,
                new Game { GameName = "Battle Call" },
                new Game { GameName = "Pong" });

            context.SaveChanges();
            #region create players
            Random r = new Random();
            PasswordHasher hasher = new PasswordHasher();
            context.Users.AddOrUpdate(u => u.UserName,
                new ApplicationUser
                {
                    XP = r.Next(400),
                    UserName = "User " + Counter.ToString() + "@itsligo.ie",
                    Email = "User " + EmailCounter.ToString() + "@itsligo.ie",
                    EmailConfirmed = true,
                    GamerTag = "GamerTag" + _usercounter.ToString(), 
                    PasswordHash = hasher.HashPassword("GamerTag" + _usercounter.ToString()),
                    
                },
                new ApplicationUser
                {
                    XP = r.Next(400),
                    UserName = "User " + Counter.ToString() + "@itsligo.ie",
                    Email = "User " + EmailCounter.ToString() + "@itsligo.ie",
                    EmailConfirmed = true,
                    GamerTag = "GamerTag" + _usercounter.ToString(),
                    PasswordHash = hasher.HashPassword("GamerTag" + _usercounter.ToString()),
                },
                new ApplicationUser
                {
                    XP = r.Next(400),
                    UserName = "User " + Counter.ToString() + "@itsligo.ie",
                    Email = "User " + EmailCounter.ToString() + "@itsligo.ie",
                    EmailConfirmed = true,
                    GamerTag = "GamerTag" + _usercounter.ToString(),
                    PasswordHash = hasher.HashPassword("GamerTag" + _usercounter.ToString()),
                },
                new ApplicationUser
                {
                    XP = r.Next(400),
                    UserName = "User " + Counter.ToString() + "@itsligo.ie",
                    Email = "User " + EmailCounter.ToString() + "@itsligo.ie",
                    EmailConfirmed = true,
                    GamerTag = "GamerTag" + _usercounter.ToString(),
                    PasswordHash = hasher.HashPassword("GamerTag" + _usercounter.ToString()),
                },
                new ApplicationUser {
                XP = r.Next(400),
                    UserName = "User " + Counter.ToString() + "@itsligo.ie",
                    Email = "User " + EmailCounter.ToString() + "@itsligo.ie",
                    EmailConfirmed = true,
                    GamerTag = "GamerTag" + _usercounter.ToString(),
                    PasswordHash = hasher.HashPassword("GamerTag" + _usercounter.ToString()),
                },
                new ApplicationUser
                {
                    XP = r.Next(400),
                    UserName = "User " + Counter.ToString() + "@itsligo.ie",
                    Email = "User " + EmailCounter.ToString() + "@itsligo.ie",
                    EmailConfirmed = true,
                    GamerTag = "GamerTag" + _usercounter.ToString(),
                    PasswordHash = hasher.HashPassword("GamerTag" + _usercounter.ToString()),
                },
                new ApplicationUser
                {
                    XP = r.Next(400),
                    UserName = "User " + Counter.ToString() + "@itsligo.ie",
                    Email = "User " + EmailCounter.ToString() + "@itsligo.ie",
                    EmailConfirmed = true,
                    GamerTag = "GamerTag" + _usercounter.ToString(),
                    PasswordHash = hasher.HashPassword("GamerTag" + _usercounter.ToString()),
                },
                new ApplicationUser
                {
                    XP = r.Next(400),
                    UserName = "User " + Counter.ToString() + "@itsligo.ie",
                    Email = "User " + EmailCounter.ToString() + "@itsligo.ie",
                    EmailConfirmed = true,
                    GamerTag = "GamerTag" + _usercounter.ToString(),
                    PasswordHash = hasher.HashPassword("GamerTag" + _usercounter.ToString()),
                }
                );
            context.SaveChanges();
            #endregion

            #region create games and scores
            List<GameScore> scores = new List<GameScore>();
            Game bg = context.Games.FirstOrDefault(battle => battle.GameName == "Battle Call");
            if (bg != null)
            {
                foreach (ApplicationUser player in context.Users)
                {
                    //context.GameScores.AddOrUpdate(score => score.PlayerID,
                    scores.Add(new GameScore
                    {
                        PlayerID = player.Id,
                        score = r.Next(1200),
                        GameID = bg.GameID
                    }
                    );
                }
                context.GameScores.AddOrUpdate(score => score.PlayerID,
                    scores.ToArray());

                context.SaveChanges();
            }
                #endregion

                Achievement[] achievements = new Achievement[14];
                for (int i = 0; i < 14; i++)
                {
                    achievements[i] = new Achievement { Name = "Badges_" + i.ToString() };
                }
                context.Achievements.AddOrUpdate(a => a.Name,
                    achievements
                    );
                context.SaveChanges();
            // Get a copy of the current players and Achievements as you cannot work on more than one
            // Data set in the one context
            List<Achievement> _achviements = context.Achievements.ToList();
            List<ApplicationUser> _players = context.Users.ToList();
            // A list to hold the new player acheivements
            List<PlayerAchievement> _playerAchievements = new List<PlayerAchievement>();
                foreach (ApplicationUser player in _players)
                {
                    var acs = (from ac in _achviements
                               select new
                               {
                                   ac.ID,
                                   localid = Guid.NewGuid()
                               }).OrderBy(q => q.localid).ToList();

                    var topFiveAchiements = acs.Select(ac => ac.ID).Take(5);

                    foreach(var achievementID in topFiveAchiements)
                    {
                        _playerAchievements.Add(new
                            PlayerAchievement
                        { PlayerID = player.Id, AchievementID = achievementID });
                    }
                }
                context.PlayerAchievements.AddOrUpdate(
                            rep => new { rep.PlayerID, rep.AchievementID },
                            _playerAchievements.ToArray()
                            );

            
            context.SaveChanges();
        }
    }
}
