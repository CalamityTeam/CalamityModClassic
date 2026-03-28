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
	public class ScarletDevil : CalamityDamageItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Scarlet Devil");
			/* Tooltip.SetDefault("Throws an ultra high velocity spear, which creates more projectiles that home in\n"+
			"The spear creates a Scarlet Blast upon hitting an enemy\n"+
			"Stealth strikes grant you lifesteal\n"+
			"'Divine Spear \"Spear the Gungnir\"'"); */
		}

		public override void SafeSetDefaults()
		{
			Item.width = 94;
			Item.height = 94;
			Item.damage = 40000;
			Item.crit += 20;
			Item.noMelee = true;
            Item.noUseGraphic = true;
			Item.useAnimation = 60;
			Item.useStyle = 1;
			Item.useTime = 60;
			Item.knockBack = 8f;
			Item.UseSound = SoundID.Item60;
			Item.autoReuse = true;
            Item.value = Item.buyPrice(5, 0, 0, 0);
            Item.rare = 8;
			Item.shoot = Mod.Find<ModProjectile>("ScarletDevilProjectile").Type;
			Item.shootSpeed = 30f;
			Item.GetGlobalItem<CalamityGlobalItem>().rogue = true;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 16;
		}
	
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "ProfanedTrident");
            recipe.AddIngredient(null, "BloodstoneCore", 15);
            recipe.AddIngredient(ItemID.SoulofNight, 15);
            recipe.AddIngredient(null, "ShadowspecBar", 5);
            recipe.AddTile(null, "DraedonsForge");
            recipe.Register();
		}
	}
}
