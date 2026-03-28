using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons 
{
	public class Fabstaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Fabstaff");
			Item.staff[Item.type] = true;
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 800;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 50;
	        Item.width = 84;
	        Item.height = 84;
	        Item.useTime = 20;
	        Item.useAnimation = 20;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 5f;
            Item.value = Item.buyPrice(5, 0, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item60;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("FabRay").Type;
	        Item.shootSpeed = 6f;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 18;
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