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
	public class GleamingMagnolia : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Gleaming Magnolia");
			// Tooltip.SetDefault("Casts a gleaming bolt that explodes into smaller bolts");
			Item.staff[Item.type] = true;
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 32;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 11;
	        Item.width = 52;
	        Item.height = 54;
	        Item.useTime = 27;
	        Item.useAnimation = 27;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 5.5f;
            Item.value = Item.buyPrice(0, 36, 0, 0);
            Item.rare = 5;
	        Item.UseSound = SoundID.Item109;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("GleamingBolt").Type;
	        Item.shootSpeed = 14f;
	    }
	    
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "ManaRose");
	        recipe.AddIngredient(ItemID.HallowedBar, 5);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	    }
	}
}