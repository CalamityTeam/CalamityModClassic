using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Astrageldon
{
	public class AstralChunk : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Astral Chunk");
            // Tooltip.SetDefault("Summons Astrum Aureus");
        }
		
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 20;
			Item.rare = 7;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = 4;
			Item.consumable = true;
		}
		
		public override bool CanUseItem(Player player)
		{
			return !Main.dayTime && !NPC.AnyNPCs(Mod.Find<ModNPC>("Astrageldon").Type);
		}
		
		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
            if (Main.netMode != 1)
            {
                int num = NPC.NewNPC(null, (int)(player.position.X + (float)(Main.rand.Next(-50, 50))), (int)(player.position.Y - 150f), Mod.Find<ModNPC>("Astrageldon").Type, 0, 0f, 0f, 0f, 0f, 255);
                SoundEngine.PlaySound(SoundID.Roar, player.position);
            }
			return true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "Stardust", 15);
			recipe.AddIngredient(ItemID.FallenStar, 10);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}