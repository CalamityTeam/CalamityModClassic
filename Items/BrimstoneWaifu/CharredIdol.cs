using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.BrimstoneWaifu
{
	public class CharredIdol : ModItem
	{
		public override void SetStaticDefaults()
 		{
 			//DisplayName.SetDefault("Charred Idol");
 			//Tooltip.SetDefault("Use in the Calamity biome at your own risk");
 		}
		
		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 18;
			Item.maxStack = 20;
			Item.rare = ItemRarityID.LightPurple;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.consumable = true;
		}
		
		public override bool CanUseItem(Player player)
		{
			CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
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
			recipe.AddIngredient(ItemID.HallowedBar, 5);
			recipe.AddIngredient(ItemID.SoulofNight, 5);
			recipe.AddIngredient(null, "EssenceofChaos", 5);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}