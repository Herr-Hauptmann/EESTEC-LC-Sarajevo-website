﻿using EESTEC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EESTEC.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Board> Boards { get; set; }
        public DbSet<BoardMember> BoardMembers { get; set; }
        public DbSet<InternationalEvent> InternationalEvents { get; set; }
        public DbSet<LocalEvent> LocalEvents { get; set; }
        public DbSet<EventFile> EventFiles { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<PartnerCategory> PartnerCategories { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
