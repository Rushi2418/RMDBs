using static RMDB_Utility.Class1;

public class MovieIMG
{
    public int ID { get; set; }
   
    public int? MovieID { get; set; }
    public int? ActorId { get; set; }
    public string FilePath { get; set; }

    public bool? DefaultFlag { get; set; }
    public bool? ActorImgFlag { get; set; }
    public bool? MovieImgFlag { get; set; }
    public decimal? SortOrder { get; set; }
    public bool? ActiveFlag { get; set; }
}
