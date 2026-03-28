using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.AquaticScourge
{
	public class Seafood : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Seafood");
			/* Tooltip.SetDefault("The sulphuric sand stirs...\n" +
                "Summons the Aquatic Scourge"); */
		}
		
		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 28;
			Item.maxStack = 20;
			Item.rare = 5;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = 4;
			Item.consumable = true;
		}
		
		public override bool CanUseItem(Player player)
		{
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            return modPlayer.ZoneSulphur && !NPC.AnyNPCs(Mod.Find<ModNPC>("AquaticScourgeHead").Type);
		}
		
		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("AquaticScourgeHead").Type);
			SoundEngine.PlaySound(SoundID.Roar, player.position);
			return true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "SulphurousSand", 10);
            recipe.AddIngredient(ItemID.Starfish, 5);
            recipe.AddIngredient(ItemID.SharkFin, 3);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}