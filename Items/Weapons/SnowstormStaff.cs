using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class SnowstormStaff : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/5.Cryogen/SnowstormStaff");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Snowstorm Staff");
        Item.damage = 53;
        Item.DamageType = DamageClass.Magic;
        Item.channel = true;
        Item.mana = 13;
        Item.width = 66;
        Item.height = 66;
        Item.useTime = 22;
        Item.scale = 0.8f;
        Item.useAnimation = 22;
        Item.useStyle = 1;
        ////Tooltip.SetDefault("Fires a snowflake that follows the mouse cursor");
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 5;
        Item.value = 450000;
        Item.rare = 5;
        Item.UseSound = SoundID.Item46;
        Item.shoot = Mod.Find<ModProjectile>("Snowflake").Type;
        Item.shootSpeed = 7f;
    }
    
    public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "CryoBar", 6);
        recipe.AddTile(TileID.IceMachine);
        recipe.Register();
	}
}}