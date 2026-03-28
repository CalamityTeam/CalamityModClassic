using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Calamitas
{
	public class BlightedEyeball : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Eye of Desolation");
			/* Tooltip.SetDefault("Tonight is going to be a horrific night...\n" +
                "Summons Calamitas\n" +
				"Not consumable"); */
		}
		
		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 18;
			Item.rare = 6;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = 4;
			Item.consumable = false;
		}
		
		public override bool CanUseItem(Player player)
		{
			return !Main.dayTime && !NPC.AnyNPCs(Mod.Find<ModNPC>("Calamitas").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("CalamitasRun3").Type);
		}
		
		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("Calamitas").Type);
			SoundEngine.PlaySound(SoundID.Roar, player.position);
			return true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HellstoneBar, 10);
            recipe.AddIngredient(null, "EssenceofChaos", 7);
            recipe.AddIngredient(ItemID.SoulofFright, 5);
			recipe.AddIngredient(null, "BlightedLens", 3);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}