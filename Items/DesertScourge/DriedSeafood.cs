using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point0.Items.DesertScourge
{
	public class DriedSeafood : ModItem
	{
		public override void SetDefaults()
		{
			//DisplayName.SetDefault("Desert Medallion");
			Item.width = 28;
			Item.height = 28;
			Item.maxStack = 20;
			//AddTooltip("The desert sand stirs...");
			Item.rare = 2;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = 4;
			Item.UseSound = SoundID.Item44;
			Item.consumable = false;
		}

		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			if (player.ZoneDesert)
			{
				Item.consumable = true;
				SoundEngine.PlaySound(SoundID.Roar, player.position);
				if (Main.netMode != 1)
				{
					NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("DesertScourgeHead").Type);
				}
				return true;
			}
			return true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.SandBlock, 15);
			recipe.AddIngredient(ItemID.AntlionMandible);
			recipe.AddIngredient(ItemID.Cactus, 5);
			recipe.AddIngredient(null, "DesertFeather");
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}