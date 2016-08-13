using System;

namespace NetIGeo.DataAccess.Documents
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EditDate { get; set; }
    }
}