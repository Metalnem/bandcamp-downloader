namespace Api.Model;

public class AlbumDetails
{
	public long Id { get; set; }
	public string Title { get; set; }
	public string BandcampUrl { get; set; }
	public BandInfo Band { get; set; }
	public string TralbumArtist { get; set; }
	public long AlbumId { get; set; }
	public string AlbumTitle { get; set; }
	public string Currency { get; set; }
	public decimal? Price { get; set; }
}
