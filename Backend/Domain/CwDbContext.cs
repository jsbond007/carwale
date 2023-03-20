using Carwale.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Carwale.Domain
{
	public class CwDbContext : DbContext
	{
		private readonly IConfiguration configuration;
		public CwDbContext(DbContextOptions<CwDbContext> options, IConfiguration configuration) : base(options)
		{
			this.configuration = configuration;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder
				.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
			}

			base.OnConfiguring(optionsBuilder);
		}

		public DbSet<Car> Cars { get; set; }

		public DbSet<Make> Makes { get; set; }

		public DbSet<Model> Models { get; set; }

        public DbSet<Tenant> Tenants { get; set; }

        public DbSet<CwUser> Users { get; set; }
    }

}	
