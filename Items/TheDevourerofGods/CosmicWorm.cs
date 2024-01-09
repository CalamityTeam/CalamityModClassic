using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point0.Items.TheDevourerofGods
{
	public class CosmicWorm : ModItem
	{
		public override void SetDefaults()
		{
			//DisplayName.SetDefault("Cosmic Worm");
			Item.width = 28;
			Item.height = 18;
			Item.maxStack = 20;
			//AddTooltip("Summons the devourer of the cosmos");
			Item.rare = 9;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = 4;
			Item.UseSound = SoundID.Item44;
			Item.consumable = false;
		}

		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			if(!Main.dayTime)
			{
				Item.consumable = true;
				SoundEngine.PlaySound(SoundID.Roar, player.position);
				if (Main.netMode != 1)
				{
					NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("DevourerofGodsHead").Type);
				}
				return true;
			}
			return true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.LunarBar, 5);
			recipe.AddIngredient(ItemID.FragmentNebula, 3);
			recipe.AddIngredient(ItemID.FragmentStardust, 3);
			recipe.AddIngredient(ItemID.FragmentSolar, 3);
			recipe.AddIngredient(ItemID.FragmentVortex, 3);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register();
		}
	}
}