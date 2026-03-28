using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.FabsolStuff
{
	public class FabsolsVodka : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Fabsol's Vodka");
			/* Tooltip.SetDefault("Boosts all damage stats by 8% but lowers defense by 20\n" +
			                   "Increases immune time after being struck"); */
		}
		
		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 18;
			Item.useTurn = true;
			Item.maxStack = 30;
			Item.rare = 3;
			Item.useAnimation = 17;
			Item.useTime = 17;
			Item.useStyle = 2;
			Item.UseSound = SoundID.Item3;
			Item.consumable = true;
			Item.buffType = Mod.Find<ModBuff>("FabsolVodka").Type;
			Item.buffTime = 54000;
            Item.value = Item.buyPrice(0, 10, 0, 0);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Ale);
            recipe.AddIngredient(ItemID.PixieDust, 10);
            recipe.AddIngredient(ItemID.CrystalShard, 5);
            recipe.AddIngredient(ItemID.UnicornHorn);
            recipe.AddTile(TileID.Kegs);
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(null, "BloodOrb", 40);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddTile(TileID.AlchemyTable);
            recipe.Register();
        }
	}
}