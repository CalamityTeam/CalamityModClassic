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
	public class CobaltKunai : CalamityDamageItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cobalt Kunai");
		}

		public override void SafeSetDefaults()
		{
			Item.width = 18;
			Item.damage = 28;
			Item.noMelee = true;
			Item.consumable = true;
			Item.noUseGraphic = true;
			Item.useAnimation = 12;
			Item.scale = 0.75f;
			Item.useStyle = 1;
			Item.useTime = 12;
			Item.knockBack = 2.5f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 40;
			Item.maxStack = 999;
			Item.value = 900;
			Item.rare = 4;
			Item.shoot = Mod.Find<ModProjectile>("CobaltKunaiProjectile").Type;
			Item.shootSpeed = 12f;
			Item.GetGlobalItem<CalamityGlobalItem>().rogue = true;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(30);
	        recipe.AddIngredient(ItemID.CobaltBar);
	        recipe.AddTile(TileID.Anvils);
	        recipe.Register();
		}
	}
}
