using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMDBs_API.Migrations
{
    /// <inheritdoc />
    public partial class AddAllDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Actors",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: true),
                    Height = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    BornDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BornPlace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Parents = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Chilldren = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DebutMovie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Biography = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActiveFlag = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actors", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AwardCategories",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ActiveFlag = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AwardCategories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Awards",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ActiveFlag = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Awards", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ActiveFlag = table.Column<bool>(type: "bit", nullable: false),
                    SortOrder = table.Column<decimal>(type: "decimal(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    productionStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Budget = table.Column<decimal>(type: "decimal(10,4)", nullable: false),
                    BoxOfficeCollection = table.Column<decimal>(type: "decimal(10,4)", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActiveFlag = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ActiveFlag = table.Column<bool>(type: "bit", nullable: false),
                    SortOrder = table.Column<decimal>(type: "decimal(5,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ReceiverTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActiveFlag = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiverTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DateJoined = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProfilePicture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobileNumber = table.Column<long>(type: "bigint", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActiveFlag = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MovieGenres",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieID = table.Column<int>(type: "int", nullable: false),
                    GenreID = table.Column<int>(type: "int", nullable: false),
                    ActiveFlag = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieGenres", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MovieGenres_Genres_GenreID",
                        column: x => x.GenreID,
                        principalTable: "Genres",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieGenres_Movies_MovieID",
                        column: x => x.MovieID,
                        principalTable: "Movies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Popularity",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieID = table.Column<int>(type: "int", nullable: false),
                    PopularityScore = table.Column<decimal>(type: "decimal(5,4)", nullable: false),
                    RecordedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Popularity", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Popularity_Movies_MovieID",
                        column: x => x.MovieID,
                        principalTable: "Movies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieCasts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieID = table.Column<int>(type: "int", nullable: false),
                    ActorID = table.Column<int>(type: "int", nullable: false),
                    PositionID = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActiveFlag = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieCasts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MovieCasts_Actors_ActorID",
                        column: x => x.ActorID,
                        principalTable: "Actors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieCasts_Movies_MovieID",
                        column: x => x.MovieID,
                        principalTable: "Movies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieCasts_Positions_PositionID",
                        column: x => x.PositionID,
                        principalTable: "Positions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActorMovieAwards",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Typeid = table.Column<int>(type: "int", nullable: false),
                    ActorID = table.Column<int>(type: "int", nullable: true),
                    MovieID = table.Column<int>(type: "int", nullable: true),
                    AwardID = table.Column<int>(type: "int", nullable: false),
                    AwardCategoryID = table.Column<int>(type: "int", nullable: false),
                    AwardDiscription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorMovieAwards", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ActorMovieAwards_Actors_ActorID",
                        column: x => x.ActorID,
                        principalTable: "Actors",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ActorMovieAwards_AwardCategories_AwardCategoryID",
                        column: x => x.AwardCategoryID,
                        principalTable: "AwardCategories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActorMovieAwards_Awards_AwardID",
                        column: x => x.AwardID,
                        principalTable: "Awards",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActorMovieAwards_Movies_MovieID",
                        column: x => x.MovieID,
                        principalTable: "Movies",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ActorMovieAwards_ReceiverTypes_Typeid",
                        column: x => x.Typeid,
                        principalTable: "ReceiverTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieMedias1",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceiverId = table.Column<int>(type: "int", nullable: false),
                    MovieID = table.Column<int>(type: "int", nullable: true),
                    ActroId = table.Column<int>(type: "int", nullable: true),
                    MediaType = table.Column<int>(type: "int", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DefaultFlag = table.Column<bool>(type: "bit", nullable: false),
                    MovieTrailer = table.Column<bool>(type: "bit", nullable: false),
                    ActorImgFlag = table.Column<bool>(type: "bit", nullable: false),
                    MovieImgFlag = table.Column<bool>(type: "bit", nullable: false),
                    SortOrder = table.Column<decimal>(type: "decimal(5,4)", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActiveFlag = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieMedias1", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MovieMedias1_Actors_ActroId",
                        column: x => x.ActroId,
                        principalTable: "Actors",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_MovieMedias1_Movies_MovieID",
                        column: x => x.MovieID,
                        principalTable: "Movies",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_MovieMedias1_ReceiverTypes_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "ReceiverTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    MovieID = table.Column<int>(type: "int", nullable: false),
                    CommentText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActiveFlag = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Comments_Movies_MovieID",
                        column: x => x.MovieID,
                        principalTable: "Movies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRatings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    MovieID = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    RatingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReviewText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DefaultRatingScore = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRatings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserRatings_Movies_MovieID",
                        column: x => x.MovieID,
                        principalTable: "Movies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRatings_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.Sql(@"
ALTER TABLE MovieMedias1
ADD CONSTRAINT CK_MovieMedia_ReceiverValidation
CHECK (
    (ReceiverId = 1 AND ActroId IS NOT NULL AND MovieId IS NULL) OR
    (ReceiverId = 2 AND MovieId IS NOT NULL AND ActroId IS NULL)
)
");

            migrationBuilder.Sql(@"
    ALTER TABLE ActorMovieAwards
    ADD CONSTRAINT CK_ActorMovieAward_ActorMovieValidation
    CHECK (
        (Typeid = 1 AND ActorID IS NOT NULL AND MovieID IS NULL) OR
        (Typeid = 2 AND MovieID IS NOT NULL AND ActorID IS NULL) OR
        (Typeid = 3 AND MovieID IS NOT NULL AND ActorID IS NOT NULL)
    );
");

            migrationBuilder.CreateIndex(
                name: "IX_ActorMovieAwards_ActorID",
                table: "ActorMovieAwards",
                column: "ActorID");

            migrationBuilder.CreateIndex(
                name: "IX_ActorMovieAwards_AwardCategoryID",
                table: "ActorMovieAwards",
                column: "AwardCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_ActorMovieAwards_AwardID",
                table: "ActorMovieAwards",
                column: "AwardID");

            migrationBuilder.CreateIndex(
                name: "IX_ActorMovieAwards_MovieID",
                table: "ActorMovieAwards",
                column: "MovieID");

            migrationBuilder.CreateIndex(
                name: "IX_ActorMovieAwards_Typeid",
                table: "ActorMovieAwards",
                column: "Typeid");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_MovieID",
                table: "Comments",
                column: "MovieID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserID",
                table: "Comments",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_MovieCasts_ActorID",
                table: "MovieCasts",
                column: "ActorID");

            migrationBuilder.CreateIndex(
                name: "IX_MovieCasts_MovieID",
                table: "MovieCasts",
                column: "MovieID");

            migrationBuilder.CreateIndex(
                name: "IX_MovieCasts_PositionID",
                table: "MovieCasts",
                column: "PositionID");

            migrationBuilder.CreateIndex(
                name: "IX_MovieGenres_GenreID",
                table: "MovieGenres",
                column: "GenreID");

            migrationBuilder.CreateIndex(
                name: "IX_MovieGenres_MovieID",
                table: "MovieGenres",
                column: "MovieID");

            migrationBuilder.CreateIndex(
                name: "IX_MovieMedias1_ActroId",
                table: "MovieMedias1",
                column: "ActroId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieMedias1_MovieID",
                table: "MovieMedias1",
                column: "MovieID");

            migrationBuilder.CreateIndex(
                name: "IX_MovieMedias1_ReceiverId",
                table: "MovieMedias1",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Popularity_MovieID",
                table: "Popularity",
                column: "MovieID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRatings_MovieID",
                table: "UserRatings",
                column: "MovieID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRatings_UserID",
                table: "UserRatings",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActorMovieAwards");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "MovieCasts");

            migrationBuilder.DropTable(
                name: "MovieGenres");

            migrationBuilder.DropTable(
                name: "MovieMedias1");

            migrationBuilder.DropTable(
                name: "Popularity");

            migrationBuilder.DropTable(
                name: "UserRatings");

            migrationBuilder.DropTable(
                name: "AwardCategories");

            migrationBuilder.DropTable(
                name: "Awards");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Actors");

            migrationBuilder.DropTable(
                name: "ReceiverTypes");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
