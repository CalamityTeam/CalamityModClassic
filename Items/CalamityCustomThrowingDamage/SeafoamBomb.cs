using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage
{
	public class SeafoamBomb : CalamityDamageItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Seafoam Bomb");
			// Tooltip.SetDefault("Throws a bomb that explodes into a bubble which deals extra damage to enemies");
		}

		public override void SafeSetDefaults()
		{
			Item.width = 26;
			Item.height = 44;
			Item.damage = 18;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.useAnimation = 18;
			Item.useStyle = 1;
			Item.useTime = 18;
			Item.knockBack = 8f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.value = Item.buyPrice(0, 2, 0, 0);
			Item.rare = 2;
			Item.shoot = Mod.Find<ModProjectile>("SeafoamBomb").Type;
			Item.shootSpeed = 8f;
			Item.GetGlobalItem<CalamityGlobalItem>().rogue = true;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Bomb, 25);
	        recipe.AddIngredient(null, "SeaPrism", 10);
			recipe.AddTile(TileID.Anvils);
	        recipe.Register();
		}
	}
}
