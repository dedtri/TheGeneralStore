using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TheGeneralStore.Backend.Database;
using TheGeneralStore.Backend.Database.DataModels;
using TheGeneralStore.Backend.Database.QueryModels;
using TheGeneralStore.Backend.Database.Repositories;
using TheGeneralStore.Backend.WebAPI.Controllers.Resources;
using TheGeneralStore.Backend.WebAPI.Controllers.Resources.Images;
using TheGeneralStore.Backend.WebAPI.Persistence.Services;

namespace TheGeneralStore.Backend.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly UnitOfWork unitOfWork;
        private readonly ImageRepository imageRepository;
        private readonly ProductRepository productRepository;
        private readonly ImageService imageService;

        public ImagesController
            (
            IMapper mapper,
            UnitOfWork unitOfWork,
            ImageRepository imageRepository,
            ProductRepository productRepository,
            ImageService imageService
            )
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.imageRepository = imageRepository;
            this.productRepository = productRepository;
            this.imageService = imageService;
        }

        #region Create
        [HttpPost]
        public async Task<ActionResult<ImageResource>> Create([FromForm] ImageCreateResource createResource)
        {
            string statusString = "";
            if (!ModelState.IsValid)
            {
                statusString = "Please pass valid data.";
                return Ok(statusString);
            }

            if (createResource.ImageFile != null)
            {
                var imageResult = this.imageService.SaveImage(createResource);
                if (imageResult.Item1 == 1)
                {
                    createResource.ImageId = Guid.Parse(imageResult.Item2);
                    createResource.FileExtension = Path.GetExtension(createResource.ImageFile.FileName);

                    var entity = this.mapper.Map<ImageCreateResource, Image>(createResource);

                    try
                    {
                        this.imageRepository.Add(entity);
                        statusString = "Added succesfully!!";
                    }
                    catch (Exception)
                    {
                        statusString = "Error on adding an image.";
                        throw;
                    }
                }

                await this.unitOfWork.SaveChangesAsync();

                return Ok(new { Message = statusString });
            }
            return Ok(new { Message = statusString });
        }
        #endregion

        #region GetAll
        [HttpGet]
        public async Task<ActionResult<BaseQueryResultResource<ImageResource>>> GetAll([FromQuery] ImageQueryResource filter)
        {
            if (filter.PageSize == 0)
                filter.PageSize = -1;

            var query = new ImageQuery
            {
                Page = filter.Page,
                PageSize = filter.PageSize,
            };

            var queryResult = await this.imageRepository.GetAllAsync(query, true);

            var resource = this.mapper.Map<BaseQueryResult<Image>, BaseQueryResultResource<ImageResource>>(queryResult);

            return resource;
        }
        #endregion

        #region Get
        [HttpGet("{entityId}")]
        public async Task<ActionResult<ImageResource>> Get(int entityId)
        {
            var entity = await this.imageRepository.GetAsync(entityId);
            if (entity == null)
                return NotFound();

            var resource = this.mapper.Map<ImageResource>(entity);

            return resource;
        }
        #endregion

        #region Delete
        [HttpDelete("delete/{entityId}")]
        public async Task<ActionResult> Delete(int entityId)
        {
            var entity = await this.imageRepository.GetAsync(entityId);

            if (entity == null)
                return NotFound();

            var productEntity = await this.productRepository.GetAsync(entity.ProductId.Value);
            productEntity.IsDeleted = true;

            try
            {
                this.imageRepository.Remove(entity);
                this.productRepository.Update(productEntity);
                this.imageService.DeleteImage(entity);

                await this.unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }

            return Ok();
        }
        #endregion
    }
}
