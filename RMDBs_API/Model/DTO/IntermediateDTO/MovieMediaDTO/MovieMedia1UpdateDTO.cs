using static RMDB_Utility.Class1;

public class MovieMedia1UpdateDTO
{
    public int ID { get; set; }
    public int ReceiverId { get; set; }
    public int? MovieID { get; set; }
    public int? ActorId { get; set; }
    public MediaType MediaType { get; set; }
    public string FilePath { get; set; }
    public bool DefaultFlag { get; set; }
    public bool MovieTrailer { get; set; }
    public bool ActorImgFlag { get; set; }
    public bool MovieImgFlag { get; set; }
    public decimal SortOrder { get; set; }
    public DateTime UploadDate { get; set; }
    public bool ActiveFlag { get; set; }
}