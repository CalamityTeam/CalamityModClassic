using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Crabulon
{
	public class DecapoditaSprout : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Decapodita Sprout");
			// Tooltip.SetDefault("Summons Crabulon");
		}
		
		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 18;
			Item.maxStack = 20;
			Item.rare = 2;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = 4;
			Item.consumable = true;
		}
		
		public override bool CanUseItem(Player player)
		{
			return player.ZoneGlowshroom && !NPC.AnyNPCs(Mod.Find<ModNPC>("CrabulonIdle").Type);
		}
		
		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
            if (Main.netMode != 1)
            {
                NPC.NewNPC(null,(int)(player.position.X + (float)(Main.rand.Next(-50, 51))), (int)(player.position.Y - 50f), Mod.Find<ModNPC>("CrabulonIdle").Type, 0, 0f, 0f, 0f, 0f, 255);
                SoundEngine.PlaySound(SoundID.Roar, player.position);
            }
			return true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.GlowingMushroom, 25);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}