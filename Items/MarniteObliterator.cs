using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Items
{
	public class MarniteObliterator : ModItem
	{
		public override void SetDefaults()
		{
			//Tooltip.SetDefault("Marnite Obliterator");
			Item.damage = 13;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 36;
			Item.height = 18;
			Item.useTime = 7;
			Item.useAnimation = 25;
			Item.channel = true;
			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.pick = 57;
			Item.axe = 10;
			Item.hammer = 45;
			Item.tileBoost++;
			Item.useStyle = 5;
			Item.knockBack = 3;
			Item.value = Item.buyPrice(0, 1, 0, 0);
			Item.rare = 2;
			Item.UseSound = SoundID.Item23;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("MarniteObliterator").Type;
			Item.shootSpeed = 40f;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.PlatinumBar, 3);
			recipe.AddIngredient(ItemID.Granite, 5);
			recipe.AddIngredient(ItemID.Marble, 5);
	        recipe.AddTile(TileID.Anvils);
	        recipe.Register();
	        recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.GoldBar, 3);
			recipe.AddIngredient(ItemID.Granite, 5);
			recipe.AddIngredient(ItemID.Marble, 5);
	        recipe.AddTile(TileID.Anvils);
	        recipe.Register();
		}
	}
}