using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class TerraRay : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/TerraRay");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Terra Ray");
        Item.damage = 47;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 10;
        Item.width = 58;
        Item.height = 58;
        Item.useTime = 25;
        Item.useAnimation = 25;
        Item.useStyle = 5;
        Item.staff[Item.type] = true;
        ////Tooltip.SetDefault("Fires a beam of life energy that can split into additional beams\nEnemies must be near for the beam to split");
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 5.5f;
        Item.value = 1000000;
        Item.rare = 8;
        Item.UseSound = SoundID.Item60;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("TerraRay").Type;
        Item.shootSpeed = 6f;
    }
    
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "LivingShard", 5);
        recipe.AddIngredient(ItemID.ShadowbeamStaff);
        recipe.AddIngredient(null, "NightsRay");
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}