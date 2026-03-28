using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.AbyssWeapons
{
	public class TyphonsGreed : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Typhon's Greed");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 75;
	        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
	        Item.width = 16;
	        Item.height = 16;
	        Item.useTime = 30;
	        Item.useAnimation = 30;
	        Item.useStyle = 5;
	        Item.noMelee = true;
            Item.noUseGraphic = true;
	        Item.knockBack = 5f;
            Item.value = Item.buyPrice(0, 60, 0, 0);
            Item.rare = 7;
	        Item.UseSound = SoundID.DD2_SkyDragonsFurySwing;
	        Item.autoReuse = true;
            Item.channel = true;
	        Item.shoot = Mod.Find<ModProjectile>("TyphonsGreedStaff").Type;
	        Item.shootSpeed = 24f;
	    }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "DepthCells", 30);
            recipe.AddIngredient(null, "Lumenite", 10);
            recipe.AddIngredient(null, "Tenebris", 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}