using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Items;

namespace CalamityModClassic1Point0.Items {
public class SkyfringePickaxe : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture = "CalamityModClassic1Point0/Items/SkyfringePickaxe");
        return true;
    }
	
    public override void SetDefaults()
    {
        //DisplayName.SetDefault("Skyfringe Pickaxe");
        Item.damage = 17;
        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
        Item.width = 19;
        Item.height = 19;
        Item.useTime = 15;
        Item.useAnimation = 15;
        Item.pick = 95;
        Item.useStyle = 1;
        Item.knockBack = 4;
        Item.value = 97000;
        Item.rare = 3;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "AerialiteBar", 7);
        recipe.AddIngredient(ItemID.SunplateBlock, 3);
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