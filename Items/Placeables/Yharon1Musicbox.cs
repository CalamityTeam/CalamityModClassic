using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Placeables
{
    public class Yharon1Musicbox : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Music Box (Yharon, Pre-dark sun)");
        }

        public override void SetDefaults()
        {
            Item.useStyle = 1;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.createTile = Mod.Find<ModTile>("Yharon1Musicbox").Type;
            Item.width = 24;
            Item.height = 24;
            Item.rare = 4;
            Item.value = 100000;
            Item.accessory = true;
        }

	    public override void AddRecipes()
	    {
            Recipe recipe = CreateRecipe(1);
			recipe.AddIngredient(null, "DarksunFragment", 5);
			recipe.AddIngredient(null, "CosmiliteBar", 3);
			recipe.AddTile(null, "DraedonsForge");
			recipe.Register();
	    }
	}
}