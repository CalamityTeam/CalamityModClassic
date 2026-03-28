using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.DesertScourge
{
	public class DriedSeafood : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Desert Medallion");
			/* Tooltip.SetDefault("The desert sand stirs...\n" +
                "Summons the Desert Scourge"); */
		}
		
		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 28;
			Item.maxStack = 20;
			Item.rare = 2;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = 4;
			Item.consumable = true;
		}
		
		public override bool CanUseItem(Player player)
		{
			return player.ZoneDesert && !NPC.AnyNPCs(Mod.Find<ModNPC>("DesertScourgeHead").Type);
		}
		
		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("DesertScourgeHead").Type);
			if (CalamityWorldPreTrailer.revenge)
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
			recipe.AddIngredient(ItemID.SandBlock, 15);
			recipe.AddIngredient(ItemID.AntlionMandible, 3);
			recipe.AddIngredient(ItemID.Cactus, 10);
			recipe.AddIngredient(null, "StormlionMandible");
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}