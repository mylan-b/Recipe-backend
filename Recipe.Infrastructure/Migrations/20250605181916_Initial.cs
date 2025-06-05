using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Recipe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recipe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ingredients = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Steps = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CookingTimeMinutes = table.Column<int>(type: "int", nullable: false),
                    DietaryTag = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Recipe",
                columns: new[] { "Id", "CookingTimeMinutes", "DietaryTag", "Ingredients", "Steps", "Title" },
                values: new object[,]
                {
                    { 1, 30, "gluten,meat", "Spaghetti, Tomato Sauce, Beef", "Boil pasta, cook sauce, mix.", "Spaghetti Bolognese" },
                    { 2, 20, "vegan,gluten-free", "Tofu, Broccoli, Soy Sauce", "Fry tofu, add veg, stir-fry.", "Vegan Tofu Stir Fry" },
                    { 3, 10, "vegetarian", "Bread, Avocado, Salt, Pepper", "Toast bread, spread avocado.", "Avocado Toast" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recipe");
        }
    }
}
