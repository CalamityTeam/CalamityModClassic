using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
	public class BlushieStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Staff of Blushie");
			// Tooltip.SetDefault("Hold your mouse, wait, wait, wait, and put your trust in the power of blue magic");
		}

		public override void SetDefaults()
		{
			Item.width = 38;
			Item.height = 38;
			Item.useStyle = 4;
			Item.useAnimation = 30;
			Item.useTime = 30;
			Item.channel = true;
			Item.noMelee = true;
			Item.damage = 1;
			Item.knockBack = 1f;
			Item.autoReuse = false;
			Item.useTurn = false;
			Item.DamageType = DamageClass.Magic;
            Item.value = Item.buyPrice(5, 0, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item1;
			Item.shoot = Mod.Find<ModProjectile>("BlushieStaffProj").Type;
			Item.mana = 200;
			Item.shootSpeed = 0f;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 19;
		}
	    
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "Phantoplasm", 100);
	        recipe.AddIngredient(null, "ShadowspecBar", 50);
	        recipe.AddTile(null, "DraedonsForge");
	        recipe.Register();
	    }
	}
}