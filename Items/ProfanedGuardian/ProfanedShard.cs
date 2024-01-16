using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Items.ProfanedGuardian
{
	public class ProfanedShard : ModItem
	{
		public override void SetDefaults()
		{
			//Tooltip.SetDefault("Profaned Shard");
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 20;
			////Tooltip.SetDefault("A shard of the unholy flame");
			Item.rare = 10;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = 4;
			Item.UseSound = SoundID.Item44;
			Item.consumable = true;
			Item.shoot = Mod.Find<ModProjectile>("ProvSpawnBoss").Type;
		}
		
		public override bool CanUseItem(Player player)
		{
			return player.ZoneHallow || player.ZoneUnderworldHeight;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "UnholyEssence", 50);
			recipe.AddIngredient(ItemID.LunarBar, 3);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}