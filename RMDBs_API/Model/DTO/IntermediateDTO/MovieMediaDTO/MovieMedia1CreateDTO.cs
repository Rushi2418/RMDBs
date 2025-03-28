using static RMDB_Utility.Class1;
public class MovieMedia1CreateDTO
{
    public int ReceiverId { get; set; }
    public int? MovieID { get; set; }
    public int? ActorId { get; set; }
    public MediaType MediaType { get; set; } = MediaType.Video;
    public string FilePath { get; set; }
    public bool DefaultFlag { get; set; }
    public bool MovieTrailer { get; set; }
    public bool ActorImgFlag { get; set; }
    public bool MovieImgFlag { get; set; }
    public decimal SortOrder { get; set; }
    public DateTime UploadDate { get; set; }
    public bool IsValid()
    {
        if (ReceiverId == 1 && !MovieID.HasValue)
        {
            return false; // MovieID is required for ReceiverId 1
        }
        if (ReceiverId == 2 && !ActorId.HasValue)
        {
            return false; // ActorId is required for ReceiverId 2
        }
        return true;
    }
}
