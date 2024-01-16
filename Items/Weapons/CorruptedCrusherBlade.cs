using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class CorruptedCrusherBlade : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/4.SlimeGod/CorruptedCrusherBlade");
        return true;
    }
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Corrupted Crusher Blade");
        Item.damage = 35;
        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
        Item.width = 56;
        Item.height = 60;
        Item.useTime = 31;
        Item.useAnimation = 31;
        Item.useTurn = true;
        Item.useStyle = 1;
        Item.knockBack = 6.25f;
        Item.value = 90000;
        Item.rare = 2;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
    }
    
    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.Next(7) == 0)
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 27);
        }
    }
    
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "EbonianGel", 15);
        recipe.AddIngredient(ItemID.EbonstoneBlock, 50);
        recipe.AddIngredient(ItemID.ShadowScale, 5);
        recipe.AddIngredient(ItemID.IronBar, 4);
        recipe.AddTile(TileID.DemonAltar);
        recipe.Register();
        recipe = CreateRecipe();
        recipe.AddIngredient(null, "EbonianGel", 15);
        recipe.AddIngredient(ItemID.EbonstoneBlock, 50);
        recipe.AddIngredient(ItemID.ShadowScale, 5);
        recipe.AddIngredient(ItemID.LeadBar, 4);
        recipe.AddTile(TileID.DemonAltar);
        recipe.Register();
    }
}}