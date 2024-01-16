using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Items.SupremeCalamitas
{
	public class EyeofExtinction : ModItem
	{
		public override void SetDefaults()
		{
			//Tooltip.SetDefault("Eye of Extinction");
			Item.width = 40;
			Item.height = 40;
			Item.maxStack = 20;
			////Tooltip.SetDefault("Death");
			Item.rare = 10;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = 4;
			Item.UseSound = SoundID.Item44;
			Item.consumable = true;
			Item.shoot = Mod.Find<ModProjectile>("SupremeCalamitasSpawn").Type;
		}
		
		public override bool CanUseItem(Player player)
		{
			return !Main.dayTime;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "CosmiliteBar", 5);
			recipe.AddIngredient(null, "BlightedEyeball");
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}