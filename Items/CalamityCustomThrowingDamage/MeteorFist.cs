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
	public class MeteorFist : CalamityDamageItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Meteor Fist");
		}

		public override void SafeSetDefaults()
		{
			Item.width = 22;
			Item.damage = 15;
			Item.noMelee = true;
			Item.useTurn = true;
			Item.noUseGraphic = true;
			Item.useAnimation = 30;
			Item.useStyle = 5;
			Item.useTime = 30;
			Item.knockBack = 5.75f;
			Item.UseSound = SoundID.Item20;
			Item.autoReuse = true;
			Item.height = 28;
            Item.value = Item.buyPrice(0, 2, 0, 0);
            Item.rare = 2;
			Item.shoot = Mod.Find<ModProjectile>("MeteorFist").Type;
			Item.shootSpeed = 10f;
			Item.GetGlobalItem<CalamityGlobalItem>().rogue = true;
		}
	
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.MeteoriteBar, 10);
	        recipe.AddTile(TileID.Anvils);
	        recipe.Register();
		}
	}
}
