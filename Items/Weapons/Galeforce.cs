using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;
//using TerrariaOverhaul;

namespace CalamityModClassicPreTrailer.Items.Weapons 
{
	public class Galeforce : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Galeforce");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 18;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 32;
	        Item.height = 52;
	        Item.useTime = 17;
	        Item.useAnimation = 17;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 4;
            Item.value = Item.buyPrice(0, 4, 0, 0);
            Item.rare = 3;
	        Item.UseSound = SoundID.Item5;
	        Item.autoReuse = true;
	        Item.shoot = 10;
	        Item.shootSpeed = 20f;
	        Item.useAmmo = 40;
	    }

        /*public void OverhaulInit()
        {
            this.SetTag("bow");
        }*/

        public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "AerialiteBar", 8);
	        recipe.AddIngredient(ItemID.SunplateBlock, 3);
	        recipe.AddTile(TileID.SkyMill);
	        recipe.Register();
	    }
	}
}