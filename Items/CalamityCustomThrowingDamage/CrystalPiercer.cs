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
	public class CrystalPiercer : CalamityDamageItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Crystal Piercer");
		}

		public override void SafeSetDefaults()
		{
			Item.width = 62;
			Item.damage = 43;
			Item.noMelee = true;
			Item.consumable = true;
			Item.noUseGraphic = true;
			Item.useAnimation = 20;
			Item.useStyle = 1;
			Item.useTime = 20;
			Item.knockBack = 6f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 62;
			Item.maxStack = 999;
			Item.value = 2500;
			Item.rare = 5;
			Item.shoot = Mod.Find<ModProjectile>("CrystalPiercerProjectile").Type;
			Item.shootSpeed = 20f;
			Item.GetGlobalItem<CalamityGlobalItem>().rogue = true;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(20);
	        recipe.AddIngredient(null, "VerstaltiteBar");
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
		}
	}
}
