using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.Scavenger
{
	public class AncientMedallion : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Ancient Medallion");
			//Tooltip.SetDefault("A very old temple medallion");
		}
		
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 20;
			Item.rare = ItemRarityID.Cyan;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.consumable = true;
		}
		
		public override bool CanUseItem(Player player)
		{
			return !NPC.AnyNPCs(Mod.Find<ModNPC>("ScavengerBody").Type) && player.ZoneOverworldHeight;
		}
		
		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			int num = NPC.NewNPC(player.GetSource_FromThis(), (int)(player.position.X + (float)(Main.rand.Next(-100, 101))), (int)(player.position.Y - 250f), Mod.Find<ModNPC>("ScavengerBody").Type, 0, 0f, 0f, 0f, 0f, 255);
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
			recipe.AddIngredient(ItemID.LunarTabletFragment, 5);
			recipe.AddIngredient(ItemID.LihzahrdBrick, 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}