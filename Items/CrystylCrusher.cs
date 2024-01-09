using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items {
public class CrystylCrusher : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Crystyl Crusher");
		//Tooltip.SetDefault("Gotta dig faster, gotta go deeper");
	}
		
    public override void SetDefaults()
    {
        Item.damage = 255;
        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
        Item.width = 70;
        Item.height = 70;
        Item.useTime = 1;
        Item.useAnimation = 30;
        Item.useTurn = true;
        Item.pick = 5000;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 9;
        Item.value = 10000000;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.tileBoost += 50;
    }
    
    public override void ModifyTooltips(List<TooltipLine> list)
    {
        foreach (TooltipLine line2 in list)
        {
            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
            {
                line2.OverrideColor = new Color(255, 0, 255);
            }
        }
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "GallantPickaxe");
        recipe.AddIngredient(null, "BlossomPickaxe");
        recipe.AddIngredient(null, "ShadowspecBar", 5);
        recipe.AddTile(null, "DraedonsForge");
        recipe.Register();
    }

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if(Main.rand.NextBool(3))
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