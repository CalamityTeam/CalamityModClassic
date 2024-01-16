using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Items.Cryogen
{
	public class CryoKey : ModItem
	{
		public override void SetDefaults()
		{
			//Tooltip.SetDefault("Cryo Key");
			Item.width = 28;
			Item.height = 18;
			Item.maxStack = 20;
			////Tooltip.SetDefault("Summons the gigantic floating ice mass");
			Item.rare = 5;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = 4;
			Item.UseSound = SoundID.Item44;
			Item.consumable = true;
			Item.shoot = Mod.Find<ModProjectile>("CryogenSpawn").Type;
		}
		
		public override bool CanUseItem(Player player)
		{
			return player.ZoneSnow;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.IceBlock, 50);
			recipe.AddIngredient(ItemID.SoulofNight, 3);
			recipe.AddIngredient(ItemID.SoulofLight, 3);
			recipe.AddIngredient(null, "EssenceofEleum", 5);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}