namespace Api.Model;

public class Album
{
	public string Token { get; set; }
	public long TralbumId { get; set; }
	public string Title { get; set; }
	public string Artist { get; set; }
	public string PageUrl { get; set; }
	public long ArtId { get; set; }
	public List<Track> Tracks { get; set; }
	public BandInfo BandInfo { get; set; }
}
