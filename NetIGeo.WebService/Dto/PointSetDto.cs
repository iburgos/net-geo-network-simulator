using System.Collections.Generic;

namespace NetIGeo.WebService.Dto
{
    public class PointSetDto
    {
        public string Id { get; set; }
        public ICollection<PointDto> Points { get; set; }
    }
}