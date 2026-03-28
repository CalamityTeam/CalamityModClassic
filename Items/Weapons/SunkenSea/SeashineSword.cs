using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Weapons.SunkenSea
{
    public class SeashineSword : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Seashine Sword");
			// Tooltip.SetDefault("Shoots an aqua sword beam");
		}

		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.EnchantedSword);
			Item.damage = 26;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.value = Item.buyPrice(0, 2, 0, 0);
			Item.width = 38;
			Item.height = 38;
			Item.knockBack = 2;
			Item.shootSpeed = 11;
			Item.rare = 2;
			Item.shoot = Mod.Find<ModProjectile>("SeashineSwordProj").Type;
			Item.UseSound = SoundID.Item1;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "SeaPrism", 7);
			recipe.AddIngredient(null, "Navystone", 10);
	        recipe.AddTile(TileID.Anvils);
	        recipe.Register();
		}
    }
}
