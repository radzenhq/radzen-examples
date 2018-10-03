export interface Album {
  album_type: string;
  artists: Array<Artist>;
  id: string;
  images: Array<Image>;
  name: string;
}

export interface Albums {
  items: Array<Album>;
}

export interface AlbumsResponse {
  albums: Albums;
}

export interface Artist {
  name: string;
}

export interface Image {
  url: string;
}

export interface Track {
  name: string;
  preview_url: string;
}

export interface TracksResponse {
  items: Array<Track>;
}
