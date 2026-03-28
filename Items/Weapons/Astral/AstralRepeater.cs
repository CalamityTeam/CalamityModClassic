using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;
//using TerrariaOverhaul;

namespace CalamityModClassicPreTrailer.Items.Weapons.Astral
{
	public class AstralRepeater : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Astral Bow");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 65;
	        Item.crit += 25;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 38;
	        Item.height = 78;
	        Item.useTime = 4;
	        Item.reuseDelay = 15;
	        Item.useAnimation = 12;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 2.5f;
            Item.value = Item.buyPrice(0, 60, 0, 0);
            Item.rare = 7;
	        Item.UseSound = SoundID.Item5;
	        Item.autoReuse = true;
	        Item.shoot = 10;
	        Item.shootSpeed = 16f;
	        Item.useAmmo = 40;
	    }

        /*public void OverhaulInit()
        {
            this.SetTag("crossbow");
        }*/

        public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "AstralBar", 7);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	    }
	}
}