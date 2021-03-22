using Microsoft.EntityFrameworkCore.Migrations;

namespace Api2.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 512, nullable: false),
                    Body = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "Body", "Title" },
                values: new object[] { 1001, "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Blandit libero volutpat sed cras ornare arcu dui vivamus arcu. Cras sed felis eget velit aliquet sagittis id. Phasellus vestibulum lorem sed risus. Varius morbi enim nunc faucibus. Ullamcorper morbi tincidunt ornare massa eget egestas purus viverra. Aliquet bibendum enim facilisis gravida neque convallis. Faucibus turpis in eu mi. Dui ut ornare lectus sit amet est. Pulvinar mattis nunc sed blandit libero. Quis imperdiet massa tincidunt nunc pulvinar sapien et ligula ullamcorper. Consequat interdum varius sit amet mattis. Tempus quam pellentesque nec nam aliquam sem. Interdum varius sit amet mattis vulputate enim nulla aliquet porttitor. Auctor augue mauris augue neque gravida in fermentum et. Cursus mattis molestie a iaculis at erat pellentesque.", "Blog title 1" });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "Body", "Title" },
                values: new object[] { 1002, "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Blandit libero volutpat sed cras ornare arcu dui vivamus arcu. Cras sed felis eget velit aliquet sagittis id. Phasellus vestibulum lorem sed risus. Varius morbi enim nunc faucibus. Ullamcorper morbi tincidunt ornare massa eget egestas purus viverra. Aliquet bibendum enim facilisis gravida neque convallis. Faucibus turpis in eu mi. Dui ut ornare lectus sit amet est. Pulvinar mattis nunc sed blandit libero. Quis imperdiet massa tincidunt nunc pulvinar sapien et ligula ullamcorper. Consequat interdum varius sit amet mattis. Tempus quam pellentesque nec nam aliquam sem. Interdum varius sit amet mattis vulputate enim nulla aliquet porttitor. Auctor augue mauris augue neque gravida in fermentum et. Cursus mattis molestie a iaculis at erat pellentesque.", "Blog title 2" });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "Body", "Title" },
                values: new object[] { 1003, "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Blandit libero volutpat sed cras ornare arcu dui vivamus arcu. Cras sed felis eget velit aliquet sagittis id. Phasellus vestibulum lorem sed risus. Varius morbi enim nunc faucibus. Ullamcorper morbi tincidunt ornare massa eget egestas purus viverra. Aliquet bibendum enim facilisis gravida neque convallis. Faucibus turpis in eu mi. Dui ut ornare lectus sit amet est. Pulvinar mattis nunc sed blandit libero. Quis imperdiet massa tincidunt nunc pulvinar sapien et ligula ullamcorper. Consequat interdum varius sit amet mattis. Tempus quam pellentesque nec nam aliquam sem. Interdum varius sit amet mattis vulputate enim nulla aliquet porttitor. Auctor augue mauris augue neque gravida in fermentum et. Cursus mattis molestie a iaculis at erat pellentesque.", "Blog title 3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blogs");
        }
    }
}
