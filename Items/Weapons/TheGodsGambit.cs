using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class TheGodsGambit : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/4.SlimeGod/TheGodsGambit");
		return true;
	}
	
    public override void SetDefaults()
    {
    	Item.CloneDefaults(3291);
        //Tooltip.SetDefault("The God's Gambit");
        Item.damage = 28;
        Item.useTime = 21;
        Item.useAnimation = 21;
        Item.useStyle = 5;
        Item.channel = true;
        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
        Item.knockBack = 3.5f;
        Item.value = 300000;
        ////Tooltip.SetDefault("His most treasured relic");
        Item.rare = 4;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("TheGodsGambitProjectile").Type;
    }
    
    public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "PurifiedGel", 40);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
	}
}}