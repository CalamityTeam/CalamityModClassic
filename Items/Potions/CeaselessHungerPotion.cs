using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Potions
{
	public class CeaselessHungerPotion : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ceaseless Hunger Potion");
			// Tooltip.SetDefault("Causes you to suck up all items in the world");
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
			Item.buffType = Mod.Find<ModBuff>("CeaselessHunger").Type;
			Item.buffTime = 600;
			Item.value = Item.buyPrice(0, 2, 0, 0);
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(4);
			recipe.AddIngredient(ItemID.BottledWater, 4);
			recipe.AddIngredient(null, "DarkPlasma");
			recipe.AddIngredient(null, "GalacticaSingularity");
			recipe.AddTile(TileID.AlchemyTable);
			recipe.Register();
			recipe = CreateRecipe(4);
			recipe.AddIngredient(ItemID.BottledWater, 4);
			recipe.AddIngredient(null, "BloodOrb", 20);
			recipe.AddIngredient(null, "DarkPlasma");
			recipe.AddTile(TileID.AlchemyTable);
			recipe.Register();
		}
	}
}