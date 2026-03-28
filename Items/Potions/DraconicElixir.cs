using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Potions
{
	public class DraconicElixir : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Draconic Elixir");
			/* Tooltip.SetDefault("Greatly increases wing flight time and speed and increases defense by 16\n" +
				"God slayer revival heals you to full HP instead of 150 HP when triggered\n" +
				"Silva invincibility heals you to full HP when triggered\n" +
				"If you trigger the above heals you cannot drink this potion again for 30 seconds"); */
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
			Item.buffType = Mod.Find<ModBuff>("DraconicSurgeBuff").Type;
			Item.buffTime = 18000;
			Item.value = Item.buyPrice(0, 2, 0, 0);
		}

		public override bool CanUseItem(Player player)
		{
			return player.GetModPlayer<CalamityPlayerPreTrailer>().draconicSurgeCooldown == 0;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.BottledWater);
			recipe.AddIngredient(null, "HellcasterFragment");
			recipe.AddIngredient(ItemID.Daybloom);
			recipe.AddIngredient(ItemID.Moonglow);
			recipe.AddIngredient(ItemID.Fireblossom);
			recipe.AddTile(TileID.AlchemyTable);
			recipe.Register();
			recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.BottledWater);
			recipe.AddIngredient(null, "BloodOrb", 50);
			recipe.AddIngredient(null, "HellcasterFragment");
			recipe.AddTile(TileID.AlchemyTable);
			recipe.Register();
		}
	}
}