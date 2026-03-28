using System;
using System.Collections.Generic;
using CalamityModClassicPreTrailer.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items
{
    public class AstralHamaxe : ModItem
    {
	    public override void SetStaticDefaults()
	    {
		    // DisplayName.SetDefault("Astral Hamaxe");
	    }
		
        public override void SetDefaults()
        {
            Item.damage = 70;
            Item.crit += 25;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 60;
            Item.height = 70;
            Item.useTime = 10;
            Item.useAnimation = 20;
            Item.useTurn = true;
            Item.axe = 30;
            Item.hammer = 150;
            Item.useStyle = 1;
            Item.knockBack = 5f;
            Item.value = Item.buyPrice(0, 60, 0, 0);
            Item.rare = 7;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.tileBoost += 3;
        }
    
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            Dust d = CalamityGlobalItem.MeleeDustHelper(player, Main.rand.Next(2) == 0 ? ModContent.DustType<AstralOrange>() : ModContent.DustType<AstralBlue>(), 0.48f, 50, 78, -0.1f, 0.1f);
            if (d != null)
            {
                d.customData = 0.02f;
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "AstralBar", 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}