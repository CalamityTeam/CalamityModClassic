using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.AstrumDeus
{
	public class Starcore : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Starcore");
			/* Tooltip.SetDefault("May the stars guide your way\n" +
                "Summons Astrum Deus\n" +
				"Not consumable"); */
		}
		
		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 18;
			Item.rare = 7;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = 4;
			Item.consumable = false;
		}
		
		public override bool CanUseItem(Player player)
		{
			return !Main.dayTime && !NPC.AnyNPCs(Mod.Find<ModNPC>("AstrumDeusHead").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("AstrumDeusHeadSpectral").Type);
		}

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            for (int x = 0; x < 10; x++)
            {
                NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("AstrumDeusHead").Type);
            }
            NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("AstrumDeusHeadSpectral").Type);
            SoundEngine.PlaySound(SoundID.Roar, player.position);
			return true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "Stardust", 25);
            recipe.AddIngredient(null, "AstralJelly", 8);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}