using System;
using System.Collections.Generic;

namespace Models
{
    [Serializable]
    public class BookModel
    {
        public string Name { get; set; }
        public List<int> GenreIndexes { get; set; }
        public int Stars { get; set; }
        public string Description { get; set; }
    }
}