using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.Crabulon
{
	public class DecapoditaSprout : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Decapodita Sprout");
			//Tooltip.SetDefault("Summons the giant mushroom crab");
		}
		
		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 18;
			Item.maxStack = 20;
			Item.rare = ItemRarityID.Green;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.consumable = true;
		}
		
		public override bool CanUseItem(Player player)
		{
			return player.ZoneGlowshroom && !NPC.AnyNPCs(Mod.Find<ModNPC>("CrabulonIdle").Type);
		}
		
		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			int num = NPC.NewNPC(player.GetSource_FromThis(), (int)(player.position.X + (float)(Main.rand.Next(-50, 51))), (int)(player.position.Y - 50f), Mod.Find<ModNPC>("CrabulonIdle").Type, 0, 0f, 0f, 0f, 0f, 255);
			if (Main.netMode == NetmodeID.Server && num < 200)
			{
				NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, num, 0f, 0f, 0f, 0, 0, 0);
			}
			SoundEngine.PlaySound(SoundID.Roar, player.position);
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