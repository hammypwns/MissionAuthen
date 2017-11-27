using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MissionAuthen.Models;
using System.Data.Entity;

namespace MissionAuthen.DAL
{
    public class MISSION_HQContext : DbContext
    {
        public MISSION_HQContext() : base("MISSION_HQContext")
        {

        }

        public DbSet<Mission> Missions { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Response> Responses { get; set; }

        public System.Data.Entity.DbSet<MissionAuthen.Models.User> Users { get; set; }
    }
}