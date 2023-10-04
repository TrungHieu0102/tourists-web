using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrungHieuTourists.Admin.Tours.Attributes;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace TrungHieuTourists.Admin.Tours
{
    public interface IToursAppService :ICrudAppService
        <TourDto,
        Guid,
        PagedResultRequestDto,
        CreateUpdateTourDto,
        CreateUpdateTourDto>
    {
        Task<PagedResultDto<TourInListDto>> GetListFilterAsync(TourListFilterDto input);
        Task<List<TourInListDto>> GetListAllAsync();
        Task DeleteMultipleAsync(IEnumerable<Guid> ids);
        Task<string> GetThumbnailImageAsync(string fileName);
        Task<string> GetSuggestNewCodeAsync();
        Task<TourAttributeValueDto> AddTourAttributeAsync(AddUpdateTourAttributeDto input);
        Task<TourAttributeValueDto> UpdateTourAttributeAsync(Guid id, AddUpdateTourAttributeDto input);

        Task RemoveTourAttributeAsync(Guid attributeId, Guid id);

        Task<List<TourAttributeValueDto>> GetListTourAttributeAllAsync(Guid productId);
        Task<PagedResultDto<TourAttributeValueDto>> GetListTourAttributesAsync(TourAttributeListFilterDto input);
    }
}
