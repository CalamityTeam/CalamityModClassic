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
	public class WulfrumKnife : CalamityDamageItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Wulfrum Knife");
		}

		public override void SafeSetDefaults()
		{
			Item.width = 22;
			Item.damage = 8;
			Item.noMelee = true;
			Item.consumable = true;
			Item.noUseGraphic = true;
			Item.useAnimation = 15;
			Item.useStyle = 1;
			Item.useTime = 15;
			Item.knockBack = 1f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 38;
			Item.maxStack = 999;
			Item.value = 100;
			Item.rare = 1;
			Item.shoot = Mod.Find<ModProjectile>("WulfrumKnife").Type;
			Item.shootSpeed = 12f;
			Item.GetGlobalItem<CalamityGlobalItem>().rogue = true;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(50);
	        recipe.AddIngredient(null, "WulfrumShard");
	        recipe.AddTile(TileID.Anvils);
	        recipe.Register();
		}
	}
}
