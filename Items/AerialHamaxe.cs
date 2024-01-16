using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items {
public class AerialHamaxe : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/AerialHamaxe");
        return true;
    }
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Aerial Hamaxe");
        Item.damage = 25;
        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
        Item.width = 48;
        Item.height = 48;
        Item.useTime = 20;
        Item.useAnimation = 20;
        Item.useTurn = true;
        Item.axe = 20;
        Item.hammer = 75;
        Item.useStyle = 1;
        Item.knockBack = 7;
        Item.value = 50000;
        Item.rare = 3;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();  //Specific hamaxe 1 recipe.
        recipe.AddIngredient(null, "AerialiteBar", 6);
        recipe.AddIngredient(ItemID.SunplateBlock, 5);
        recipe.AddTile(TileID.SkyMill);
        recipe.Register();
    }

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if(Main.rand.Next(3) == 0)
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 59);
        }
    }
}}