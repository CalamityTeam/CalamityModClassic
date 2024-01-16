using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Items.Calamitas
{
	public class BlightedEyeball : ModItem
	{
		public override void SetDefaults()
		{
			//Tooltip.SetDefault("Eye of Desolation");
			Item.width = 28;
			Item.height = 18;
			Item.maxStack = 20;
			////Tooltip.SetDefault("Tonight is going to be a horrific night...");
			Item.rare = 6;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = 4;
			Item.UseSound = SoundID.Item44;
			Item.consumable = true;
			Item.shoot = Mod.Find<ModProjectile>("CalamitasSpawn").Type;
		}
		
		public override bool CanUseItem(Player player)
		{
			return !Main.dayTime;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.HallowedBar, 5);
			recipe.AddIngredient(ItemID.SoulofSight, 5);
			recipe.AddIngredient(ItemID.SoulofFright, 5);
			recipe.AddIngredient(null, "BlightedLens", 3);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}