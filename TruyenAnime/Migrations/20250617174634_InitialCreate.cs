using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TruyenAnime.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    SentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublishYear = table.Column<int>(type: "int", nullable: false),
                    Publisher = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsNewArrival = table.Column<bool>(type: "bit", nullable: false),
                    IsBestSeller = table.Column<bool>(type: "bit", nullable: false),
                    IsHotDeal = table.Column<bool>(type: "bit", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderPlaced = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    OrderDetailId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Shounen" },
                    { 2, "Shoujo" },
                    { 3, "Seinen" },
                    { 4, "Josei" },
                    { 5, "Kodomo" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Author", "CategoryId", "Description", "ImageUrl", "IsBestSeller", "IsHotDeal", "IsNewArrival", "Name", "Price", "PublishYear", "Publisher", "Size", "Stock" },
                values: new object[,]
                {
                    { 1, "Gege Akutami", 1, "Itadori Yuji là một học sinh cấp Ba sở hữu năng lực thể chất phi thường. Hằng ngày cậu thường tới bệnh viện chăm người ông đang ốm liệt giường. Nhưng một ngày nọ, phong ấn của “chú vật” vốn ngủ yên trong trường bị phá giải, quái vật xuất hiện. Để cứu hai anh chị khóa trên đang gặp nguy hiểm, Itadori đã xông vào trường và...", "https://product.hstatic.net/200000343865/product/chu-thuat-hoi-chien---tap-1_6d3f94c0ed3e4253baa722e759fe2eaa_master.jpg", false, false, false, "Chú thuật hồi chiến - Tập 1", 27000m, 2020, "NXB Kim Đồng", "13x18cm", 0 },
                    { 2, "Tomohito Oda", 2, "\"Chứng rối loạn giao tiếp xã hội là việc cảm thấy khó khăn khi tương tác với người khác.\nNó còn dùng để chỉ những người ít hoặc không có kĩ năng giao tiếp.\"\nNhững người mắc phải chứng này chỉ sợ nói chuyện, chứ kì thực họ có rất nhiều điều muốn nói. Vậy phải làm sao để họ có thể giao tiếp trong cuộc sống thường ngày...?", "https://product.hstatic.net/200000343865/product/1_202f1ff8a90946b5ba433792cc267064_07fb27d9a3094122b7818c8c94983408_master.jpg", false, false, false, "Komi - Nữ thần sợ giao tiếp - Tập 1", 22500m, 2021, "NXB Trẻ", "13x18cm", 0 },
                    { 3, "Makoto Shinkai", 3, "Giới thiệu sách: TOP 100 BEST SELLER - 5cm/s không chỉ là vận tốc của những cánh anh đào rơi, mà còn là vận tốc khi chúng ta lặng lẽ bước qua đời nhau, đánh mất bao cảm xúc thiết tha nhất của tình yêu.", "https://product.hstatic.net/200000287623/product/5cm_5f3b4ebe155b4d6491c244b8623657b5_master.jpg", false, false, false, "5 Centimet Trên Giây", 48750m, 2019, "NXB Văn Học", "14.5x20.5cm", 0 },
                    { 4, "Tomoko Ninomiya", 3, "Chiaki Shinichi là một sinh viên tài năng của nhạc viện nhưng chẳng có cơ hội phô diễn hết năng lực. Trong quá khứ cậu từng gặp ám ảnh tâm lý nên đã định từ bỏ ước mơ làm nhạc trưởng.\nNgày nọ, nghe thấy tiếng nhạc tuyệt vời bên tai, Chiaki tỉnh giấc và trông thấy cô nàng Nodame đang chơi piano giữa đống rác chất chồng…!? Cô là thiên tài hay kẻ kì quặc? Đó là khúc nhạc định mệnh đầu tiên dẫn dắt Chiaki đến với thứ âm nhạc mới mà cậu sẽ tạo nên cùng những người bạn kì lạ tại trường nhạc!", "https://product.hstatic.net/200000287623/product/nodame_cantabile_1_manga_bia_1_400eb59146564bbf9b10212b69f0e37a_master.png", false, false, false, "Nodame Cantabile - Khúc Ngẫu Hứng Của Nodame - 1", 101200m, 2022, "NXB Nhạc Việt", "14x21cm", 0 },
                    { 5, "Akira Toriyama", 4, "Mặt trời lại mọc trên ngôi làng Penguin ~\nMảnh đất của những chuyên gia tấu hài!!\nNơi mà ai cũng điên khùng theo một cách riêng...\nĐừng cố lí giải vì mọi thứ ở đây đều không theo một quy luật nào!", "https://product.hstatic.net/200000343865/product/1_69cfc243af404c3d8270011f76a44164_master.jpg", false, false, false, "Dr.SLUMP Ultimate Edition - Tập 1", 54000m, 2023, "NXB Kim Đồng", "13x18cm", 0 },
                    { 6, "Akumi Agitogi", 3, "Nguyện ước… chỉ là niềm “hạnh phúc” nhỏ nhoi…\nSaimori Miyo sinh ra trong một gia đình sở hữu siêu năng nhưng lại không được thừa hưởng sức mạnh ấy.\nĐứa em gái cùng cha khác mẹ đã thức tỉnh năng lực của Miyo đối xử với cô như kẻ hầu người hạ, cha cũng chẳng yêu thương. Miyo không khác gì một đứa con gái không ai cần đến.\nCậu bạn thanh mai trúc mã cũng là người duy nhất luôn đứng về phía Miyo lại sẽ kết hôn với cô em gái cùng cha khác mẹ kia để kế nghiệp nhà vợ...\nMiyo trở thành kẻ ngáng đường và bị đem gả vào gia tộc Kudou, cho một kẻ nổi danh lạnh lùng tàn nhẫn…", "https://product.hstatic.net/200000343865/product/1--manga_46fb6583acf548a09c5357f04cfeca81_71bbec2087ef48378f6e991b6fca9cb9_master.jpg", false, false, false, "Hôn nhân hạnh phúc của tôi - Tập 1", 45000m, 2024, "NXB Phụ Nữ", "14x20cm", 0 },
                    { 7, "Kouji Miura", 1, "Inomata Taiki, thành viên câu lạc bộ cầu lông, mê chị Kano Chinatsu bên câu lạc bộ bóng rổ nữ như điếu đổ. Cậu bị thu hút bởi hình ảnh Chinatsu luôn chăm chỉ luyện tập một mình. Ngày nọ, một tình huống bất ngờ đã xảy ra. Quãng đường trước mắt còn xa xôi, song chí hướng của cả hai lại gần không ngờ tới. Từ đó, câu chuyện tình yêu tuổi thanh xuân ngọt ngào bắt đầu chớm nở.", "https://product.hstatic.net/200000343865/product/bluebox_tap-8_bia_f39eaafbe870416f974a189487ed4f97_master.jpg", false, false, false, "Blue Box - Tập 8", 36000m, 2024, "NXB Trẻ", "13x18cm", 0 },
                    { 8, "Hiromu Arakawa", 1, "Trong thế giới của những giả kim thuật sư, có một tồn tại đã đi vào huyền thoại và trở thành ước mơ bất cứ ai cũng ao khát - đó chính là \"Hòn đá Triết gia\".", "https://product.hstatic.net/200000343865/product/1_e43e42b5db6d4d41a9ba1ee351fc2120.jpg", false, false, false, "Fullmetal Alchemist - Cang giả kim thuật sư", 22000m, 2020, "NXB Kim Đồng", "13x18cm", 0 }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Content", "CreatedAt", "Email", "ProductId", "Rating", "UserId", "Username" },
                values: new object[,]
                {
                    { 1, "Truyện hay lắm, nét vẽ đẹp, cốt truyện cuốn hút.", new DateTime(2025, 6, 17, 15, 0, 0, 0, DateTimeKind.Unspecified), "minh@example.com", 1, 5, null, "Minh" },
                    { 2, "Rất ý nghĩa, nhân vật chính dễ thương.", new DateTime(2025, 6, 17, 15, 1, 0, 0, DateTimeKind.Unspecified), "linh@example.com", 2, 4, null, "Linh" },
                    { 3, "Nội dung ổn nhưng in hơi mờ.", new DateTime(2025, 6, 17, 15, 2, 0, 0, DateTimeKind.Unspecified), "hai@example.com", 1, 3, null, "Hải" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ProductId",
                table: "Reviews",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactMessages");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
