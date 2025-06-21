using Microsoft.EntityFrameworkCore;
using System.Collections.Generic; 
using System.Linq; 
using TruyenAnime.Models;

namespace TruyenAnime.Data
{
    public class TruyenAnimeDbContext : DbContext
    {
        public TruyenAnimeDbContext(DbContextOptions<TruyenAnimeDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Banner> Banners { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderDetail>()
                .HasKey(od => new { od.OrderId, od.ProductId });

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Order>()
                .Property(o => o.OrderTotal)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany()
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderDetail>()
                .Property(od => od.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.ProductId);

            // Seed Categories
            var categories = new Category[]
            {
                new Category { Id = 1, Name = "Shounen" }, 
                new Category { Id = 2, Name = "Shoujo" },
                new Category { Id = 3, Name = "Seinen" },
                new Category { Id = 4, Name = "Josei" },
                new Category { Id = 5, Name = "Kodomo" }
            };
            modelBuilder.Entity<Category>().HasData(categories);

            // Seed Products
            var products = new Product[]
            {
                new Product { 
                    Id = 1, Name = "Chú thuật hồi chiến - Tập 1", 
                    Price = 27000, 
                    ImageUrl = "https://product.hstatic.net/200000343865/product/chu-thuat-hoi-chien---tap-1_6d3f94c0ed3e4253baa722e759fe2eaa_master.jpg", 
                    CategoryId = 1, 
                    Description="Itadori Yuji là một học sinh cấp Ba sở hữu năng lực thể chất phi thường. Hằng ngày cậu thường tới bệnh viện chăm người ông đang ốm liệt giường. Nhưng một ngày nọ, phong ấn của “chú vật” vốn ngủ yên trong trường bị phá giải, quái vật xuất hiện. Để cứu hai anh chị khóa trên đang gặp nguy hiểm, Itadori đã xông vào trường và...",
                    Publisher = "NXB Kim Đồng",
                    PublishYear = 2020,
                    Author = "Gege Akutami",
                    Size = "13x18cm"
                },
                new Product { 
                    Id = 2, 
                    Name = "Komi - Nữ thần sợ giao tiếp - Tập 1", 
                    Price = 22500, 
                    ImageUrl = "https://product.hstatic.net/200000343865/product/1_202f1ff8a90946b5ba433792cc267064_07fb27d9a3094122b7818c8c94983408_master.jpg", 
                    CategoryId = 2, 
                    Description="\"Chứng rối loạn giao tiếp xã hội là việc cảm thấy khó khăn khi tương tác với người khác.\nNó còn dùng để chỉ những người ít hoặc không có kĩ năng giao tiếp.\"\nNhững người mắc phải chứng này chỉ sợ nói chuyện, chứ kì thực họ có rất nhiều điều muốn nói. Vậy phải làm sao để họ có thể giao tiếp trong cuộc sống thường ngày...?",
                    Publisher = "NXB Trẻ",
                    PublishYear = 2021,
                    Author = "Tomohito Oda",
                    Size = "13x18cm"
                },
                new Product { 
                    Id = 3, 
                    Name = "5 Centimet Trên Giây", 
                    Price = 48750, 
                    ImageUrl = "https://product.hstatic.net/200000287623/product/5cm_5f3b4ebe155b4d6491c244b8623657b5_master.jpg", 
                    CategoryId = 3, 
                    Description="Giới thiệu sách: TOP 100 BEST SELLER - 5cm/s không chỉ là vận tốc của những cánh anh đào rơi, mà còn là vận tốc khi chúng ta lặng lẽ bước qua đời nhau, đánh mất bao cảm xúc thiết tha nhất của tình yêu.",
                    Publisher = "NXB Văn Học",
                    PublishYear = 2019,
                    Author = "Makoto Shinkai",
                    Size = "14.5x20.5cm"
                },
                new Product { 
                    Id = 4, 
                    Name = "Nodame Cantabile - Khúc Ngẫu Hứng Của Nodame - 1", 
                    Price = 101200, 
                    ImageUrl = "https://product.hstatic.net/200000287623/product/nodame_cantabile_1_manga_bia_1_400eb59146564bbf9b10212b69f0e37a_master.png", 
                    CategoryId = 3, 
                    Description="Chiaki Shinichi là một sinh viên tài năng của nhạc viện nhưng chẳng có cơ hội phô diễn hết năng lực. Trong quá khứ cậu từng gặp ám ảnh tâm lý nên đã định từ bỏ ước mơ làm nhạc trưởng.\nNgày nọ, nghe thấy tiếng nhạc tuyệt vời bên tai, Chiaki tỉnh giấc và trông thấy cô nàng Nodame đang chơi piano giữa đống rác chất chồng…!? Cô là thiên tài hay kẻ kì quặc? Đó là khúc nhạc định mệnh đầu tiên dẫn dắt Chiaki đến với thứ âm nhạc mới mà cậu sẽ tạo nên cùng những người bạn kì lạ tại trường nhạc!",
                    Publisher = "NXB Nhạc Việt",
                    PublishYear = 2022,
                    Author = "Tomoko Ninomiya",
                    Size = "14x21cm"
                },
                new Product { 
                    Id = 5, 
                    Name = "Dr.SLUMP Ultimate Edition - Tập 1", 
                    Price = 54000, 
                    ImageUrl = "https://product.hstatic.net/200000343865/product/1_69cfc243af404c3d8270011f76a44164_master.jpg", 
                    CategoryId = 4, 
                    Description="Mặt trời lại mọc trên ngôi làng Penguin ~\nMảnh đất của những chuyên gia tấu hài!!\nNơi mà ai cũng điên khùng theo một cách riêng...\nĐừng cố lí giải vì mọi thứ ở đây đều không theo một quy luật nào!",
                    Publisher = "NXB Kim Đồng",
                    PublishYear = 2023,
                    Author = "Akira Toriyama",
                    Size = "13x18cm"
                },
                new Product { 
                    Id = 6, 
                    Name = "Hôn nhân hạnh phúc của tôi - Tập 1", 
                    Price = 45000, 
                    ImageUrl = "https://product.hstatic.net/200000343865/product/1--manga_46fb6583acf548a09c5357f04cfeca81_71bbec2087ef48378f6e991b6fca9cb9_master.jpg", 
                    CategoryId = 3, 
                    Description="Nguyện ước… chỉ là niềm “hạnh phúc” nhỏ nhoi…\nSaimori Miyo sinh ra trong một gia đình sở hữu siêu năng nhưng lại không được thừa hưởng sức mạnh ấy.\nĐứa em gái cùng cha khác mẹ đã thức tỉnh năng lực của Miyo đối xử với cô như kẻ hầu người hạ, cha cũng chẳng yêu thương. Miyo không khác gì một đứa con gái không ai cần đến.\nCậu bạn thanh mai trúc mã cũng là người duy nhất luôn đứng về phía Miyo lại sẽ kết hôn với cô em gái cùng cha khác mẹ kia để kế nghiệp nhà vợ...\nMiyo trở thành kẻ ngáng đường và bị đem gả vào gia tộc Kudou, cho một kẻ nổi danh lạnh lùng tàn nhẫn…",
                    Publisher = "NXB Phụ Nữ",
                    PublishYear = 2024,
                    Author = "Akumi Agitogi",
                    Size = "14x20cm"
                },
                new Product { 
                    Id = 7, 
                    Name = "Blue Box - Tập 8", 
                    Price = 36000, 
                    ImageUrl = "https://product.hstatic.net/200000343865/product/bluebox_tap-8_bia_f39eaafbe870416f974a189487ed4f97_master.jpg", 
                    CategoryId = 1, 
                    Description="Inomata Taiki, thành viên câu lạc bộ cầu lông, mê chị Kano Chinatsu bên câu lạc bộ bóng rổ nữ như điếu đổ. Cậu bị thu hút bởi hình ảnh Chinatsu luôn chăm chỉ luyện tập một mình. Ngày nọ, một tình huống bất ngờ đã xảy ra. Quãng đường trước mắt còn xa xôi, song chí hướng của cả hai lại gần không ngờ tới. Từ đó, câu chuyện tình yêu tuổi thanh xuân ngọt ngào bắt đầu chớm nở.",
                    Publisher = "NXB Trẻ",
                    PublishYear = 2024,
                    Author = "Kouji Miura",
                    Size = "13x18cm"
                },
                new Product { 
                    Id = 8, 
                    Name = "Fullmetal Alchemist - Cang giả kim thuật sư", 
                    Price = 22000, 
                    ImageUrl = "https://product.hstatic.net/200000343865/product/1_e43e42b5db6d4d41a9ba1ee351fc2120.jpg", 
                    CategoryId = 1, 
                    Description="Trong thế giới của những giả kim thuật sư, có một tồn tại đã đi vào huyền thoại và trở thành ước mơ bất cứ ai cũng ao khát - đó chính là \"Hòn đá Triết gia\".",
                    Publisher = "NXB Kim Đồng",
                    PublishYear = 2020,
                    Author = "Hiromu Arakawa",
                    Size = "13x18cm"
                }
            };
            modelBuilder.Entity<Product>().HasData(products);

            // Seed Reviews
            var reviews = new Review[]
            {
                new Review
                {
                    Id = 1,
                    ProductId = 1,
                    Username = "Minh",
                    Email = "minh@example.com",
                    Rating = 5,
                    Content = "Truyện hay lắm, nét vẽ đẹp, cốt truyện cuốn hút.",
                    CreatedAt = new DateTime(2025, 6, 17, 15, 0, 0)
                },
                new Review
                {
                    Id = 2,
                    ProductId = 2,
                    Username = "Linh",
                    Email = "linh@example.com",
                    Rating = 4,
                    Content = "Rất ý nghĩa, nhân vật chính dễ thương.",
                    CreatedAt = new DateTime(2025, 6, 17, 15, 1, 0)
                },
                new Review
                {
                    Id = 3,
                    ProductId = 1,
                    Username = "Hải",
                    Email = "hai@example.com",
                    Rating = 3,
                    Content = "Nội dung ổn nhưng in hơi mờ.",
                    CreatedAt = new DateTime(2025, 6, 17, 15, 2, 0)
                }
            };
            modelBuilder.Entity<Review>().HasData(reviews);


        }
    }
}