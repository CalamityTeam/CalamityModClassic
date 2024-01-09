using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.AstrumDeus
{
	public class Starcore : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Starcore");
			//Tooltip.SetDefault("May the stars guide your way");
		}
		
		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 18;
			Item.maxStack = 20;
			Item.rare = ItemRarityID.Lime;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.consumable = true;
		}
		
		public override bool CanUseItem(Player player)
		{
			return !Main.dayTime && !NPC.AnyNPCs(Mod.Find<ModNPC>("AstrumDeusHead").Type);
		}
		
		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("AstrumDeusHead").Type);
			SoundEngine.PlaySound(SoundID.Roar, player.position);
			return true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "Stardust", 10);
			recipe.AddIngredient(ItemID.HallowedBar, 7);
			recipe.AddIngredient(null, "LivingShard", 2);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}