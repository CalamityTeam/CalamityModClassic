using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons
{
    public class AstralBlade : ModItem
    {
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Astral Blade");
		}

        public override void SetDefaults()
        {
            Item.damage = 83;
            Item.crit += 25;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 38;
            Item.height = 42;
            Item.useTime = 6;
            Item.useAnimation = 6;
			Item.useTurn = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 5f;
            Item.value = 350000;
            Item.rare = ItemRarityID.Lime;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }
        
        public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "AstralBar", 8);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	    }
        
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
        	if (Main.rand.NextBool(5))
	        {
	        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.ShadowbeamStaff);
	        }
        }
    }
}