using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.DesertScourge
{
	public class DriedSeafood : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Desert Medallion");
			//Tooltip.SetDefault("The desert sand stirs...");
		}
		
		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 28;
			Item.maxStack = 20;
			Item.rare = ItemRarityID.Green;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.consumable = true;
		}
		
		public override bool CanUseItem(Player player)
		{
			return player.ZoneDesert && !NPC.AnyNPCs(Mod.Find<ModNPC>("DesertScourgeHead").Type);
		}
		
		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("DesertScourgeHead").Type);
			if (CalamityWorld1Point2.revenge)
			{
				NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("DesertScourgeHeadSmall").Type);
				NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("DesertScourgeHeadSmall").Type);
			}
			SoundEngine.PlaySound(SoundID.Roar, player.position);
			return true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.SandBlock, 25);
			recipe.AddIngredient(ItemID.AntlionMandible, 3);
			recipe.AddIngredient(ItemID.Cactus, 15);
			recipe.AddIngredient(null, "StormlionMandible");
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}