using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.Calamitas
{
	public class BlightedEyeball : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Eye of Desolation");
			//Tooltip.SetDefault("Tonight is going to be a horrific night...");
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
			recipe.AddIngredient(ItemID.HallowedBar, 5);
			recipe.AddIngredient(ItemID.SoulofSight, 5);
			recipe.AddIngredient(ItemID.SoulofFright, 5);
			recipe.AddIngredient(null, "BlightedLens", 3);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}