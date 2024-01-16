using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class ElementalRay : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/ElementalRay");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Elemental Ray");
        Item.damage = 95;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 10;
        Item.width = 60;
        Item.height = 60;
        Item.useTime = 22;
        Item.useAnimation = 22;
        Item.useStyle = 5;
        Item.staff[Item.type] = true;
        ////Tooltip.SetDefault("Fires a beam of cosmic energy that can split into additional beams\nEnemies must be near for the beam to split");
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 7.5f;
        Item.value = 10000000;
        Item.rare = 10;
        Item.UseSound = SoundID.Item60;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("ElementRay").Type;
        Item.shootSpeed = 6f;
    }
    
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "GalacticaSingularity", 5);
        recipe.AddIngredient(ItemID.LunarBar, 5);
        recipe.AddIngredient(null, "TerraRay");
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}