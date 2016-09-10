using System;
using System.Collections.Generic;
using System.Linq;

namespace NetIGeo.Domain.Models
{
    public class ProjectModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public IEnumerable<PointModel> Points { get; set; } = Enumerable.Empty<PointModel>();
    }
}