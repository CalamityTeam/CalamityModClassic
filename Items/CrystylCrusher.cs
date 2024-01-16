using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items {
public class CrystylCrusher : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/CrystylCrusher");
        return true;
    }
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Crystyl Crusher");
        Item.damage = 255;
        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
        Item.width = 70;
        Item.height = 70;
        ////Tooltip.SetDefault("Gotta dig faster, gotta go deeper");
        Item.useTime = 1;
        Item.useAnimation = 30;
        Item.useTurn = true;
        Item.pick = 5000;
        Item.useStyle = 1;
        Item.knockBack = 9;
        Item.value = 10000000;
        Item.expert = true;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.tileBoost += 50;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "GallantPickaxe");
        recipe.AddIngredient(null, "ShadowspecBar", 5);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if(Main.rand.Next(3) == 0)
        {
        	int num307 = Main.rand.Next(3);
			if (num307 == 0)
			{
				num307 = 173;
			}
			else if (num307 == 1)
			{
				num307 = 57;
			}
			else
			{
				num307 = 58;
			}
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, num307);
        }
    }
}}