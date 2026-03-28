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
	public class ExecutionersBlade : CalamityDamageItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Executioner's Blade");
		}

		public override void SafeSetDefaults()
		{
			Item.width = 50;
			Item.damage = 615;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.useTime = 3;
			Item.useAnimation = 9;
			Item.useStyle = 1;
			Item.knockBack = 6.75f;
			Item.UseSound = SoundID.Item73;
			Item.autoReuse = true;
			Item.height = 50;
            Item.value = Item.buyPrice(1, 80, 0, 0);
            Item.rare = 10;
            Item.shoot = Mod.Find<ModProjectile>("ExecutionersBlade").Type;
			Item.shootSpeed = 26f;
			Item.GetGlobalItem<CalamityGlobalItem>().rogue = true;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 14;
		}
		
		public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "CosmiliteBar", 11);
	        recipe.AddTile(null, "DraedonsForge");
	        recipe.Register();
	    }
	}
}
