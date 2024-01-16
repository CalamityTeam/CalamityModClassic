using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Items.SlimeGod
{
	public class OverloadedSludge : ModItem
	{
		public override void SetDefaults()
		{
			//Tooltip.SetDefault("Overloaded Sludge");
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 20;
			////Tooltip.SetDefault("It looks corrupted");
			Item.rare = 5;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = 4;
			Item.UseSound = SoundID.Item44;
			Item.consumable = true;
			Item.shoot = Mod.Find<ModProjectile>("SlimeGodSpawn").Type;
		}
		
		public override bool CanUseItem(Player player)
		{
			return true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.SlimeCrown);
			recipe.AddIngredient(null, "EbonianGel", 50);
			recipe.AddIngredient(ItemID.EbonstoneBlock, 25);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
			recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.SlimeCrown);
			recipe.AddIngredient(null, "EbonianGel", 50);
			recipe.AddIngredient(ItemID.CrimstoneBlock, 25);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
			recipe = CreateRecipe();
			recipe.AddIngredient(null, "PurifiedGel", 20);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}