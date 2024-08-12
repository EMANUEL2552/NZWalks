using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RegionsController : ControllerBase
	{
		private readonly NZWalksDbContext dbContext;

        public RegionsController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        //GET ALL REGIONS
        [HttpGet]
		public IActionResult GetAll()
		{
			var regionsDomain = dbContext.Regions.ToList();
			

			//MAP DOMAIN MODELS TO DTOS
			var regionsDto = new List<RegionDTO>();
			foreach (var regionDomain in regionsDomain) 
			{
				regionsDto.Add(new RegionDTO()
				{
					Id = regionDomain.Id,
					Code = regionDomain.Code,
					Name = regionDomain.Name,
					RegionImageUrl = regionDomain.RegionImageUrl

				});
			}


			return Ok(regionsDto);
		}

		//GET REGION BY ID
		[HttpGet]
		[Route("{id:guid}")]
		public IActionResult GetById([FromRoute]Guid id) 
		{

          //GET REGION DOMAIN MODEL FROM DATABASE
		  var regionDomain = dbContext.Regions.FirstOrDefault(x => x.Id == id);

			if(regionDomain == null) 
			{
			  return NotFound();
			}

			//Map/Convert Region Domain Model to region DTO
			//
			var regionDto = new RegionDTO
			{
				Id = regionDomain.Id,
				Code = regionDomain.Code,
				Name = regionDomain.Name,
				RegionImageUrl = regionDomain.RegionImageUrl

			};

			//return DTO back to client
			return Ok(regionDto);
		}
	}
}
