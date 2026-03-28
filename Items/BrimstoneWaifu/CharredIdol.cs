using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.BrimstoneWaifu
{
	public class CharredIdol : ModItem
	{
		public override void SetStaticDefaults()
 		{
 			// DisplayName.SetDefault("Charred Idol");
 			/* Tooltip.SetDefault("Use in the Brimstone Crag at your own risk\n" +
                "Summons the Brimstone Elemental"); */
 		}
		
		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 18;
			Item.maxStack = 20;
			Item.rare = 6;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = 4;
			Item.consumable = true;
		}
		
		public override bool CanUseItem(Player player)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			return modPlayer.ZoneCalamity && !NPC.AnyNPCs(Mod.Find<ModNPC>("BrimstoneElemental").Type);
		}
		
		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("BrimstoneElemental").Type);
			SoundEngine.PlaySound(SoundID.Roar, player.position);
			return true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.SoulofNight, 5);
			recipe.AddIngredient(null, "EssenceofChaos", 5);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}