using System;
using System.Collections.Generic;
using System.Linq;

namespace NetIGeo.DataAccess.Documents
{
    public class ProjectDocument : IDocument
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public IEnumerable<PointDocument> Points { get; set; } = Enumerable.Empty<PointDocument>();
    }
}