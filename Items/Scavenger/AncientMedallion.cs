using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Scavenger
{
	public class AncientMedallion : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ancient Medallion");
			/* Tooltip.SetDefault("A very old temple medallion\n" +
                "Summons the Ravager"); */
		}
		
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 20;
			Item.rare = 8;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = 4;
			Item.consumable = true;
		}
		
		public override bool CanUseItem(Player player)
		{
			return !NPC.AnyNPCs(Mod.Find<ModNPC>("ScavengerBody").Type) && player.ZoneOverworldHeight;
		}
		
		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
            if (Main.netMode != 1)
            {
                NPC.NewNPC(Entity.GetSource_FromThis(null),(int)(player.position.X + (float)(Main.rand.Next(-100, 101))), (int)(player.position.Y - 250f), Mod.Find<ModNPC>("ScavengerBody").Type, 0, 0f, 0f, 0f, 0f, 255);
                SoundEngine.PlaySound(SoundID.Roar, player.position);
            }
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