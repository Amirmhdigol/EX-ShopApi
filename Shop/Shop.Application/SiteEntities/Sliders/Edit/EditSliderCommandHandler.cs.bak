using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Microsoft.AspNetCore.Http;
using Shop.Application._Utilities;
using Shop.Domain.SiteEntities.Repositories;

namespace Shop.Application.SiteEntities.Sliders.Edit
{
    internal class EditSliderCommandHandler : IBaseCommandHandler<EditSliderCommand>
    {
        private readonly IFileService _fileService;
        private readonly ISliderRepository _repository;
        public EditSliderCommandHandler(IFileService fileService, ISliderRepository repository)
        {
            _fileService = fileService;
            _repository = repository;
        }

        public async Task<OperationResult> Handle(EditSliderCommand request, CancellationToken cancellationToken)
        {
            var slider = await _repository.GetTracking(request.Id);
            if (slider == null)
                return OperationResult.NotFound();

            var imageName = slider.ImageName;
            var oldName = slider.ImageName;
            if (request.ImageFile != null)
                imageName = await _fileService.SaveFileAndGenerateName(request.ImageFile, Directories.BannerImages);

            slider.Edit(request.Title, request.Link, imageName);
            await _repository.Save();
            DeleteOldImage(request.ImageFile,oldName);
            return OperationResult.Success();
        }
        private void DeleteOldImage(IFormFile? imageFile,string oldImageName)
        {
            if (imageFile != null)
                _fileService.DeleteFile(Directories.SliderImages, oldImageName);
        }
    }

}
