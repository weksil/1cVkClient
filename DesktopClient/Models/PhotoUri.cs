using System;
using CoreApi;

namespace DesktopClient
{
    class PhotoUri
    {
        public Photo Picture { get; set; }
        public Uri PuctureUri { get; set; }
        public int Id { get { return Picture.id; }  }
        public PhotoUri(Photo picture, AuthToken token)
        {
            Picture = picture;
            PuctureUri = Connector.GetPhotoUri(picture.link, token);
        }
        public override string ToString()
        {
            return Picture.ToString();
        }
    }
}
