using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace CalamityModClassicPreTrailer.Items.Potions
{
	public class AstralInjection : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Astral Injection");
			// Tooltip.SetDefault("Gives mana sickness and hurts you when used, but you regenerate mana extremely quickly even while moving or casting spells");
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
			Item.buffType = Mod.Find<ModBuff>("AstralInjectionBuff").Type;
			Item.buffTime = 180;
			Item.value = Item.buyPrice(0, 2, 0, 0);
		}

		public override void OnConsumeItem(Player player)
		{
			player.AddBuff(BuffID.ManaSickness, Player.manaSickTime / 2, true);
			player.statLife -= 5;
			if (Main.myPlayer == player.whoAmI)
			{
				player.HealEffect(-5, true);
			}
			if (player.statLife <= 0)
			{
				player.KillMe(PlayerDeathReason.ByOther(10), 1000.0, 0, false);
			}
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(4);
			recipe.AddIngredient(ItemID.BottledWater, 4);
			recipe.AddIngredient(null, "Stardust", 4);
			recipe.AddIngredient(null, "AstralJelly");
			recipe.AddTile(TileID.AlchemyTable);
			recipe.Register();
			recipe = CreateRecipe(4);
			recipe.AddIngredient(ItemID.BottledWater, 4);
			recipe.AddIngredient(null, "BloodOrb", 5);
			recipe.AddIngredient(null, "AstralJelly");
			recipe.AddTile(TileID.AlchemyTable);
			recipe.Register();
		}
	}
}