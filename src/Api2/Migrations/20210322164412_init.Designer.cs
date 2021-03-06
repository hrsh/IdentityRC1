// <auto-generated />
using Api2.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Api2.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210322164412_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.4");

            modelBuilder.Entity("Api2.Entities.Blog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Blogs");

                    b.HasData(
                        new
                        {
                            Id = 1001,
                            Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Blandit libero volutpat sed cras ornare arcu dui vivamus arcu. Cras sed felis eget velit aliquet sagittis id. Phasellus vestibulum lorem sed risus. Varius morbi enim nunc faucibus. Ullamcorper morbi tincidunt ornare massa eget egestas purus viverra. Aliquet bibendum enim facilisis gravida neque convallis. Faucibus turpis in eu mi. Dui ut ornare lectus sit amet est. Pulvinar mattis nunc sed blandit libero. Quis imperdiet massa tincidunt nunc pulvinar sapien et ligula ullamcorper. Consequat interdum varius sit amet mattis. Tempus quam pellentesque nec nam aliquam sem. Interdum varius sit amet mattis vulputate enim nulla aliquet porttitor. Auctor augue mauris augue neque gravida in fermentum et. Cursus mattis molestie a iaculis at erat pellentesque.",
                            Title = "Blog title 1"
                        },
                        new
                        {
                            Id = 1002,
                            Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Blandit libero volutpat sed cras ornare arcu dui vivamus arcu. Cras sed felis eget velit aliquet sagittis id. Phasellus vestibulum lorem sed risus. Varius morbi enim nunc faucibus. Ullamcorper morbi tincidunt ornare massa eget egestas purus viverra. Aliquet bibendum enim facilisis gravida neque convallis. Faucibus turpis in eu mi. Dui ut ornare lectus sit amet est. Pulvinar mattis nunc sed blandit libero. Quis imperdiet massa tincidunt nunc pulvinar sapien et ligula ullamcorper. Consequat interdum varius sit amet mattis. Tempus quam pellentesque nec nam aliquam sem. Interdum varius sit amet mattis vulputate enim nulla aliquet porttitor. Auctor augue mauris augue neque gravida in fermentum et. Cursus mattis molestie a iaculis at erat pellentesque.",
                            Title = "Blog title 2"
                        },
                        new
                        {
                            Id = 1003,
                            Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Blandit libero volutpat sed cras ornare arcu dui vivamus arcu. Cras sed felis eget velit aliquet sagittis id. Phasellus vestibulum lorem sed risus. Varius morbi enim nunc faucibus. Ullamcorper morbi tincidunt ornare massa eget egestas purus viverra. Aliquet bibendum enim facilisis gravida neque convallis. Faucibus turpis in eu mi. Dui ut ornare lectus sit amet est. Pulvinar mattis nunc sed blandit libero. Quis imperdiet massa tincidunt nunc pulvinar sapien et ligula ullamcorper. Consequat interdum varius sit amet mattis. Tempus quam pellentesque nec nam aliquam sem. Interdum varius sit amet mattis vulputate enim nulla aliquet porttitor. Auctor augue mauris augue neque gravida in fermentum et. Cursus mattis molestie a iaculis at erat pellentesque.",
                            Title = "Blog title 3"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
