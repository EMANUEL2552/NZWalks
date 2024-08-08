namespace NZWalks.API.Models.Domain
{
	public class Walk
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public double LenghInKm { get; set; }

		public string WalkImageUrl { get;}

		public Guid DifficultyId { get; set; }

		public Guid RegionId { get; set; }


		//navigation properties
		public Difficulty Difficulty { get; set; }

		public Difficulty Region { get; set; }
	}
}
