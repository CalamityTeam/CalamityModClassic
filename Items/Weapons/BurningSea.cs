using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class BurningSea : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/BurningSea");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Burning Sea");
        Item.damage = 65;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 15;
        Item.width = 28;
        Item.height = 30;
        Item.useTime = 22;
        Item.useAnimation = 22;
        Item.useStyle = 5;
        ////Tooltip.SetDefault("Casts brimstone fireballs that bounce\nWhen the fireballs strike water they emit tiny brimstone homing flares");
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 6.5f;
        Item.value = 300000;
        Item.rare = 6;
        Item.UseSound = SoundID.Item20;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("BrimstoneFireball").Type;
        Item.shootSpeed = 12f;
    }
    
    public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "UnholyCore", 5);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}