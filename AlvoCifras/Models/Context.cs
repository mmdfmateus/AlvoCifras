﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlvoCifras.Models;
using System.Threading;

namespace AlvoCifras.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<Artist> Artist { get; set; }
        public DbSet<Songs> Songs { get; set; }
        public DbSet<Lyrics> Lyrics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Remove o comportamento padrão de delete cascade
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            SetProperties();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void SetProperties()
        {
            foreach (var auditableEntity in ChangeTracker.Entries<BaseModel>())
            {
                if (auditableEntity.State == EntityState.Added)
                {
                    auditableEntity.Entity.CreatedAt = DateTime.Now;
                }
                else
                {
                    auditableEntity.Property(p => p.CreatedAt).IsModified = false;
                }
            }
        }        
    }
}
