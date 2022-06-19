using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Shop.Application._Utilities;
using Shop.Domain.SiteEntities.Repositories;

namespace Shop.Application.SiteEntities.Banners.Delete;

public class DeleteBannerCommandHandler : IBaseCommandHandler<DeleteBannerCommand>
{
    private readonly IBannerRepository _bannerRepository;
    private readonly IFileService _fileService;
    public DeleteBannerCommandHandler(IBannerRepository bannerRepository, IFileService fileService)
    {
        _bannerRepository = bannerRepository;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(DeleteBannerCommand request, CancellationToken cancellationToken)
    {
        var banner = await _bannerRepository.GetTracking(request.BannerId);
        if (banner == null) return OperationResult.NotFound();

        _bannerRepository.Delete(banner);

        await _bannerRepository.Save();
        _fileService.DeleteFile(Directories.BannerImages, banner.ImageName);
        return OperationResult.Success();
    }
}