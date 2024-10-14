using Teppi.Share.Entities;

namespace Teppi.Data.Persistence;

    public static class DbInitializer
    {
        public static void Initialize(TeppiDbContext context)
        {

            if (context.Tags.Any() && context.Categories.Any())
            {
                return; // DB has been seeded
            }

            var categories = new Category[]
            {
                new Category
                {
                    Name = "Interior design",
                    ImageUrl = "https://images.unsplash.com/photo-1586023492125-27b2c045efd7"
                },
                new Category
                {
                    Name = "Traditional art",
                    ImageUrl = "https://images.unsplash.com/photo-1616855997498-e0ad5f7721db"
                },
                new Category
                {
                    Name = "3D Animation",
                    ImageUrl = "https://images.unsplash.com/photo-1627163439134-7a8c47e08208"
                },
                new Category
                {
                    Name = "Marketing",
                    ImageUrl = "https://images.unsplash.com/photo-1533750349088-cd871a92f312"
                },
                new Category
                {
                    Name = "Photography",
                    ImageUrl = "https://images.unsplash.com/photo-1471341971476-ae15ff5dd4ea"
                },
                new Category
                {
                    Name = "Calligraphy & lettering",
                    ImageUrl = "https://images.unsplash.com/photo-1584473457431-cf7e9d172714"
                },
                new Category
                {
                    Name = "UX design",
                    ImageUrl = "https://images.unsplash.com/photo-1545235617-9465d2a55698"
                },
                new Category
                {
                    Name = "Web develop",
                    ImageUrl = "https://images.unsplash.com/photo-1579403124614-197f69d8187b"
                }
            };
            
            context.Categories.AddRange(categories);

            var tags = new Tag[]
            {
                new Tag {Name = "New"},
                new Tag {Name = "Trending"},
                new Tag {Name = "Best seller"},
                new Tag {Name = "Top rated"},
                new Tag {Name = "Free"},
                new Tag {Name = "Discount"}
                
            };
            context.Tags.AddRange(tags);
            context.SaveChanges();
        }
    }