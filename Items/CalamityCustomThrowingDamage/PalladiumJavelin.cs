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
	public class PalladiumJavelin : CalamityDamageItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Palladium Javelin");
		}

		public override void SafeSetDefaults()
		{
			Item.width = 44;
			Item.damage = 41;
			Item.noMelee = true;
			Item.consumable = true;
			Item.noUseGraphic = true;
			Item.useAnimation = 19;
			Item.useStyle = 1;
			Item.useTime = 19;
			Item.knockBack = 5.5f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 44;
			Item.shoot = 330;
			Item.maxStack = 999;
			Item.value = 1200;
			Item.rare = 4;
			Item.shoot = Mod.Find<ModProjectile>("PalladiumJavelinProjectile").Type;
			Item.shootSpeed = 16f;
			Item.GetGlobalItem<CalamityGlobalItem>().rogue = true;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(20);
	        recipe.AddIngredient(ItemID.PalladiumBar);
	        recipe.AddTile(TileID.Anvils);
	        recipe.Register();
		}
	}
}
