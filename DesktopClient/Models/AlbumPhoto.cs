using System;
using System.Collections.Generic;
using CoreApi;

namespace DesktopClient
{
    class AlbumPhoto
    {
        private Album album;
        public List<PhotoUri> Photos;

        public AlbumPhoto(Album album, AuthToken token)
        {
            this.album = album;
            Photos = new List<PhotoUri>();
            foreach (var item in album.photos)
            {
                Photos.Add(new PhotoUri(item, token));
            }
        }
    }
}
