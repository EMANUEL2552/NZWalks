
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;
using System.Reflection.Emit;
namespace NZWalks.API.Data

{
	public class NZWalksDbContext: DbContext
	{
        public NZWalksDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
		{

			

		}
        
        public DbSet<Difficulty> Difficulties { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Walk> Walks { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			//seed data for difficulties
			//easy, medium, hard

			var difficulties = new List<Difficulty>()
			{
				new Difficulty
				{
					Id = Guid.Parse("78a553b7-7dd2-483e-bd19-091db1450847"),
					Name = "Easy"

				},

				new Difficulty
				{
					Id = Guid.Parse("f0637c0e-3376-46f7-b9f1-74730273e23d"),
					Name = "Medium"

				},

				new Difficulty
				{
					Id = Guid.Parse("72a98726-6f46-41ca-a979-17deeee4c052"),
					Name = "Hard"

				}
			};

			// seed difficulties to the database
			modelBuilder.Entity<Difficulty>().HasData(difficulties);

			//seed data for regions
			var regions = new List<Region>()
			{
			  new Region
			  {
				  Id = Guid.Parse("3a797e2b-14b3-4f72-a03b-e655627eb305"),
				  Name = "Auckland",
				  Code = "ACK",
				  RegionImageUrl = "https://www.pexels.com/pt-br/foto/corpo-calmo-de-lago-entre-montanhas-346529/"
			  },

			  new Region 
			  {
				  Id = Guid.Parse("6884f7d7-ad1f-4101-8df3-7a6fa7387d81"),
					Name = "Northland",
					Code = "NTL",
					RegionImageUrl = null

			  },

			  new Region 
			  {
			        Id = Guid.Parse("14ceba71-4b51-4777-9b17-46602cf66153"),
					Name = "Bay Of Plenty",
					Code = "BOP",
					RegionImageUrl = null

			  },

			  new Region
			  {
					Id = Guid.Parse("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"),
					Name = "Wellington",
					Code = "WGN",
					RegionImageUrl = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
			  },

			  new Region
			  {
					Id = Guid.Parse("906cb139-415a-4bbb-a174-1a1faf9fb1f6"),
					Name = "Nelson",
					Code = "NSN",
					RegionImageUrl = "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
			  },

			  new Region 
			  {
			        Id = Guid.Parse("f077a22e-4248-4bf6-b564-c7cf4e250263"),
					Name = "Southland",
					Code = "STL",
					RegionImageUrl = null

			  },

			  

			};

			modelBuilder.Entity<Region>().HasData(regions);
		}

		


	}
}
