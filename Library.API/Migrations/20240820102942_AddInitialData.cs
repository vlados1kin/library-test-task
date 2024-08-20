using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Library.API.Migrations
{
    /// <inheritdoc />
    public partial class AddInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Author",
                columns: new[] { "Id", "Birthday", "Country", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("2fc63da3-b9ed-480d-9543-a31037599bbb"), new DateOnly(1947, 9, 21), "USA", "Stephen", "King" },
                    { new Guid("944936e4-5167-41fe-8373-0540f319c3d3"), new DateOnly(1882, 11, 3), "Belarus", "Yakub", "Kolas" },
                    { new Guid("ac9417d1-a277-4974-b41c-25abde25bf38"), new DateOnly(1799, 5, 26), "Russia", "Alexander", "Pushkin" }
                });

            migrationBuilder.InsertData(
                table: "Genre",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("2330f6c1-3212-44fd-b231-c9ee4ffe196e"), "Novel" },
                    { new Guid("27e0db66-79ef-448d-b20c-af2c1a769e6b"), "Poem" },
                    { new Guid("3898242b-a685-4d49-bd06-8c7e34e14d7c"), "Novel in verse" }
                });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "Id", "AuthorId", "GenreId", "ISBN", "Name", "ReceiveTime", "ReturnTime", "Title" },
                values: new object[,]
                {
                    { new Guid("130fa159-70c1-42fb-8f31-5907f04b20e2"), new Guid("944936e4-5167-41fe-8373-0540f319c3d3"), new Guid("27e0db66-79ef-448d-b20c-af2c1a769e6b"), "978-9-8588-1435-9", "Heritage", new DateTime(2024, 8, 20, 13, 29, 41, 55, DateTimeKind.Local).AddTicks(3666), new DateTime(2024, 8, 20, 13, 29, 41, 55, DateTimeKind.Local).AddTicks(3668), "The book of People's Poet of Belarus Yanka Kupala includes verses and poems that give an idea of the main stages of his creative path, the ideological, thematic and genre richness of his poetry, the peculiarities of his artistic skill." },
                    { new Guid("2c463c56-21eb-4aae-8c2b-a87bcda80256"), new Guid("944936e4-5167-41fe-8373-0540f319c3d3"), new Guid("27e0db66-79ef-448d-b20c-af2c1a769e6b"), "978-9-8515-5288-3", "The New Land", new DateTime(2024, 8, 20, 13, 29, 41, 55, DateTimeKind.Local).AddTicks(3656), new DateTime(2024, 8, 20, 13, 29, 41, 55, DateTimeKind.Local).AddTicks(3657), "The first Belarusian lyric-epic work." },
                    { new Guid("4dccb0be-1de4-4659-80d2-0cf971a0d599"), new Guid("ac9417d1-a277-4974-b41c-25abde25bf38"), new Guid("3898242b-a685-4d49-bd06-8c7e34e14d7c"), "978-0-4608-7595-0", "Eugene Onegin", new DateTime(2024, 8, 20, 13, 29, 41, 55, DateTimeKind.Local).AddTicks(3645), new DateTime(2024, 8, 20, 13, 29, 41, 55, DateTimeKind.Local).AddTicks(3647), "Eugene Onegin is the master work of the poet whom Russians regard as the fountainhead of their literature." },
                    { new Guid("bd304d0c-66b5-4873-bd22-c83f804ca7b7"), new Guid("2fc63da3-b9ed-480d-9543-a31037599bbb"), new Guid("2330f6c1-3212-44fd-b231-c9ee4ffe196e"), "978-2-2264-9274-6", "Holly", new DateTime(2024, 8, 20, 13, 29, 41, 55, DateTimeKind.Local).AddTicks(3634), new DateTime(2024, 8, 20, 13, 29, 41, 55, DateTimeKind.Local).AddTicks(3635), "Holly Gibney, one of Stephen King’s most compelling and resourceful characters, returns in this chilling novel to solve the gruesome truth behind multiple disappearances in a midwestern town." },
                    { new Guid("f86f763e-7f80-491d-ac29-ec93cf0048e0"), new Guid("2fc63da3-b9ed-480d-9543-a31037599bbb"), new Guid("2330f6c1-3212-44fd-b231-c9ee4ffe196e"), "978-3-4534-3577-3", "It", new DateTime(2024, 8, 20, 13, 29, 41, 55, DateTimeKind.Local).AddTicks(3586), new DateTime(2024, 8, 20, 13, 29, 41, 55, DateTimeKind.Local).AddTicks(3617), "Stephen King’s terrifying, classic #1 New York Times bestseller." }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "Id",
                keyValue: new Guid("130fa159-70c1-42fb-8f31-5907f04b20e2"));

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "Id",
                keyValue: new Guid("2c463c56-21eb-4aae-8c2b-a87bcda80256"));

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "Id",
                keyValue: new Guid("4dccb0be-1de4-4659-80d2-0cf971a0d599"));

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "Id",
                keyValue: new Guid("bd304d0c-66b5-4873-bd22-c83f804ca7b7"));

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "Id",
                keyValue: new Guid("f86f763e-7f80-491d-ac29-ec93cf0048e0"));

            migrationBuilder.DeleteData(
                table: "Author",
                keyColumn: "Id",
                keyValue: new Guid("2fc63da3-b9ed-480d-9543-a31037599bbb"));

            migrationBuilder.DeleteData(
                table: "Author",
                keyColumn: "Id",
                keyValue: new Guid("944936e4-5167-41fe-8373-0540f319c3d3"));

            migrationBuilder.DeleteData(
                table: "Author",
                keyColumn: "Id",
                keyValue: new Guid("ac9417d1-a277-4974-b41c-25abde25bf38"));

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "Id",
                keyValue: new Guid("2330f6c1-3212-44fd-b231-c9ee4ffe196e"));

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "Id",
                keyValue: new Guid("27e0db66-79ef-448d-b20c-af2c1a769e6b"));

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "Id",
                keyValue: new Guid("3898242b-a685-4d49-bd06-8c7e34e14d7c"));
        }
    }
}
