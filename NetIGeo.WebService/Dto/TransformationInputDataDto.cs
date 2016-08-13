using System.Collections.Generic;

namespace NetIGeo.WebService.Dto
{
    public class TransformationInputDataDto
    {
        public ICollection<PointSetDto> PointSets { get; set; }
        public string OriginId { get; set; }
        public string DestinationId { get; set; }
    }
}