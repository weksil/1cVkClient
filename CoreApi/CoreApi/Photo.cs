using System.Collections.Generic;

namespace CoreApi
{
    public class Photo
    {
        public int id { get; set; }
        public string link { get; set; }
        public string title { get; set; }
        public override string ToString()
        {
            return title;
        }
    }

    public class Album
    {
        public int count { get; set; }
        public List<Photo> photos { get; set; }
    }
}
