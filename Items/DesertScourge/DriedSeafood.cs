using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Items.DesertScourge
{
	public class DriedSeafood : ModItem
	{
		public override void SetDefaults()
		{
			//Tooltip.SetDefault("Desert Medallion");
			Item.width = 28;
			Item.height = 28;
			Item.maxStack = 20;
			////Tooltip.SetDefault("The desert sand stirs...");
			Item.rare = 2;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = 4;
			Item.UseSound = SoundID.Item44;
			Item.consumable = true;
			Item.shoot = Mod.Find<ModProjectile>("ScourgeSpawn").Type;
		}
		
		public override bool CanUseItem(Player player)
		{
			return player.ZoneDesert;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.SandBlock, 25);
			recipe.AddIngredient(ItemID.AntlionMandible, 3);
			recipe.AddIngredient(ItemID.Cactus, 15);
			recipe.AddIngredient(null, "StormlionMandible");
			recipe.AddIngredient(ItemID.Coral, 5);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}